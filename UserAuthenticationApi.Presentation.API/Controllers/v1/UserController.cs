using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands;
using UserAuthenticationApi.Core.Application.Feautures.Users.Queries;

namespace UserAuthenticationApi.Presentation.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        #region Quiries
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post([FromBody] AddUsersCommand command)
        {
            if (!ModelState.IsValid) return BadRequest("Debe de enviar los datos correctamente");
            await Mediator.Send(command);
            return Created();
        }
        #endregion

    }
}
