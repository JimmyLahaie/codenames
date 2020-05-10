using System.Web.Mvc;
using System.Web.Routing;

namespace CodeNames.WebApp
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Game",
				"Game/{key}/{action}",
				new {controller = "Game"}
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {controller = "Home", action = "Index", id = UrlParameter.Optional}
			);
		}
	}
}