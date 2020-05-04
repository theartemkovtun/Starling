using AutoMapper;
using Starling.Application.DTOs;
using Starling.Application.Requests.Shares.Queries.GetShares;
using Starling.Domain.Models;
using Starling.Domain.Models.Filters;

namespace Starling.Application.Mappings.Profiles
{
    public class ShareProfile : Profile
    {
        public ShareProfile()
        {
            CreateMap<Share, ShareDto>().ReverseMap();
            CreateMap<GetSharesQuery, GetSharesFilter>();
            CreateMap<ShareForUser, ShareForUserDto>()
                .ForMember(dto => dto.CanApprove, options => options.Ignore())
                .ForMember(dto => dto.FilesSigning, options => options.Ignore());
            CreateMap<ShareUserInfo, ShareUserInfoDto>();
            CreateMap<ShareFileSigning, ShareFileSigningDto>();
        }
    }
}