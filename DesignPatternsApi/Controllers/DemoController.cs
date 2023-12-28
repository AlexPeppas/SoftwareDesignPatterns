using DesignPatternsApi.DependencyInjection;

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

        [HttpPost]
        [Route("demonstrate/decorator")]
        [Validator]
        public IActionResult DemoDecorater([FromBody] DemoItem item)
        {
            Console.WriteLine("Succeed");

            return Ok();
        }

        [HttpGet]
        [Route("DI/keyedServices")]
        public async Task<IActionResult> KeyedDIDemo([FromKeyedServices("dummyCache")] IDummyMemoryCache cache)
        {
            await cache.RememberMe();

            return Ok();
        }
    }
}