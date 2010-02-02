using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Validator.Exceptions;
using NHibernate.Validator.Engine;
using Triggerfish.Validator;
using Triggerfish.NHibernate.Validator;

namespace Triggerfish.NHibernate.Tests.Validator
{
	[TestClass]
	public class ValidatorExtensionsTests
	{
		[TestMethod]
		public void ShouldCreateValidationException()
		{
			// arrange
			InvalidValue[] errors = new InvalidValue[] { new InvalidValue("Error", typeof(string), "Property", "this", "that", null) };

			// act
			ValidationException ex = errors.ToValidationException();

			// assert
			Assert.IsTrue(ex.Errors.AllKeys.Contains("Property"));
			string[] messages = ex.Errors.GetValues("Property");
			Assert.AreEqual(1, messages.Length);
			Assert.AreEqual("Error", messages[0]);
		}
	
		[TestMethod]
		public void ShouldNotCreateValidationExceptionIfNoErrors()
		{
			// arrange
			InvalidValue[] errors = new InvalidValue[] {};

			// act
			ValidationException ex = errors.ToValidationException();

			// assert
			Assert.AreEqual(null, ex);
		}
	}
}
