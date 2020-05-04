using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starling.Application.Requests.Shares.Commands.AcceptShare;
using Starling.Application.Requests.Shares.Commands.AddShare;
using Starling.Application.Requests.Shares.Commands.DeleteShare;
using Starling.Application.Requests.Shares.Commands.RejectShare;
using Starling.Application.Requests.Shares.Commands.SignShareFile;
using Starling.Application.Requests.Shares.Commands.VerifySignatures;
using Starling.Application.Requests.Shares.Queries.GetShare;
using Starling.Application.Requests.Shares.Queries.GetShares;
using Starling.WebApi.Models.Shares;

namespace Starling.WebApi.Controllers
{
    [Authorize]
    [Route("api/shares")]
    public class ShareController : MediatorController
    {
        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FilterShares([FromBody] GetSharesRequestViewModel request)
        {
            var query = Mapper.Map(request, new GetSharesQuery(Username));
            return Ok(await Mediator.Send(query));
        }
        
        [HttpGet("{id}", Name=nameof(GetShare))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetShare([FromRoute] Guid id)
        {
            var query = new GetShareQuery(Username, id);
            return Ok(await Mediator.Send(query));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddShare([FromForm] AddShareRequestViewModel requestViewModel)
        {
            var command = Mapper.Map(requestViewModel, new AddShareCommand(Username));
            await Mediator.Send(command);

            return CreatedAtRoute(nameof(GetShare), new {id = command.Id}, null);
        }
        
        [HttpPost("signFileByPassword")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptShare([FromBody] SignShareFileRequestViewModel request)
        {
            var command = Mapper.Map(request, new SignShareFileCommand(Username));
            return Ok(await Mediator.Send(command));
        }
        
        [HttpPost("{id}/accept")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptShare([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new AcceptShareCommand(id, Username)));
        }
        
        [HttpPost("verify")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptShare([FromBody] VerifySignaturesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [HttpPost("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectShare([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new RejectShareCommand(id, Username)));
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShare([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new DeleteShareCommand(id)));
        }
    }
}