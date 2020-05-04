using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Shares.Queries.GetShare
{
    public class GetShareQueryHandler : IRequestHandler<GetShareQuery, ShareForUserDto>
    {
        private readonly IShareRepository _shareRepository;
        private readonly IMapper _mapper;

        public GetShareQueryHandler(IShareRepository shareRepository, IMapper mapper)
        {
            _shareRepository = shareRepository;
            _mapper = mapper;
        }
        
        public async Task<ShareForUserDto> Handle(GetShareQuery request, CancellationToken cancellationToken)
        {
            var share = await _shareRepository.GetShareForUser(request.Username, request.ShareId);
            var shareDto = _mapper.Map<ShareForUserDto>(share);

            shareDto.FilesSigning = _mapper.Map<IList<ShareFileSigningDto>>(await _shareRepository.GetShareFilesSigning(request.Username, share.Id));
            shareDto.Users = _mapper.Map<IList<ShareUserInfoDto>>(await _shareRepository.ShareUserApprovements(share.Id));
            
            return shareDto;
        }
    }
}