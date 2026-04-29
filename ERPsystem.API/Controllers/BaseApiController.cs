using Microsoft.AspNetCore.Mvc;

namespace ERPsystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        // Here you can inject MediatR ISender, or extract TenantId, UserId from HttpContext 
        // to be used by all controllers.
    }
}
