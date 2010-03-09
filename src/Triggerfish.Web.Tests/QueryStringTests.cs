using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Triggerfish.Web.Mvc;
using System.Web.Routing;
using Triggerfish.Web.Routing.Testing;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class QueryStringTests
	{
		[TestMethod]
		public void ShouldBuildHttpGetString()
		{
			// arrange
			QueryString qs = new QueryString();
			qs.Add("1", "2");
			qs.Add("3", "4");

			// act and assert
			Assert.AreEqual("?1=2&3=4", qs.ToString());
		}

		[TestMethod]
		public void ShouldBuildHttpPostString()
		{
			// arrange
			QueryString qs = new QueryString();
			qs.Add("1", "2");
			qs.Add("3", "4");

			// act and assert
			Assert.AreEqual("1=2&3=4", qs.ToString(HttpAction.Post));
		}

		[TestMethod]
		public void ShouldNotEncodeWhenAdding()
		{
			// arrange
			QueryString qs = new QueryString();
			qs.Add("1", "http://");

			// act and assert
			Assert.AreEqual("http://", qs["1"]);
		}
	
		[TestMethod]
		public void ShouldEncodeWhenConvertingToString()
		{
			// arrange
			QueryString qs = new QueryString();
			qs.Add("1", "http://");

			// act and assert
			Assert.AreEqual("?1=http%3a%2f%2f", qs.ToString());
		}
	
		[TestMethod]
		public void ShouldDecodeWhenGettingValue()
		{
			// arrange
			QueryString qs = new QueryString();
			qs.Add("1", "http%3a%2f%2f");

			// act and assert
			Assert.AreEqual("http://", qs["1"]);
		}
	}
}
