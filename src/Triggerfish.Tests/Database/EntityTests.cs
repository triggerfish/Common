using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Database;

namespace Triggerfish.Tests.Database
{
	[TestClass]
	public class EntityTests
	{
		[TestMethod]
		public void ShouldEquateSameEntityReference()
		{
			// arrange
			IntEntity expected = new IntEntity(1);

			// assert
			Assert.IsTrue(expected.Equals(expected));
		}

		[TestMethod]
		public void ShouldEquateSameObjectReference()
		{
			// arrange
			IntEntity expected = new IntEntity(1);

			// assert
			Assert.IsTrue(expected.Equals((object)expected));
		}
	
		[TestMethod]
		public void ShouldEquateEntities()
		{
			// arrange
			IntEntity expected = new IntEntity(1);
			IntEntity actual = new IntEntity(1);

			// assert
			Assert.IsTrue(expected.Equals(actual));
		}

		[TestMethod]
		public void ShouldNotEquateEntities()
		{
			// arrange
			IntEntity expected = new IntEntity(1);
			IntEntity actual = new IntEntity(2);

			// assert
			Assert.IsFalse(expected.Equals(actual));
		}
	
		[TestMethod]
		public void ShouldNotEquateDifferentObjectType()
		{
			// arrange
			IntEntity expected = new IntEntity(1);

			// assert
			Assert.IsFalse(expected.Equals("1"));
		}
	}

	internal class IntEntity : Entity<int>
	{
		public IntEntity(int id) : base(id) { }
	}
}
