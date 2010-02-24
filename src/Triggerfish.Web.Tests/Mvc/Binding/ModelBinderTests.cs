using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Globalization;
using Triggerfish.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class ModelBinderTests
	{
		private ModelBindingContext m_ctx = new ModelBindingContext();

		[TestMethod]
		public void ShouldGetPrefixedKey()
		{
			// arrange
			m_ctx.ValueProvider.Add("credentials.Email", new ValueProviderResult("1@2.com", "1@2.com", CultureInfo.CurrentCulture));

			ModelBinder mb = new ModelBinder { Key = "Email", MustHave = false };

			// act
			string str = (string)mb.BindModel(null, m_ctx);

			// assert
			Assert.AreEqual("1@2.com", str);
		}
	
		[TestMethod]
		public void ShouldGetNonPrefixedKey()
		{
			// arrange
			m_ctx.FallbackToEmptyPrefix = true;
			m_ctx.ValueProvider.Add("Email", new ValueProviderResult("1@2.com", "1@2.com", CultureInfo.CurrentCulture));

			ModelBinder mb = new ModelBinder { Key = "Email", MustHave = false };

			// act
			string str = (string)mb.BindModel(null, m_ctx);

			// assert
			Assert.AreEqual("1@2.com", str);
		}

		[TestMethod]
		public void ShouldNotGetNonPrefixedKeyIfFallbackIsFalse()
		{
			// arrange
			m_ctx.FallbackToEmptyPrefix = false;
			m_ctx.ValueProvider.Add("Email", new ValueProviderResult("1@2.com", "1@2.com", CultureInfo.CurrentCulture));

			ModelBinder mb = new ModelBinder { Key = "Email", MustHave = false };

			// act
			string str = (string)mb.BindModel(null, m_ctx);

			// assert
			Assert.AreEqual(null, str);
		}

		[TestMethod]
		public void ShouldAddToModelState()
		{
			// arrange
			m_ctx.ValueProvider.Add("credentials.Email", new ValueProviderResult("1@2.com", "1@2.com", CultureInfo.CurrentCulture));

			ModelBinder mb = new ModelBinder { Key = "Email", MustHave = false };

			// act
			mb.BindModel(null, m_ctx);

			// assert
			Assert.IsTrue(m_ctx.ModelState.ContainsKey("credentials.Email"));
			Assert.AreEqual(0, m_ctx.ModelState["credentials.Email"].Errors.Count);
		}

		[TestMethod]
		public void ShouldNotAddToModelStateIfFailed()
		{
			// arrange
			m_ctx.ValueProvider.Add("credentials.User", new ValueProviderResult("1@2.com", "1@2.com", CultureInfo.CurrentCulture));

			ModelBinder mb = new ModelBinder { Key = "Email", MustHave = false };

			// act
			mb.BindModel(null, m_ctx);

			// assert
			Assert.IsFalse(m_ctx.ModelState.ContainsKey("credentials.Email"));
		}

		[TestMethod]
		public void ShouldAddErrorIfKeyCannotBeFound()
		{
			// arrange
			m_ctx.ValueProvider.Add("credentials.User", new ValueProviderResult("1@2.com", "1@2.com", CultureInfo.CurrentCulture));

			ModelBinder mb = new ModelBinder { Key = "Email", MustHave = true };

			// act
			mb.BindModel(null, m_ctx);

			// assert
			Assert.IsTrue(m_ctx.ModelState.ContainsKey("Email"));
			Assert.AreEqual(1, m_ctx.ModelState["Email"].Errors.Count);
		}

		[TestInitialize]
		public void SetupTest()
		{
			m_ctx.ModelName = "credentials";
			m_ctx.ValueProvider = new Dictionary<string, ValueProviderResult>();
		}
	}

	internal class ModelBinder : ModelBinder<string>
	{
		public string Key { get; set; }
		public bool MustHave { get; set; }

		protected override object Bind()
		{
			return GetValue(Key, MustHave);
		}
	}
}
