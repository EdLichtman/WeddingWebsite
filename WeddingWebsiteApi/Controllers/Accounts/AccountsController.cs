using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsiteApi.Authorization;
using WeddingWebsiteApi.Configuration;
using WeddingWebsiteApi.Controllers.Accounts.ViewModels;
using WeddingWebsiteApi.Helpers;

namespace WeddingWebsiteApi.Controllers.Accounts
{
    [Route("api/[controller]/[action]")]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController (UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.AdminUsers.AddAsync(new AdminUser { IdentityId = userIdentity.Id});
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}
