using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Triggerfish.NHibernate.Tests
{
	[TestClass]
	public class UnitOfWorkTests
	{
		Mock<ISession> m_session = new Mock<ISession>();
		Mock<ITransaction> m_tx = new Mock<ITransaction>();

		[TestMethod]
		public void ShouldSetToCommitModeOnConstruction()
		{
			// arrange 
			
			// act
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// assert
			Assert.AreEqual(FlushMode.Commit, m_session.Object.FlushMode);
		}

		#region IsActive tests

		[TestMethod]
		public void ShouldBeActive()
		{
			// arrange 
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			Assert.IsTrue(uow.IsActive);
		}

		[TestMethod]
		public void ShouldBeInactiveIfNoTransaction()
		{
			// arrange 
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			Assert.IsFalse(uow.IsActive);
		}

		[TestMethod]
		public void ShouldBeInactiveIfTransactionInactive()
		{
			// arrange 
			m_tx.Setup(x => x.IsActive).Returns(false);
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			Assert.IsFalse(uow.IsActive);
		}

		#endregion

		#region Begin tests

		[TestMethod]
		public void ShouldBeginNewTransaction()
		{
			// arrange 
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.Begin();

			// assert
			m_session.Verify(x => x.BeginTransaction());
		}

		[TestMethod]
		public void ShouldBeginNewTransactionIfCurrentOneNotActive()
		{
			// arrange 
			UnitOfWork uow = new UnitOfWork(m_session.Object);
			m_tx.Setup(x => x.IsActive).Returns(false);
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);

			// act
			uow.Begin();

			// assert
			m_session.Verify(x => x.BeginTransaction());
		}

		[TestMethod]
		public void ShouldNotBeginNewTransactionIfOneStillActive()
		{
			// arrange 
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.Begin();

			// assert
			m_session.Verify(x => x.BeginTransaction(), Times.Never());
		}

		[TestMethod]
		public void ShouldEndIfBeginTransactionFails()
		{
			// arrange 
			m_session.Setup(x => x.BeginTransaction()).Throws(new HibernateException());
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			try
			{
			    uow.Begin();
			}
			catch (HibernateException)
			{
				Assert.AreEqual(null, uow.Session);
			    return;
			}

			// assert
			Assert.Fail("Begin did not throw exception");
		}

		#endregion

		#region Commit tests

		[TestMethod]
		public void ShouldCommit()
		{
			// arrange
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.Commit();

			// assert
			m_tx.Verify(x => x.Commit());
		}

		[TestMethod]
		public void ShouldBeginANewTxAfterCommit()
		{
			// arrange
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.Commit();

			// assert
			m_session.Verify(x => x.BeginTransaction());
		}

		[TestMethod]
		public void ShouldNotCommitIfAlreadyCommitted()
		{
			// arrange
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			m_tx.Setup(x => x.WasCommitted).Returns(true);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.Commit();

			// assert
			m_tx.Verify(x => x.Commit(), Times.Never());
		}

		[TestMethod]
		public void ShouldNotCommitIfAlreadyRolledback()
		{
			// arrange
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			m_tx.Setup(x => x.WasRolledBack).Returns(true);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.Commit();

			// assert
			m_tx.Verify(x => x.Commit(), Times.Never());
		}

		[TestMethod]
		public void ShouldEndIfCommitFails()
		{
			// arrange 
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			m_tx.Setup(x => x.Commit()).Throws(new HibernateException());
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			try
			{
				uow.Commit();
			}
			catch (HibernateException)
			{
				Assert.AreEqual(null, uow.Session);
				return;
			}

			// assert
			Assert.Fail("Commit did not throw exception");
		}

		#endregion

		#region End tests

		[TestMethod]
		public void ShouldKillSessionOnEnd()
		{
			// arrange
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.End();

			// assert
			Assert.AreEqual(null, uow.Session);
		}

		[TestMethod]
		public void ShouldRollbackOnEnd()
		{
			// arrange
			m_session.Setup(x => x.Transaction).Returns(m_tx.Object);
			UnitOfWork uow = new UnitOfWork(m_session.Object);

			// act
			uow.End();

			// assert
			m_tx.Verify(x => x.Rollback());
		}

		#endregion

		[TestInitialize]
		public void SetupTest()
		{
			m_tx.Setup(x => x.IsActive).Returns(true);
			m_tx.Setup(x => x.WasCommitted).Returns(false);
			m_tx.Setup(x => x.WasRolledBack).Returns(false);

			m_session.SetupProperty(x => x.FlushMode, FlushMode.Auto);
			m_session.Setup(x => x.BeginTransaction()).Returns(m_tx.Object);
		}
	}
}
