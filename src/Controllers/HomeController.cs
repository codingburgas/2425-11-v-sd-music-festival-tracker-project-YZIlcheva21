using Microsoft.AspNetCore.Mvc;

namespace MusicFestivalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
