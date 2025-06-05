using Microsoft.AspNetCore.Mvc;

namespace UserAuthenticationApi.Presentation.API.Controllers
{
    [Controller]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
       
    }
}
