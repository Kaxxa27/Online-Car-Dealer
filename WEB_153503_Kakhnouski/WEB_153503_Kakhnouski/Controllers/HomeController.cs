using Microsoft.AspNetCore.Mvc;

namespace WEB_153503_Kakhnouski.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
