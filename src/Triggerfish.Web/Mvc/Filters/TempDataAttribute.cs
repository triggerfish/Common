using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Base class for attributes that import/export data from/to TempData
	/// </summary>
	public abstract class TempDataAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// The key with which to store and retrieve data in TempData
		/// </summary>
		protected string Key { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="key">The TempData key</param>
		protected TempDataAttribute(string key)
		{
			Key = key;
		}

		/// <summary>
		/// Get the data object from TempData
		/// </summary>
		/// <param name="filterContext">The context object that contains a reference to TempData</param>
		/// <returns>The object</returns>
		public object Get(ControllerContext filterContext)
		{
			if (filterContext.Controller.TempData.ContainsKey(Key))
				return filterContext.Controller.TempData[Key];

			return null;
		}

		/// <summary>
		/// Sets the data object into TempData
		/// </summary>
		/// <param name="filterContext">The context object that contains a reference to TempData</param>
		/// <param name="obj">The object to set</param>
		public void Set(ControllerContext filterContext, object obj)
		{
			filterContext.Controller.TempData[Key] = obj;
		}
	
		/// <summary>
		/// Removes the data object from TempData
		/// </summary>
		/// <param name="filterContext">The context object that contains a reference to TempData</param>
		public void Remove(ControllerContext filterContext)
		{
			filterContext.Controller.TempData.Remove(Key);
		}
	}
}
