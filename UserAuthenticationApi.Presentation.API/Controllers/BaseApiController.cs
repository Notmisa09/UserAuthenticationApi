using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserAuthenticationApi.Presentation.API.Controllers
{
    [Controller]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
