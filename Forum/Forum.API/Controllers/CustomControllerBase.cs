using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers.V1.Admin
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
    }
}
