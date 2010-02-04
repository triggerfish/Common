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
	public class UnitOfWorkFactoryTests
	{
		Mock<ITransaction> m_tx = new Mock<ITransaction>();
		Mock<ISession> m_session = new Mock<ISession>();
		Mock<ISessionFactory> m_sessionFactory = new Mock<ISessionFactory>();
		Mock<IUnitOfWorkStorage> m_storage = new Mock<IUnitOfWorkStorage>();

		#region Initialise tests

		[TestMethod]
		public void ShouldCreateSimpleSessionStorage()
		{
			// arrange

			// act
			UnitOfWorkFactory.Initialise(m_sessionFactory.Object, UnitOfWorkStorageType.Simple);

			// assert
			Assert.IsTrue(UnitOfWorkFactory.Storage is SimpleSessionStorage);
		}

		[TestMethod]
		public void ShouldCreateWebSessionStorage()
		{
			// arrange

			// act
			UnitOfWorkFactory.Initialise(m_sessionFactory.Object, UnitOfWorkStorageType.Web);

			// assert
			Assert.IsTrue(UnitOfWorkFactory.Storage is WebSessionStorage);
		}

		[TestMethod]
		public void ShouldThrowIfInvalidStorageSpecified()
		{
			// arrange

			// act
			try
			{
				UnitOfWorkFactory.Initialise(m_sessionFactory.Object, (UnitOfWorkStorageType)57);
			}
			catch (ArgumentOutOfRangeException)
			{
				return;
			}

			// assert
			Assert.Fail("Exception not thrown from Initialise for invalid storage type");
		}

		#endregion

		#region GetCurrentSession tests

		[TestMethod]
		public void ShouldGetCurrentSession()
		{
			// arrange 
			m_tx.Setup(x => x.IsActive).Returns(true);
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns(uow);

			// act
			ISession s = UnitOfWorkFactory.GetCurrentSession();

			// assert
			Assert.IsTrue(ReferenceEquals(m_session.Object, s));
		}

		[TestMethod]
		public void ShouldCreateNewUoWIfCurrentDoesNotExist()
		{
			// arrange 
			m_sessionFactory.Setup(x => x.OpenSession()).Returns(m_session.Object);
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns<IUnitOfWork>(null);

			// act
			ISession s = UnitOfWorkFactory.GetCurrentSession();

			// assert
			Assert.IsTrue(ReferenceEquals(m_session.Object, s));
			m_sessionFactory.Verify(x => x.OpenSession());
		}

		[TestMethod]
		public void ShouldCreateNewUoWIfCurrentNotActive()
		{
			// arrange 
			m_sessionFactory.Setup(x => x.OpenSession()).Returns(m_session.Object);
			m_tx.Setup(x => x.IsActive).Returns(false);
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns(uow);

			// act
			ISession s = UnitOfWorkFactory.GetCurrentSession();

			// assert
			Assert.IsTrue(ReferenceEquals(m_session.Object, s));
			m_sessionFactory.Verify(x => x.OpenSession());
		}

		[TestMethod]
		public void ShouldBeginTransactionWhenCreateNewUoW()
		{
			// arrange 
			m_sessionFactory.Setup(x => x.OpenSession()).Returns(m_session.Object);
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns<IUnitOfWork>(null);

			// act
			ISession s = UnitOfWorkFactory.GetCurrentSession();

			// assert
			m_session.Verify(x => x.BeginTransaction());
		}

		[TestMethod]
		public void ShouldSaveInStorageWhenCreateNewUoW()
		{
			// arrange 
			m_sessionFactory.Setup(x => x.OpenSession()).Returns(m_session.Object);
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns<IUnitOfWork>(null);

			// act
			ISession s = UnitOfWorkFactory.GetCurrentSession();

			// assert
			m_storage.Verify(x => x.SetCurrentUnitOfWork(It.IsAny<UnitOfWork>()));
		}

		#endregion

		#region GetCurrentUnitOfWork tests

		[TestMethod]
		public void ShouldGetCurrentUnitOfWork()
		{
			// arrange 
			Mock<IUnitOfWork> expected = new Mock<IUnitOfWork>();
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns(expected.Object);

			// act
			IUnitOfWork uow = UnitOfWorkFactory.GetCurrentUnitOfWork();

			// assert
			Assert.IsTrue(ReferenceEquals(expected.Object, uow));
			m_storage.Verify(x => x.GetCurrentUnitOfWork());
		}

		#endregion

		#region CloseCurrentUnitOfWork tests

		[TestMethod]
		public void ShouldCloseCurrentUnitOfWork()
		{
			// arrange 
			Mock<IUnitOfWork> expected = new Mock<IUnitOfWork>();
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns(expected.Object);

			// act
			UnitOfWorkFactory.CloseCurrentUnitOfWork();

			// assert
			expected.Verify(x => x.End());
			m_storage.Verify(x => x.DeleteCurrentUnitOfWork());
		}

		[TestMethod]
		public void ShouldNotCloseCurrentUnitOfWorkIfNoCurrentUoW()
		{
			// arrange 
			m_storage.Setup(x => x.GetCurrentUnitOfWork()).Returns<IUnitOfWork>(null);

			// act
			UnitOfWorkFactory.CloseCurrentUnitOfWork();

			// assert
			m_storage.Verify(x => x.DeleteCurrentUnitOfWork(), Times.Never());
		}

		#endregion


		[TestInitialize]
		public void SetupTest()
		{
			UnitOfWorkFactory.Initialise(m_sessionFactory.Object, m_storage.Object);
		}
	}
}
