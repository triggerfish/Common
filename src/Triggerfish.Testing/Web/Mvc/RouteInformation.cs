﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Reflection;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc.Testing
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
		/// <param name="a_relativeUrl">The relative url for which to get the route information</param>
		/// <param name="a_registerRoutes">Delegate instance to register routes</param>
		/// <returns>A new RouteInformation object for the url</returns>
		public static RouteInformation Create(string a_relativeUrl, Action<RouteCollection> a_registerRoutes)
		{
			return new RouteInformation(a_relativeUrl, a_registerRoutes);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="a_relativeUrl">The relative url for which to get the route information</param>
		/// <param name="a_registerRoutes">Delegate instance to register routes</param>
		public RouteInformation(string a_relativeUrl, Action<RouteCollection> a_registerRoutes)
		{
			if (String.IsNullOrEmpty(a_relativeUrl))
				return;

			Url = a_relativeUrl;

			// make sure the url conforms to expected relative format
			string url = a_relativeUrl;
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
			try
			{
				RouteValues = InboundRoutingHelpers.GenerateInboundRoute(url, a_registerRoutes);
			}
			catch (Exception)
			{
			}
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
						AuthorizeAttribute[] attrs = (AuthorizeAttribute[])mi.GetCustomAttributes(typeof(AuthorizeAttribute), false);
						return attrs.Length == 1;
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
		/// <param name="a_controller">The name of the controller</param>
		/// <returns>true if the action method is in the controller class, false otherwise</returns>
		public bool IsActionOnController(string a_controller)
		{
			int i = a_controller.IndexOf("Controller");
			if (i != -1)
			{
				a_controller = a_controller.Substring(0, i);
			}
			return 0 == String.Compare(a_controller, Controller, false);
		}

		private string GetRouteValue(string a_key)
		{
			if (RouteValues != null)
			{
				return (string)RouteValues[a_key];
			}
			return null;
		}
	}
}
