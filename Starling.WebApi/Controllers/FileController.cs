using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starling.Application.Requests.Documents.Queries.GetDocumentContent;
using Starling.Application.Requests.Files.Commands.AddFile;
using Starling.Application.Requests.Files.Queries.GetFile;
using Starling.Application.Requests.Files.Queries.GetFileContent;

namespace Starling.WebApi.Controllers
{
    [Route("api/documents")]
    public class DocumentController : MediatorController 
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetFileQuery(id)));
        }
        
        [HttpGet("{id}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Download([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetFileContentQuery(id)));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            await using var content = new MemoryStream();
            file.CopyTo(content);

            var command = new AddFileCommand(file.FileName, content);
            await Mediator.Send(command);

            return Ok();
        }
    }
}