using Microsoft.AspNetCore.Mvc;

namespace ConsoleApiCall.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Ok("Hello World from a controller");
        }
    }
}