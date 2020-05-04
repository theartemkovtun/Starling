using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Users.Queries.SearchForUsers
{
    public class SearchForUsersQueryHandler : IRequestHandler<SearchForUsersQuery, IEnumerable<UserSearchDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SearchForUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<UserSearchDto>> Handle(SearchForUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync(request.Username, request.Search, request.Take ?? 10);

            return _mapper.Map<IEnumerable<UserSearchDto>>(users);
        }
    }
}