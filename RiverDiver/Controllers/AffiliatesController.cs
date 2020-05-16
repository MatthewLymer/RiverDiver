using System.Web.Mvc;

namespace RiverDiver.Controllers
{
    public class AffiliatesController : Controller
    {
        //
        // GET: /Affiliates/

        public ActionResult Index()
        {
            return Redirect("~/");
        }
    }
}
