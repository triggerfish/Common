using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Exports the ViewData.Model object to TempData
	/// </summary>
	public class ExportModelAttribute : ExportToTempDataAttribute
	{
		/// <summary>
		/// The type of the data object stored in ViewData.Model
		/// </summary>
		public Type ModelType { get; private set; }

		/// <summary>
		/// Constuctor
		/// </summary>
		/// <param name="a_key">The key with which to store data in TempData</param>
		/// <param name="a_modelType">The type of data object in ViewData.Model</param>
		public ExportModelAttribute(string a_key, Type a_modelType)
			: base(a_key, EExportWhen.ModelStateValid)
		{
			ModelType = a_modelType;
		}

		/// <summary>
		/// Abstract method implementation to get the object in ViewData.Model
		/// </summary>
		/// <param name="a_controller">The controller object</param>
		/// <returns>The ViewData.Model object</returns>
		protected override object GetModel(ControllerBase a_controller)
		{
			object model = a_controller.ViewData.Model;
			if (null != model && model.GetType() == ModelType)
			{
				return model;
			}
			return null;
		}
	}
}
