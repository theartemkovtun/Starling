using System.Collections.Generic;
using MediatR;
using Starling.Application.DTOs;

namespace Starling.Application.Requests.Users.Queries.SearchForUsers
{
    public class SearchForUsersQuery : IRequest<IEnumerable<UserSearchDto>>
    {
        public SearchForUsersQuery(string username)
        {
            Username = username;
        }
        
        public string Search { get; set; }
        public int? Take { get; set; }
        public string Username { get; set; }
    }
}