using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestTimeConsumingOperationWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestLongOperationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Simulate a long-running operation
            int secondsDelay = 5;
            await Task.Delay(secondsDelay * 1000);
            return Ok("Web API long operation completed successfully.");
        }
    }
}
