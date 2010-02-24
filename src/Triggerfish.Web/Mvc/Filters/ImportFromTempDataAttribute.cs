using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Abstract class to import data from TempData
	/// </summary>
	public abstract class ImportFromTempDataAttribute : TempDataAttribute
	{
		/// <summary>
		/// The type of data to import
		/// </summary>
		protected Type ModelType { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="key">The key with which to store data in TempData</param>
		/// <param name="modelType">The type of data to import</param>
		protected ImportFromTempDataAttribute(string key, Type modelType)
			: base(key)
		{
			ModelType = modelType;
		}

		/// <summary>
		/// Called befre the action method executes. Perform the import of data
		/// from TempData
		/// </summary>
		/// <param name="filterContext">The filter context</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			object model = Get(filterContext);

			if (model != null && model.GetType() == ModelType)
			{
				SetModel(model, filterContext.Controller);
			}
			else
			{
				Remove(filterContext);
			}

			base.OnActionExecuting(filterContext);
		}

		/// <summary>
		/// Abstract method to set the actual data object into the Controller object
		/// </summary>
		/// <param name="model">The object imported from TempData</param>
		/// <param name="controller">The controller object</param>
		protected abstract void SetModel(object model, ControllerBase controller);
	}
}
