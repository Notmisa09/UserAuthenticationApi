using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Delete;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Login;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update;
using UserAuthenticationApi.Core.Application.Feautures.Users.Queries.GetAll;

namespace UserAuthenticationApi.Presentation.API.Controllers.v1
{
    [ApiController]
    [SwaggerTag("Mantenimiento de usuarios")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseApiController
    {
        private readonly IValidator<AddUsersCommand> _userValidatorAdd;
        private readonly IValidator<UpdateUserCommand> _userValidatorUpdate;

        public UserController(IValidator<AddUsersCommand> userValidatorAdd
            , IValidator<UpdateUserCommand> userValidatorUpdate)
        {
            _userValidatorAdd = userValidatorAdd;
            _userValidatorUpdate = userValidatorUpdate;
        }

        #region Queries
        [Authorize]
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Obtiene todos los usuarios de la aplicaiones",
            Description = "Permite obtener la info basica del usuario y sus telefonos"
         )]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
        Summary = "Permite crear un usuario",
        Description = "Recibe los parametros necesarios para crear un usuario con una lista de telefonos")]

        public async Task<IActionResult> RegisterUser([FromBody] AddUsersCommand command)
        {
            var validate = await _userValidatorAdd.ValidateAsync(command);
            if (!validate.IsValid)
            {
                var firstError = validate.Errors.FirstOrDefault()?.ErrorMessage;
                var errorResponse = new { mensaje = firstError };
                return BadRequest(errorResponse);
            };
            await Mediator.Send(command);
            return Created();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Permite loguearte como usuario de la aplicación",
        Description = "Recibe los parametros para poder autenticar tu usuario y obtener el JWT")]
        public async Task<IActionResult> Login([FromQuery] LoginCommand command)
        {
            if (!ModelState.IsValid) return BadRequest("Debe de enviar los datos correctamente");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Permite eliminar un usuario de la aplicacion",
        Description = "Recibe los parametros para poder eliminar tu usuario (soft delete)")]
        public async Task<IActionResult> Remove([FromQuery] DeleteUserCommand command)
        {
            if (!ModelState.IsValid) return BadRequest("Debe de enviar los datos correctamente");
            var result = await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Permite a los usuarios actualizar su información",
        Description = "Recibe los parametros para poder actualizar la informacion del usuario")]
        public async Task<IActionResult> Update([FromQuery] UpdateUserCommand command)
        {
            var validate = await _userValidatorUpdate.ValidateAsync(command);
            if (!validate.IsValid)
            {
                var firstError = validate.Errors.FirstOrDefault()?.ErrorMessage;
                var errorResponse = new { mensaje = firstError };
                return BadRequest(errorResponse);
            };
            await Mediator.Send(command);
            return NoContent();
        }
        #endregion
    }
}
