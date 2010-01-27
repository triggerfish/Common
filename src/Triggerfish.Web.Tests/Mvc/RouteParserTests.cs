using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Triggerfish.Web.Mvc;
using System.Web.Routing;
using Triggerfish.Web.Routing.Testing;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class RouteParserTests
	{
		private const string c_controllerName = "Mock";
		private const string c_actionName = "Index";

		private Type m_mockController;
		private MethodInfo m_mockAction;
		private RouteAttribute m_routeAttribute;

		[TestInitialize]
		public void Setup()
		{
			Mock<Type> mockController = new Mock<Type>();
			mockController
				.Setup(x => x.Name)
				.Returns(c_controllerName + "Controller");
			m_mockController = mockController.Object;

			Mock<MethodInfo> mockAction = new Mock<MethodInfo>();
			mockAction
				.Setup(x => x.Name)
				.Returns(c_actionName);
			mockAction
				.Setup(x => x.DeclaringType)
				.Returns(m_mockController);
			mockAction
				.Setup(x => x.GetCustomAttributes(typeof(RouteDefaultAttribute), true))
				.Returns(new RouteDefaultAttribute[] {
					new RouteDefaultAttribute { 
						Name = "Page", 
						Value = 1 }
				});
			mockAction
				.Setup(x => x.GetCustomAttributes(typeof(RouteConstraintAttribute), true))
				.Returns(new RouteConstraintAttribute[] { 
					new RouteConstraintAttribute { 
						Name = "Numeric", 
						Regex = "^[0-9]$" 
					} 
				});
			m_mockAction = mockAction.Object;

			m_routeAttribute = new RouteAttribute {
				Name = "HomeLink",
				Url = "home",
				Order = 99
			};
		}

		[TestMethod]
		public void ShouldParseControllerName()
		{
			RouteParser rp = new RouteParser(m_mockController, null, null);
			Assert.AreEqual(c_controllerName, rp.ControllerName);
		}

		[TestMethod]
		public void ShouldFailToParseInvalidControllerName()
		{
			Mock<Type> mockController = new Mock<Type>();
			mockController.Setup(x => x.Name).Returns(c_controllerName);

			try
			{
				RouteParser rp = new RouteParser(mockController.Object, null, null);
			}
			catch (ApplicationException)
			{
				// expected behaviour is for the ctor to throw because
				// the type name does end with "Controller"
				return;
			}
			Assert.Fail("RouteParser is not validating the controller name must end in \"Controller\"");
		}
	
		[TestMethod]
		public void ShouldParseActionName()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, null);
			Assert.AreEqual(c_actionName, rp.ActionName);
		}

		[TestMethod]
		public void ShouldParseRouteName()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, m_routeAttribute);
			Assert.AreEqual("HomeLink", rp.RouteName);
		}
	
		[TestMethod]
		public void ShouldParseOrder()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, m_routeAttribute);
			Assert.AreEqual(99, rp.Order);
		}

		[TestMethod]
		public void ShouldParseUrl()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, m_routeAttribute);
			Assert.AreEqual("home", rp.Url);
		}

		[TestMethod]
		public void ShouldParseEmptyUrlAsControllerAction()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, new RouteAttribute { Url = "" });
			Assert.AreEqual(String.Format("{0}/{1}", c_controllerName, c_actionName), rp.Url);
		}

		[TestMethod]
		public void ShouldOverrideUrlUsingIsRoot()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, new RouteAttribute { Url = "account/login", IsRoot = true });
			Assert.AreEqual("", rp.Url);
		}

		[TestMethod]
		public void ShouldFailToParseInvalidUrl()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, new RouteAttribute { Url = "/account/login" });

			try
			{
				string s = rp.Url;
			}
			catch (ApplicationException)
			{
				// expected behaviour is for the ctor to throw because
				// the type name does end with "Controller"
				return;
			}
			Assert.Fail("RouteParser is not validating the route attribute url properly");
		}

		[TestMethod]
		public void ShouldParseDefaults()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, null);

			RouteValueDictionary expected = new RouteValueDictionary(new { 
				controller = c_controllerName,
				action = c_actionName,
				Page = 1 }
			);

			RoutingAssert.AreDictionariesEqual(expected, rp.Defaults);
		}

		[TestMethod]
		public void ShouldOverrideDefaultControllerAndActionNames()
		{
			Mock<MethodInfo> mockAction = new Mock<MethodInfo>();
			mockAction
				.Setup(x => x.Name)
				.Returns(c_actionName);
			mockAction
				.Setup(x => x.DeclaringType)
				.Returns(m_mockController);
			mockAction
				.Setup(x => x.GetCustomAttributes(typeof(RouteDefaultAttribute), true))
				.Returns(new RouteDefaultAttribute[] { 
					new RouteDefaultAttribute { 
						Name = "controller", 
						Value = "plibble"
					},
					new RouteDefaultAttribute { 
						Name = "Page", 
						Value = 1 
					} 
				});

			RouteParser rp = new RouteParser(m_mockController, mockAction.Object, null);

			RouteValueDictionary expected = new RouteValueDictionary(new {
				controller = "plibble",
				action = c_actionName,
				Page = 1
			});

			RoutingAssert.AreDictionariesEqual(expected, rp.Defaults);
		}

		[TestMethod]
		public void ShouldParseConstraints()
		{
			RouteParser rp = new RouteParser(m_mockController, m_mockAction, null);

			RouteValueDictionary expected = new RouteValueDictionary(new {
				Numeric = "^[0-9]$"
			});

			RoutingAssert.AreDictionariesEqual(expected, rp.Constraints);
		}
	}
}
