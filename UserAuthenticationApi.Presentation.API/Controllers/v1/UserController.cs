using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Delete;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Login;
using UserAuthenticationApi.Core.Application.Feautures.Users.Queries.GetAll;

namespace UserAuthenticationApi.Presentation.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseApiController
    {
        #region Queries
        [HttpGet("GetAllUsers")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }
        #endregion

        #region Commands
        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> RegisterUser([FromBody] AddUsersCommand command)
        {
            if (!ModelState.IsValid) return BadRequest("Debe de enviar los datos correctamente");
            await Mediator.Send(command);
            return Created();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromQuery] LoginCommand command)
        {
            if (!ModelState.IsValid) return BadRequest("Debe de enviar los datos correctamente");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Remove([FromQuery] DeleteUserCommand command)
        {
            if (!ModelState.IsValid) return BadRequest("Debe de enviar los datos correctamente");
            var result = await Mediator.Send(command);
            return NoContent();
        }
        #endregion

    }
}
