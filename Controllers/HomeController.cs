using Microsoft.AspNetCore.Mvc;

namespace MunicipalServicesMvcCore.Controllers
{
    public class HomeController : Controller
    {
        // Main Menu
        public IActionResult Index() => View();
    }
}


