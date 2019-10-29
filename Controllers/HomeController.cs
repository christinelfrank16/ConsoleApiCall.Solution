using Microsoft.AspNetCore.Mvc;
using ConsoleApiCall.Models;

namespace ConsoleApiCall.Controllers
{
    public class HomeController : Controller
    {
        private string _apikey = System.Environment.GetEnvironmentVariable("API_KEY");
        [Route("/")]
        public IActionResult Index()
        {
            var allArticles = Article.GetArticles(_apikey);
            return View(allArticles);
        }
    }
}