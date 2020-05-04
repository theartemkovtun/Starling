using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starling.Application.Requests.Files.Commands.AddFile;
using Starling.Application.Requests.Files.Commands.UpdateFile;
using Starling.Application.Requests.Files.Queries.GetFile;
using Starling.Application.Requests.Files.Queries.GetFileContent;
using Starling.Application.Requests.Files.Queries.GetFiles;
using Starling.Shared.Extensions;
using Starling.WebApi.Models.Files;

namespace Starling.WebApi.Controllers
{
    [Authorize]
    [Route("api/files")]
    public class FileController : MediatorController 
    {
        [HttpGet("{id}" ,Name = nameof(GetFile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFile([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetFileQuery(id)));
        }
        
        [HttpGet("{id}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFile([FromRoute] Guid id)
        {
            var file = await Mediator.Send(new GetFileContentQuery(id));
            return File(file.Content, file.Name.MimeType(), file.Name);
        }
        
        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFiles([FromBody] GetFilesRequestViewModel request)
        {
            var command = Mapper.Map(request, new GetFilesQuery(Username));
            return Ok(await Mediator.Send(command));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFile([FromForm] IFormFile file)
        {
            await using var content = new MemoryStream();
            file.CopyTo(content);

            var command = new AddFileCommand(file.FileName, content, Username);
            await Mediator.Send(command);

            return CreatedAtRoute(nameof(GetFile), new {id = command.FileId}, null);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFile([FromForm] UpdateFileRequestViewModel request, [FromForm] IFormFile file)
        {
            await using var content = new MemoryStream();
            file.CopyTo(content);

            var command = Mapper.Map(request, new UpdateFileCommand(Username,content));
            await Mediator.Send(command);

            return Ok();
        }
    }
}