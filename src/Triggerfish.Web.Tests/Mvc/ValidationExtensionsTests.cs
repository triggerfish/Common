using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Mvc;
using Triggerfish.Web.Routing;
using System.Collections.Specialized;
using Triggerfish.Validator;
using System.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class ValidationExtensionsTests
	{
		[TestMethod]
		public void ShouldCopyAllErrors()
		{
			// arrange
			NameValueCollection nvc = new NameValueCollection();
			nvc.Add("Email", "Invalid");
			nvc.Add("Email", "Mising");
			nvc.Add("Password", "Mising");

			ValidationException ex = new ValidationException { Errors = nvc };
			ModelStateDictionary modelState = new ModelStateDictionary();

			// act
			ex.ToModelErrors(modelState, null);

			// assert
			Assert.AreEqual(2, modelState["Email"].Errors.Count);
			Assert.AreEqual(1, modelState["Password"].Errors.Count);
		}
	
		[TestMethod]
		public void ShouldCopyAllErrorsWithPrefix()
		{
			// arrange
			NameValueCollection nvc = new NameValueCollection();
			nvc.Add("Email", "Invalid");
			nvc.Add("Email", "Mising");
			nvc.Add("Password", "Mising");

			ValidationException ex = new ValidationException { Errors = nvc };
			ModelStateDictionary modelState = new ModelStateDictionary();

			// act
			ex.ToModelErrors(modelState, "credentials");

			// assert
			Assert.IsFalse(modelState.ContainsKey("Email"));
			Assert.IsFalse(modelState.ContainsKey("Password"));
			Assert.AreEqual(2, modelState["credentials.Email"].Errors.Count);
			Assert.AreEqual(1, modelState["credentials.Password"].Errors.Count);
		}
	}
}
