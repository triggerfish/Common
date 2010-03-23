using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Reflection;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Helper class to lookup a route based on a string url 
	/// </summary>
	public class RouteInformation
	{
		/// <summary>
		/// Property to get the Url with which this object was constructed
		/// </summary>
		public string Url { get; private set; }

		/// <summary>
		/// Test whether or not the url specifies a valid route
		/// </summary>
		public bool Valid { get { return null != RouteValues; } }

		/// <summary>
		/// Get the RouteDataDictionary containing the route values
		/// </summary>
		public RouteValueDictionary RouteValues { get; private set; }

		/// <summary>
		/// Gets the name of the controller to which this route belongs
		/// </summary>
		/// <value>
		/// The returned value excludes the Controller suffix: for example, it will return 
		/// "Authentication", not "AuthenticationController"
		/// </value>
		public string Controller { get { return GetRouteValue("controller"); } }

		/// <summary>
		/// Gets the name of the action invoked by this route
		/// </summary>
		public string Action { get { return GetRouteValue("action"); } }

		/// <summary>
		/// Static creation method
		/// </summary>
		/// <param name="relativeUrl">The relative url for which to get the route information</param>
		/// <param name="registerRoutes">Delegate instance to register routes</param>
		/// <returns>A new RouteInformation object for the url</returns>
		public static RouteInformation Create(string relativeUrl, Action<RouteCollection> registerRoutes)
		{
			return new RouteInformation(relativeUrl, registerRoutes);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="relativeUrl">The relative url for which to get the route information</param>
		/// <param name="registerRoutes">Delegate instance to register routes</param>
		public RouteInformation(string relativeUrl, Action<RouteCollection> registerRoutes)
		{
			if (String.IsNullOrEmpty(relativeUrl))
				return;

			Url = relativeUrl;

			// make sure the url conforms to expected relative format
			string url = relativeUrl;
			if (0 != url.IndexOf("~/"))
			{
				if (url[0] != '/')
				{
					url = "~/" + url;
				}
				else
				{
					url = "~" + url;
				}
			}

			RouteCollection routes = new RouteCollection();
			RouteValues = GenerateInboundRoute(url, registerRoutes);
		}

		/// <summary>
		/// Tests whether the route action requires authorization to be invoked, i.e. it is 
		/// decorated with the AuthorizeAttribute
		/// </summary>
		/// <returns>true if the action requires authorisation, false otherwise</returns>
		public bool DoesActionRequireAuthorisation()
		{
			return DoesActionRequireAuthorisation(Assembly.GetCallingAssembly());
		}

		/// <summary>
		/// Tests whether the route action requires authorization to be invoked, i.e. it is 
		/// decorated with the AuthorizeAttribute
		/// </summary>
		/// <param name="callingAssembly">The calling assembly that contains the controller class</param>
		/// <returns>true if the action requires authorisation, false otherwise</returns>
		public bool DoesActionRequireAuthorisation(Assembly callingAssembly)
		{
			string controller = Controller + "Controller";
			string action = Action;

			try
			{
				Type tc = callingAssembly.GetTypes().FirstOrDefault(t => t.Name == controller);
				if (null != tc)
				{
					MethodInfo mi = tc.GetMethod(action);

					if (null != mi)
					{
						AuthorizeAttribute[] microsoftAttrs = (AuthorizeAttribute[])mi.GetCustomAttributes(typeof(AuthorizeAttribute), false);
						AuthoriseAttribute[] triggerfishAttrs = (AuthoriseAttribute[])mi.GetCustomAttributes(typeof(AuthoriseAttribute), false);
						return (microsoftAttrs.Length > 0) || (triggerfishAttrs.Length > 0);
					}
				}
			}
			catch (Exception)
			{
			}

			return false;
		}

		/// <summary>
		/// Tests whether or not the action method is on the specified controller class
		/// </summary>
		/// <param name="controller">The name of the controller</param>
		/// <returns>true if the action method is in the controller class, false otherwise</returns>
		public bool IsActionOnController(string controller)
		{
			int i = controller.IndexOf("Controller");
			if (i != -1)
			{
				controller = controller.Substring(0, i);
			}
			return 0 == String.Compare(controller, Controller, false);
		}

		private string GetRouteValue(string key)
		{
			if (RouteValues != null)
			{
				return (string)RouteValues[key];
			}
			return null;
		}

		private RouteValueDictionary GenerateInboundRoute(string url, Action<RouteCollection> registerRoutes)
		{
			RouteCollection routes = new RouteCollection();
			registerRoutes(routes);

			HttpContextBase mockHttp = new FakeHttpContextBase(new FakeHttpRequestBase(url), new FakeHttpResponseBase());

			RouteData data = routes.GetRouteData(mockHttp);
			if (null != data)
			{
				return data.Values;
			}

			return null;
		}

		private class FakeHttpContextBase : HttpContextBase
		{
			private HttpRequestBase m_request;
			private HttpResponseBase m_response;

			public FakeHttpContextBase(HttpRequestBase request, HttpResponseBase response) : base() { m_request = request; m_response = response; }

			public override HttpRequestBase Request { get { return m_request; } }
		}

		private class FakeHttpRequestBase : HttpRequestBase
		{
			private string m_url;

			public FakeHttpRequestBase(string url) : base() { m_url = url; }

			public override string AppRelativeCurrentExecutionFilePath { get { return m_url; } }
			public override string PathInfo { get { return ""; } }
		}

		private class FakeHttpResponseBase : HttpResponseBase
		{
			public FakeHttpResponseBase() : base() { }

			public override string ApplyAppPathModifier(string virtualPath)
			{
				return virtualPath;
			}
		}
	}
}
