using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;

namespace Triggerfish.Web.Mvc.Testing
{
	/// <summary>
	/// Binder helper methods
	/// </summary>
	public static class BinderHelpers
	{
		/// <summary>
		/// Creates a default model binding context object
		/// </summary>
		/// <param name="argName">The action method argument name</param>
		/// <returns>A new ModelBindingContext object</returns>
		public static ModelBindingContext CreateModelBindingContext(string argName)
		{
			return new ModelBindingContext() {
				FallbackToEmptyPrefix = true,
				ModelName = argName,
				ModelState = new ModelStateDictionary(),
				ValueProvider = new Dictionary<string, ValueProviderResult>(),
			};
		}

		/// <summary>
		/// Creates a default model binding context object
		/// </summary>
		/// <param name="argName">The action method argument name</param>
		/// <param name="argValues">Dictionary of context name-value pairs</param>
		/// <returns>A new ModelBindingContext object</returns>
		public static ModelBindingContext CreateModelBindingContext(string argName, IDictionary<string, string> argValues)
		{
			ModelBindingContext ctx = CreateModelBindingContext(argName);
			foreach (KeyValuePair<string, string> kvp in argValues)
			{
				ctx.ValueProvider.Add(kvp.Key, new ValueProviderResult(kvp.Value, kvp.Value, CultureInfo.CurrentCulture));
			}
			return ctx;
		}
	}
}
