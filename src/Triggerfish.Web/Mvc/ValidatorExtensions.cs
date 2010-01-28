using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Extension methods for validator objects
	/// </summary>
	public static class ValidationExtensions
	{
		/// <summary>
		/// Copies the errors in a ValidationException to a ModelStateDictionary
		/// </summary>
		/// <param name="exception">The exception containing the error data</param>
		/// <param name="dictionary">The ModelStateDictionary to receive a copy of the errors</param>
		/// <param name="prefix">The prefix for the form controls where the errors occurred</param>
		public static void ToModelErrors(this ValidationException exception, ModelStateDictionary dictionary, string prefix)
		{
			if (!String.IsNullOrEmpty(prefix))
			{
				prefix = prefix + ".";
			}

			foreach (string key in exception.Errors)
			{
				foreach (string value in exception.Errors.GetValues(key))
				{
					dictionary.AddModelError(prefix + key, value);
				}
			}
		}
	}
}
