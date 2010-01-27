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
	public class SessionExtensionsTests
	{
		Mock<ISession> m_session = new Mock<ISession>();
		Mock<ITransaction> m_tx = new Mock<ITransaction>();

		[TestMethod]
		public void ShouldCommitData()
		{
			// act
			m_session.Object.WithinTransaction(x => { /* do nothing */ });

			// assert
			m_tx.Verify(x => x.Commit());
			m_tx.Verify(x => x.Rollback(), Times.Never());
			m_tx.Verify(x => x.Dispose());
		}

		[TestMethod]
		public void ShouldThrowAndRollbackData()
		{
			// act
			try
			{
				m_session.Object.WithinTransaction(x => { throw new Exception(); });
			}
			catch (Exception)
			{
				// assert
				m_tx.Verify(x => x.Commit(), Times.Never());
				m_tx.Verify(x => x.Rollback());
				m_tx.Verify(x => x.Dispose());
				return;
			}

			Assert.Fail("Did not throw exception");
		}

		[TestInitialize]
		public void SetupTest()
		{
			m_session.Setup(x => x.BeginTransaction()).Returns(m_tx.Object);
		}
	}
}
