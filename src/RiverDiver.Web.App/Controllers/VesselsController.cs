using Microsoft.AspNetCore.Mvc;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class VesselsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}