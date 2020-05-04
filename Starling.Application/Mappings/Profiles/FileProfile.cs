using AutoMapper;
using Starling.Application.DTOs;
using Starling.Application.Requests.Files.Queries.GetFiles;
using Starling.Domain.Models;
using Starling.Domain.Models.Filters;

namespace Starling.Application.Mappings.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileWithTotal, FileDto>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
            CreateMap<GetFilesQuery, GetFilesFilter>();
        }
    }
}