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
		/// <param name="a_key">The key with which to store data in TempData</param>
		/// <param name="a_modelType">The type of data object in ViewData.Model</param>
		public ImportModelAttribute(string a_key, Type a_modelType)
			: base(a_key, a_modelType)
		{
		}

		/// <summary>
		/// Abstract method implementation to set the object in ViewData.Model
		/// </summary>
		/// <param name="a_model">The ViewData.Model object</param>
		/// <param name="a_controller">The controller object</param>
		protected override void SetModel(object a_model, ControllerBase a_controller)
		{
			a_controller.ViewData.Model = a_model;
		}
	}
}
