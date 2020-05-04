using AutoMapper;
using Starling.Application.DTOs;
using Starling.Domain.Models;

namespace Starling.Application.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserSearchDto>();
        }
    }
}