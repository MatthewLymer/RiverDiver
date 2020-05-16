using Microsoft.AspNetCore.Mvc;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
