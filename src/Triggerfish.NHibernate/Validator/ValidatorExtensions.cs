using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Triggerfish.Validator;
using NHibernate.Validator.Exceptions;
using NHibernate.Validator.Engine;

namespace Triggerfish.NHibernate.Validator
{
	/// <summary>
	/// Extension methods for validation errors
	/// </summary>
	public static class ValidatorExtensions
	{
		/// <summary>
		/// Constructs a ValidationException based on a NHibernate.Validator InvalidStateException
		/// </summary>
		/// <param name="exception">The exception received from NHibernate.Validator</param>
		/// <returns>A ValidationException object</returns>
		public static ValidationException ToValidationException(this InvalidStateException exception)
		{
			return exception.GetInvalidValues().ToValidationException();
		}

		/// <summary>
		/// Constructs a ValidationException based on NHibernate.Validator InvalidValue objects.
		/// </summary>
		/// <param name="errors">The errors received from NHibernate.Validator</param>
		/// <returns>A ValidationException or null if there are no errors</returns>
		public static ValidationException ToValidationException(this InvalidValue[] errors)
		{
			if (null != errors && errors.Length > 0)
			{
				ValidationException ex = new ValidationException();

				foreach (InvalidValue val in errors)
				{
					ex.Errors.Add(val.PropertyName, val.Message);
				}

				return ex;
			}

			return null;
		}
	}
}
