using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Attribute to import data from TempData into ViewData.Model
	/// </summary>
	public class ImportModelAttribute : ImportFromTempDataAttribute
	{
		/// <summary>
		/// Constuctor
		/// </summary>
		/// <param name="key">The key with which to store data in TempData</param>
		/// <param name="modelType">The type of data object in ViewData.Model</param>
		public ImportModelAttribute(string key, Type modelType)
			: base(key, modelType)
		{
		}

		/// <summary>
		/// Abstract method implementation to set the object in ViewData.Model
		/// </summary>
		/// <param name="model">The ViewData.Model object</param>
		/// <param name="controller">The controller object</param>
		protected override void SetModel(object model, ControllerBase controller)
		{
			controller.ViewData.Model = model;
		}
	}
}
