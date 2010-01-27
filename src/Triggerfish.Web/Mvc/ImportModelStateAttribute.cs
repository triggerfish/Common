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
		/// <param name="a_key">The key with which to store data in TempData</param>
		public ImportModelStateAttribute(string a_key) : base(a_key, typeof(ModelStateDictionary)) { }

		/// <summary>
		/// Abstract method implementation to set the object in ViewData.ModelState
		/// </summary>
		/// <param name="a_model">The ViewData.ModelState object</param>
		/// <param name="a_controller">The controller object</param>
		protected override void SetModel(object a_model, ControllerBase a_controller)
		{
			a_controller.ViewData.ModelState.Merge(a_model as ModelStateDictionary);
		}
	}
}
