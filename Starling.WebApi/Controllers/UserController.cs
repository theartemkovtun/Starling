using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starling.Application.Requests.Users.Commands.AuthorizeUser;
using Starling.Application.Requests.Users.Commands.LogoutUser;
using Starling.Application.Requests.Users.Commands.RefreshAuthorizationToken;
using Starling.Application.Requests.Users.Commands.RegisterUser;
using Starling.Application.Requests.Users.Queries.SearchForUsers;
using Starling.WebApi.Models.Users;

namespace Starling.WebApi.Controllers
{
    
    [Authorize]
    [Route("api/users")]
    public class UserController : MediatorController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
        
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchUsers([FromBody] SearchForUsersRequestViewModel request)
        {
            var query = Mapper.Map(request, new SearchForUsersQuery(Username));
            return Ok(await Mediator.Send(query));
        }
        
        
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthorizeUser([FromBody] AuthorizeUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] LogoutUserRequestViewModel request)
        {
            var command = Mapper.Map(request, new LogoutUserCommand(Username));
            return Ok(await Mediator.Send(command));
        }
        
        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshAuthorizationTokenCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}