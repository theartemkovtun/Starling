using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Starling.Application.DTOs;

namespace Starling.WebApi.Mappings.Converters
{
    public class FileConverter : IValueConverter<FormFile, FileDataDto>
    {
        public FileDataDto Convert(FormFile sourceMember, ResolutionContext context)
        {
            using var content = new MemoryStream();
            sourceMember.CopyTo(content);
            return new FileDataDto(sourceMember.FileName, content.ToArray());
        }
    }
}