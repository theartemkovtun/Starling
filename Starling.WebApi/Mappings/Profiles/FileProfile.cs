using AutoMapper;
using Starling.Application.Requests.Files.Commands.UpdateFile;
using Starling.Application.Requests.Files.Queries.GetFiles;
using Starling.WebApi.Models.Files;

namespace Starling.WebApi.Mappings.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<GetFilesRequestViewModel, GetFilesQuery>();
            CreateMap<UpdateFileRequestViewModel, UpdateFileCommand>();
        }
    }
}