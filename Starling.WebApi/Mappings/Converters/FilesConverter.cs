using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Starling.Application.DTOs;

namespace Starling.WebApi.Mappings.Converters
{
    public class FilesConverter : IValueConverter<IList<IFormFile>, IList<FileDataDto>>
    {
        public IList<FileDataDto> Convert(IList<IFormFile> formFiles, ResolutionContext context)
        {
            return formFiles.Select(file =>
            {
                using var content = new MemoryStream();
                file.CopyTo(content);
                
                return new FileDataDto(file.FileName, content.ToArray());
            }).ToList();
        }
    }
}