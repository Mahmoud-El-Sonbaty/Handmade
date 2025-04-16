using Microsoft.AspNetCore.Mvc;

namespace Handmade.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return Ok("site tracking");
        }
    }
}
