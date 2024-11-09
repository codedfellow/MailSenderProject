using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkMailAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Bulk Mail API","text/plain");
        }
    }
}
