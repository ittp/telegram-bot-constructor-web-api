using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}