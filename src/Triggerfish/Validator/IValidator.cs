using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Triggerfish.Validator
{
	/// <summary>
	/// Interface for a validator
	/// </summary>
	public interface IValidator
	{
		/// <summary>
		/// Validate the specified object. Should throw a ValidationException if the 
		/// object is invalid
		/// </summary>
		/// <param name="obj">The object to validate</param>
		void Validate(object obj);
	}
}
