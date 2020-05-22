using Microsoft.AspNetCore.Mvc;
using RiverDiver.Web.App.Models;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class WaiverController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WaiverModel waiver)
        {
            return View("Index", waiver);
        }
    }
}