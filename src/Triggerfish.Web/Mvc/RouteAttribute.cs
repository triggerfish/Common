using System;

namespace Triggerfish.Web.Mvc
{
    /// <summary>
    /// Assigns a URL route to an MVC Controller class method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// Optional name of the route.  Route names must be unique per route.
        /// </summary>
        public string Name { get; set; }

		/// <summary>
		/// Optional specifier of whether or not the route is for the site 
		/// root url (~/). If equal to true the Path property is ignored
		/// </summary>
		public bool IsRoot { get; set; }

        /// <summary>
        /// Path of the URL route.  This is relative to the root of the web site.
        /// Do not append a "/" prefix. If not specified (null or empty) the default
		/// controller/action url is created
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Optional order in which to add the route (default is 0).  Routes
        /// with lower order values will be added before those with higher.
        /// Routes that have the same order value will be added in undefined
        /// order with respect to each other.
        /// </summary>
        public int Order { get; set; }
    }
}
