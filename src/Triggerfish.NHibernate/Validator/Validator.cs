﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Validator.Engine;
using Triggerfish.Validator;

namespace Triggerfish.NHibernate.Validator
{
	/// <summary>
	/// Invokes the NHibernate.Validator validation runner
	/// </summary>
	public class Validator : Triggerfish.Validator.IValidator
	{
		private ValidatorEngine m_engine;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="engine">The NHibernate.Validator validation engine</param>
		public Validator(ValidatorEngine engine)
		{
			m_engine = engine;
		}

		/// <summary>
		/// Validate the specified object. Should throw a ValidationException if the 
		/// object is invalid
		/// </summary>
		/// <param name="obj">The object to validate</param>
		public void Validate(object obj)
		{
			InvalidValue[] errors = m_engine.Validate(obj);
			if (null != errors && errors.Length > 0)
			{
				ValidationException ex = new ValidationException();

				foreach (InvalidValue val in errors)
				{
					ex.Errors.Add(val.PropertyName, val.Message);
				}

				throw ex;
			}
		}
	}
}