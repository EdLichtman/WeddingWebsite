﻿using AutoMapper;
using WeddingWebsiteApi.Authorization;

namespace WeddingWebsiteApi.Controllers.Accounts.ViewModels
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
