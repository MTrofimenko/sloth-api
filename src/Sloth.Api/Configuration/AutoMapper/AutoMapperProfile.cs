using AutoMapper;
using Sloth.Api.Models;
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

            CreateMap<ChatMessage, ChatMessageDto>()
                .ForMember(dest => dest.UserId, 
                    opt => opt.MapFrom(src => src.Sender.UserId));
            // SendDate = x.CreatedOn.Value TODO: check why is not set on db

            CreateMap<ChatMember, ChatMemberDto>()
                .ForMember(dest => dest.ChatMemberId,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}