using FluentValidation;
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
        private readonly IValidator<AddUsersCommand> _userValidatorAdd;
        public UserController(IValidator<AddUsersCommand> userValidatorAdd)
        {
            _userValidatorAdd = userValidatorAdd;
        }

        #region Queries
        [HttpGet("GetAllUsers")]
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
            var validate = await _userValidatorAdd.ValidateAsync(command);
            if (!validate.IsValid)
            {
                var firstError = validate.Errors.FirstOrDefault()?.ErrorMessage;
                var errorResponse = new { mensaje = firstError };
                return BadRequest(errorResponse);
            }
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
