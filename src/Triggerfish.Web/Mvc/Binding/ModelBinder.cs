using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Abstract base class for a custom module binder
	/// </summary>
	/// <typeparam name="T">The type of data to bind</typeparam>
	public abstract class ModelBinder<T> : IModelBinder where T : class
	{
		/// <summary>
		/// The model binding context
		/// </summary>
		protected ControllerContext ControllerContext { get; private set; }

		/// <summary>
		/// The model binding context
		/// </summary>
		protected ModelBindingContext BindingContext { get; private set; }

		/// <summary>
		/// The name of the data model
		/// </summary>
		protected string ModelName { get { return BindingContext.ModelName; } }

		/// <summary>
		/// Bind the incoming data to the model object. IModelBinder implementation
		/// </summary>
		/// <param name="controllerContext">The controller context</param>
		/// <param name="bindingContext">The binding context</param>
		/// <returns>The object</returns>
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			ControllerContext = controllerContext;
			BindingContext = bindingContext;

			try
			{
				return Bind();
			}
			catch (ValidationException ex)
			{
				ex.ToModelErrors(bindingContext.ModelState, "");
			}

			return null;
		}

		/// <summary>
		/// Perform specific input data parsing and object constructin
		/// </summary>
		/// <returns>The model object</returns>
		protected abstract object Bind();

		/// <summary>
		/// Helper method to retrieve from the binding context a data value
		/// Handles both prefixed and non-prefixed model names
		/// </summary>
		/// <param name="key">The data value key (name)</param>
		/// <param name="mustHaveValue">True if the value must be present (a ValidationException is thrown if the value is not present), false otherwise</param>
		/// <returns>The data value as a string</returns>
		protected string GetValue(string key, bool mustHaveValue)
		{
			ValueProviderResult v;

			// first try with the prefix
			string k = BindingContext.ModelName + "." + key;
			bool gotIt = BindingContext.ValueProvider.TryGetValue(k, out v);

			// if that failed, try with the raw name (unless the model has the Bind(Prefix = XXX) attribute specified)
			if (!gotIt && BindingContext.FallbackToEmptyPrefix)
			{
				k = key;
				gotIt = BindingContext.ValueProvider.TryGetValue(k, out v);
			}

			if (gotIt)
			{
				BindingContext.ModelState.SetModelValue(k, v);

				if (!mustHaveValue || !String.IsNullOrEmpty(v.AttemptedValue))
				{
					return v.AttemptedValue;
				}
			}

			if (mustHaveValue)
			{
				throw new ValidationException(key, "This field is required");
			}

			return null;
		}
	}
}
