using System;
using System.Web;
using System.Web.Mvc;

namespace RiverDiver
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HttpContext.Current.RewritePath(Request.ApplicationPath);
			IHttpHandler httpHandler = new MvcHttpHandler();
			httpHandler.ProcessRequest(HttpContext.Current);
			Response.End();
		}
	}
}