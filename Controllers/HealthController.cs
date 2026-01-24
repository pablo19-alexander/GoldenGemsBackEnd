using Microsoft.AspNetCore.Mvc;

namespace GoldenGemsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                service = "GoldenGems API"
            });
        }
    }
}
