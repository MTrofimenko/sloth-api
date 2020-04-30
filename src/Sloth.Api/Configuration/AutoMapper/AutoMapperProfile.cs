using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sloth.Auth.Models;
using Sloth.DB.Models;

namespace Sloth.Api.Configuration.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentityModel, User>();
            CreateMap<RegisterModel, User>();
            CreateMap<User, CurrentUser>();
        }
    }
}
