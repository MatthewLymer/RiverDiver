using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RiverDiver
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Blank",
				"",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new[] { "RiverDiver.Controllers" }
			);

			routes.MapRoute(
				"Default",
				"{controller}.aspx/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new[] { "RiverDiver.Controllers" }
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}
	}
}