using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc.Testing;

namespace Triggerfish.Testing.Tests
{
	public class UrlHelpersTests
	{
		[TestMethod]
		public void ShouldSanitiseValidRoute()
		{
			Assert.AreEqual("/artists", UrlHelpers.SanitiseUrl("/artists", RegisterRoutes));
		}

		[TestMethod]
		public void ShouldSanitiseInvalidRoute()
		{
			Assert.AreEqual("/", UrlHelpers.SanitiseUrl("/invalid", RegisterRoutes));
		}

		[TestMethod]
		public void ShouldSanitiseRouteRequiringAuthorisation1()
		{
			Assert.AreEqual("/secret", UrlHelpers.SanitiseUrl("/secret", RegisterRoutes));
		}

		[TestMethod]
		public void ShouldSanitiseRouteRequiringAuthorisation2()
		{
			Assert.AreEqual("/", UrlHelpers.SanitiseUrl("/secret", RegisterRoutes, false));
		}
	
		private static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"artists/{genre}",
				new { controller = "Artists", Action = "List", genre = "All" }
			);
			
			routes.MapRoute(
				null,
				"secret",
				new { controller = "Secret", Action = "Index" }
			);		
		}
	}

	public class SecretController
	{
		[Authorize]
		public ActionResult Index()
		{
			return null;
		}
	}
}
