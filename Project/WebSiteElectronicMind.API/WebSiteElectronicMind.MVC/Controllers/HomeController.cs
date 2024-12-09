using Microsoft.AspNetCore.Mvc;

namespace WebSiteElectronicMind.MVC.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult Success()
        {
            return View();
        }
    }
}
