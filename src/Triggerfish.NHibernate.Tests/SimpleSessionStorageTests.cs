using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using Triggerfish.Database;

namespace Triggerfish.NHibernate.Tests
{
	[TestClass]
	public class SimpleSessionStorageTests
	{
		Mock<IUnitOfWork> m_uow = new Mock<IUnitOfWork>();

		[TestMethod]
		public void ShouldBeInitialisedToNull()
		{
			// arrange 
			SimpleSessionStorage storage = new SimpleSessionStorage();
			
			// act
			IUnitOfWork uow = storage.GetCurrentUnitOfWork();

			// assert
			Assert.AreEqual(null, uow);
		}

		[TestMethod]
		public void ShouldGetAndSetUnitOfWork()
		{
			// arrange 
			SimpleSessionStorage storage = new SimpleSessionStorage();
			
			// act
			storage.SetCurrentUnitOfWork(m_uow.Object);
			IUnitOfWork uow = storage.GetCurrentUnitOfWork();

			// assert
			Assert.IsTrue(ReferenceEquals(uow, m_uow.Object));
		}

		[TestMethod]
		public void ShouldDeleteUnitOfWork()
		{
			// arrange 
			SimpleSessionStorage storage = new SimpleSessionStorage();
			
			// act
			storage.SetCurrentUnitOfWork(m_uow.Object);
			storage.DeleteCurrentUnitOfWork();
			IUnitOfWork uow = storage.GetCurrentUnitOfWork();

			// assert
			Assert.AreEqual(null, uow);
		}
	}
}
