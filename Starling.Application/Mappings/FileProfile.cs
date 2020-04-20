using AutoMapper;
using Starling.Application.DTOs;
using Starling.Domain.Models;

namespace Starling.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<File, DocumentDto>().ReverseMap();
        }
    }
}