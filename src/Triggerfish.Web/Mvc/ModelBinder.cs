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
		private ModelBindingContext m_context;

		/// <summary>
		/// The name of the data model
		/// </summary>
		protected string ModelName { get { return m_context.ModelName; } }

		/// <summary>
		/// Bind the incoming data to the model object. IModelBinder implementation
		/// </summary>
		/// <param name="controllerContext">The controller context</param>
		/// <param name="bindingContext">The binding context</param>
		/// <returns>The object</returns>
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			// The action method argument
			m_context = bindingContext;

			try
			{
				object obj = Bind();

				if (null != obj)
				{
					Validate(obj);
				}

				return obj;
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
		/// Validate the object against the project validation strategy
		/// </summary>
		/// <param name="obj"></param>
		protected abstract void Validate(object obj);

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
			string k = m_context.ModelName + "." + key;
			bool gotIt = m_context.ValueProvider.TryGetValue(k, out v);

			// if that failed, try with the raw name (unless the model has the Bind(Prefix = XXX) attribute specified)
			if (!gotIt && m_context.FallbackToEmptyPrefix)
			{
				k = key;
				gotIt = m_context.ValueProvider.TryGetValue(k, out v);
			}

			if (gotIt)
			{
				m_context.ModelState.SetModelValue(k, v);

				if (!mustHaveValue || !String.IsNullOrEmpty(v.AttemptedValue))
				{
					return v.AttemptedValue;
				}
			}

			if (mustHaveValue)
			{
				throw new ValidationException(key, String.Format("{0} must be specified", key));
			}

			return null;
		}
	}
}
