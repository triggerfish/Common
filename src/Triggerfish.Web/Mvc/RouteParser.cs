using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Routing;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Class to parse route information from controller methods decorated with
	/// RouteAttribute attributes
	/// </summary>
	public class RouteParser
	{
		private Type m_controller;
		private MethodInfo m_action;
		private RouteAttribute m_routeAttribute;

		/// <summary>
		/// Property to get the name of the controller
		/// </summary>
		public string ControllerName { get; private set; }

		/// <summary>
		/// Property to get the name of the action on the controller
		/// </summary>
		public string ActionName
		{
			get
			{
				return m_action.Name;
			}
		}

		/// <summary>
		/// Property to get the name of the route
		/// </summary>
		public string RouteName
		{
			get
			{
				return String.IsNullOrEmpty(m_routeAttribute.Name) ? null : m_routeAttribute.Name;
			}
		}

		/// <summary>
		/// Property to get the order of the route in the route collection
		/// </summary>
		public int Order
		{
			get
			{
				return m_routeAttribute.Order;
			}
		}

		/// <summary>
		/// Property to get the url of the route
		/// </summary>
		public string Url
		{
			get
			{
				string url = m_routeAttribute.Url;

				// force the url to empty if IsRoot specified
				if (m_routeAttribute.IsRoot)
				{
					url = "";
				}
				// if no url is specified default to controller/action
				else if (String.IsNullOrEmpty(url))
				{
					url = String.Format("{0}/{1}", ControllerName, ActionName);
				}

				// validate url parameter
				if (url.StartsWith("/") || url.Contains("?"))
				{
					throw new ApplicationException(String.Format(
						"Invalid UrlRoute attribute \"{0}\" on method {1}.{2}: Url cannot start with \"/\" or contain \"?\".",
						url, ControllerName, ActionName));
				}

				return url;
			}
		}

		/// <summary>
		/// Property to get the route default values
		/// </summary>
		public RouteValueDictionary Defaults
		{
			get
			{
				RouteValueDictionary defaults = new RouteValueDictionary();
				defaults["controller"] = ControllerName;
				defaults["action"] = ActionName;

				foreach (RouteDefaultAttribute attrib in m_action.GetCustomAttributes(typeof(RouteDefaultAttribute), true))
				{
					if (String.IsNullOrEmpty(attrib.Name))
					{
						throw new ApplicationException(String.Format("UrlRouteParameterDefault attribute on {0}.{1} is missing the Name property.",
							m_action.DeclaringType.Name, ActionName));
					}

					if (attrib.Value == null)
					{
						throw new ApplicationException(String.Format("UrlRouteParameterDefault attribute on {0}.{1} is missing the Value property.",
							m_action.DeclaringType.Name, ActionName));
					}

					defaults[attrib.Name] = attrib.Value;
				}

				return defaults;
			}
		}

		/// <summary>
		/// Property to get the route constraints
		/// </summary>
		public RouteValueDictionary Constraints
		{
			get
			{
				RouteValueDictionary constraints = new RouteValueDictionary();
				foreach (RouteConstraintAttribute attrib in m_action.GetCustomAttributes(typeof(RouteConstraintAttribute), true))
				{
					if (String.IsNullOrEmpty(attrib.Name))
					{
						throw new ApplicationException(String.Format("UrlRouteParameterContraint attribute on {0}.{1} is missing the Name property.",
							m_action.DeclaringType.Name, ActionName));
					}

					if (String.IsNullOrEmpty(attrib.Regex))
					{
						throw new ApplicationException(String.Format("UrlRouteParameterContraint attribute on {0}.{1} is missing the RegEx property.",
							m_action.DeclaringType.Name, ActionName));
					}

					constraints[attrib.Name] = attrib.Regex;
				}
				return constraints;
			}
		}

		/// <summary>
		/// Constructs a route parser
		/// </summary>
		/// <param name="a_controller">The type of the controller class</param>
		/// <param name="a_action">The action method on the controller</param>
		/// <param name="a_route">The route attribute applied to the action method</param>
		public RouteParser(Type a_controller, MethodInfo a_action, RouteAttribute a_route)
		{
			m_controller = a_controller;
			m_action = a_action;
			m_routeAttribute = a_route;

			string controllerName = m_controller.Name;

			if (!controllerName.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase))
			{
				// MVC requires a controller name
				throw new ApplicationException(String.Format(
					"Invalid controller class name {0}: name must end with \"Controller\"",
					controllerName));
			}

			// MVC ignoes the Controller part of the class name 
			ControllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
		}

		/// <summary>
		/// Static method to create a list of RouteParser objects from all the controllers
		/// in the specified assembly. RouteParser objects are only created where a controller method 
		/// is decorated with a RouteAttribute
		/// </summary>
		/// <param name="a_assembly">The assembly to scan</param>
		/// <returns>List of RouteParser objects</returns>
		public static List<RouteParser> CreateFromAttributes(Assembly a_assembly)
		{
			List<RouteParser> parsers = new List<RouteParser>();
			IEnumerable<Type> controllerClasses =
				from t in a_assembly.GetTypes()
				where t.IsClass && t.IsSubclassOf(typeof(System.Web.Mvc.Controller))
				select t;

			foreach (Type c in controllerClasses)
			{
				foreach (MethodInfo a in c.GetMethods())
				{
					foreach (RouteAttribute r in a.GetCustomAttributes(typeof(RouteAttribute), false))
					{
						parsers.Add(new RouteParser(c, a, r));
					}
				}
			}

			return parsers;
		}
	}
}
