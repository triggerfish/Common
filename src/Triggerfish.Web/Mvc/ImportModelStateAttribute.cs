using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Attribute to import data from TempData into ViewData.ModelState
	/// </summary>
	public class ImportModelStateAttribute : ImportFromTempDataAttribute
	{
		/// <summary>
		/// Constuctor
		/// </summary>
		/// <param name="key">The key with which to store data in TempData</param>
		public ImportModelStateAttribute(string key) : base(key, typeof(ModelStateDictionary)) { }

		/// <summary>
		/// Abstract method implementation to set the object in ViewData.ModelState
		/// </summary>
		/// <param name="model">The ViewData.ModelState object</param>
		/// <param name="controller">The controller object</param>
		protected override void SetModel(object model, ControllerBase controller)
		{
			controller.ViewData.ModelState.Merge(model as ModelStateDictionary);
		}
	}
}
