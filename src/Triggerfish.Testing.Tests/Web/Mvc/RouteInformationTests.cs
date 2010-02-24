using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Triggerfish.Web.Mvc.Testing;
using Triggerfish.Web.Routing.Testing;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Testing.Tests
{
	[TestClass]
	public class RouteInformationTests
	{
		[TestMethod]
		public void ShouldGetRouteInformation()
		{
			RouteValueDictionary expected = new RouteValueDictionary(new {
				controller = "Artists",
				action = "List",
				genre = "All"
			});

			RouteInformation ri = new RouteInformation("~/Artists", RegisterRoutes);

			Assert.AreEqual("~/Artists", ri.Url);
			Assert.IsTrue(ri.Valid);
			Assert.AreEqual("Artists", ri.Controller);
			Assert.AreEqual("List", ri.Action);
			Assert.IsTrue(ri.IsActionOnController("Artists"));
			Assert.IsTrue(ri.IsActionOnController("ArtistsController"));
			RoutingAssert.AreDictionariesEqual(expected, ri.RouteValues);
		}

		[TestMethod]
		public void ShouldProperlyFormatUrl1()
		{
			RouteInformation ri = new RouteInformation("/Artists", RegisterRoutes);

			Assert.AreEqual("/Artists", ri.Url);
		}

		[TestMethod]
		public void ShouldProperlyFormatUrl2()
		{
			RouteInformation ri = new RouteInformation("Artists", RegisterRoutes);

			Assert.AreEqual("Artists", ri.Url);
		}

		[TestMethod]
		public void ShouldNotGetRouteInformationForNullUrl()
		{
			RouteInformation ri = new RouteInformation("", RegisterRoutes);

			Assert.IsFalse(ri.Valid);
		}

		[TestMethod]
		public void ShouldNotGetRouteInformationForNonExistentRoute()
		{
			RouteInformation ri = new RouteInformation("/Invalid", RegisterRoutes);

			Assert.IsFalse(ri.Valid);
		}

		[TestMethod]
		public void ShouldNotGetRouteInformationForAbsoluteRoute()
		{
			RouteInformation ri = new RouteInformation("http://localhost/Artists", RegisterRoutes);

			Assert.IsFalse(ri.Valid);
		}

		[TestMethod]
		public void ShouldGetAuthorizeRequired()
		{
			RouteInformation ri = new RouteInformation("/Artists", RegisterRoutes);

			Assert.IsTrue(ri.DoesActionRequireAuthorisation());
		}

		[TestMethod]
		public void ShouldGetAuthoriseRequired()
		{
			RouteInformation ri = new RouteInformation("/Home", RegisterRoutes);

			Assert.IsTrue(ri.DoesActionRequireAuthorisation());
		}

		[TestMethod]
		public void ShouldNotGetAuthoriseRequired()
		{
			RouteInformation ri = new RouteInformation("/Test", RegisterRoutes);

			Assert.IsFalse(ri.DoesActionRequireAuthorisation());
		}

		private static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"Artists/{genre}",
				new { controller = "Artists", Action = "List", genre = "All" }
			);
			routes.MapRoute(
				null,
				"Home",
				new { controller = "Artists", Action = "Index" }
			);
			routes.MapRoute(
				null,
				"Test",
				new { controller = "Artists", Action = "Test" }
			);
		}
	}

	public class ArtistsController : Controller
	{
		[Authorize()]
		public ActionResult List(string genre)
		{
			return null;
		}
	
		[Authorise()]
		public ActionResult Index()
		{
			return null;
		}
	
		public ActionResult Test()
		{
			return null;
		}
	}
}
