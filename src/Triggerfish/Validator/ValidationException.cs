using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Triggerfish.Validator
{
	/// <summary>
	/// Generic class for handling validation exceptions
	/// </summary>
	public class ValidationException : Exception
	{
		/// <summary>
		/// A collection of errors. Key is the name of the source of the error
		/// and the value is the error message
		/// </summary>
		public NameValueCollection Errors { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public ValidationException()
		{
			Errors = new NameValueCollection();
		}
	
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="key">The source of the error</param>
		/// <param name="value">The error message</param>
		public ValidationException(string key, string value)
			: this()
		{
			Errors.Add(key, value);
		}
	}
}
