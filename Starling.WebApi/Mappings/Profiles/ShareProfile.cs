using AutoMapper;
using Starling.Application.Requests.Shares.Commands.AddShare;
using Starling.Application.Requests.Shares.Commands.SignShareFile;
using Starling.Application.Requests.Shares.Queries.GetShares;
using Starling.WebApi.Mappings.Converters;
using Starling.WebApi.Models.Shares;

namespace Starling.WebApi.Mappings.Profiles
{
    public class ShareProfile : Profile
    {
        public ShareProfile()
        {
            CreateMap<AddShareRequestViewModel, AddShareCommand>()
                .ForMember(command => command.Files, options => options.ConvertUsing(new FilesConverter()));
            CreateMap<GetSharesRequestViewModel, GetSharesQuery>();
            CreateMap<SignShareFileRequestViewModel, SignShareFileCommand>();
        }
    }
}