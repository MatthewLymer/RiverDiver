using Microsoft.AspNetCore.Mvc;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class ChartersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}