using FluentValidation.Attributes;

namespace WeddingWebsiteApi.Controllers.Accounts.ViewModels
{
    [Validator(typeof(RegistrationViewModelValidator))]
    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
