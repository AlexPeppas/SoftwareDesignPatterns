using Microsoft.AspNetCore.Mvc;

namespace DesignPatternsApi
{
    [ApiController]
    [Route("api/demo/")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "demonstrate/decorator")]
        [Validator]
        public IActionResult DemoDecorater([FromBody] DemoItem item)
        {
            Console.WriteLine("Succeed");

            return Ok();
        }
    }
}