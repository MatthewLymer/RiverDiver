using Microsoft.AspNetCore.Mvc;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}