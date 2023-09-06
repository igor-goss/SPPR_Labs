using Microsoft.AspNetCore.Mvc;

namespace Web_3_Shevelenkov.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
