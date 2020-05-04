using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Models.Filters;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Shares.Queries.GetShares
{
    public class GetSharesQueryHandler : IRequestHandler<GetSharesQuery, ShareSearchResult>
    {
        private readonly IShareRepository _shareRepository;
        private readonly IMapper _mapper;

        public GetSharesQueryHandler(IShareRepository shareRepository, IMapper mapper)
        {
            _shareRepository = shareRepository;
            _mapper = mapper;
        }
        
        public async Task<ShareSearchResult> Handle(GetSharesQuery request, CancellationToken cancellationToken)
        {
            var filters = _mapper.Map<GetSharesFilter>(request);
            var shares = await _shareRepository.GetShares(filters);

            var sharesDto = _mapper.Map<IList<ShareDto>>(shares);
            foreach (var shareDto in sharesDto)
            {
                shareDto.UsersQuantity = await _shareRepository.GetUsersQuantity(shareDto.Id);
                shareDto.FilesQuantity = await _shareRepository.GetFilesQuantity(shareDto.Id);
            }

            return new ShareSearchResult
            {
                Shares = sharesDto,
                Total = shares.FirstOrDefault()?.TotalQuantity ?? 0
            };
        }
    }
}