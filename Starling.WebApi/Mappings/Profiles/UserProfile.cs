using AutoMapper;
using Starling.Application.Requests.Users.Commands.LogoutUser;
using Starling.Application.Requests.Users.Queries.SearchForUsers;
using Starling.WebApi.Models.Users;

namespace Starling.WebApi.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SearchForUsersRequestViewModel, SearchForUsersQuery>();
            CreateMap<LogoutUserRequestViewModel, LogoutUserCommand>();
        }
    }
}