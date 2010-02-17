using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using NHibernate.Validator.Exceptions;
using Triggerfish.Validator;
using NHibernate.Validator.Engine;

namespace Triggerfish.NHibernate.Tests
{
	[TestClass]
	public class RepositoryTests
	{
		Mock<ISession> m_session = new Mock<ISession>();
		Mock<ITransaction> m_tx = new Mock<ITransaction>();

		[TestMethod]
		public void ShouldGetObject()
		{
			// arrange 
			int id = 1;
			string expected = "TestObject";
			m_session.Setup(x => x.Get<string>(id)).Returns(expected);

			Repository<string> r = new Repository<string>(m_session.Object);

			// act
			string s = r.Get(id);			

			// assert
			Assert.AreEqual(expected, s);
		}

		[TestMethod]
		public void ShouldReturnNullIfObjectDoesNotExist()
		{
			// arrange 
			m_session.Setup(x => x.Get<string>(It.IsAny<object>())).Returns<string>(null);

			Repository<string> r = new Repository<string>(m_session.Object);

			// act
			string s = r.Get(1);

			// assert
			Assert.AreEqual(null, s);
		}

		[TestMethod]
		public void ShouldDelete()
		{
			// arrange 
			Repository<string> r = new Repository<string>(m_session.Object);
			string s = "this";

			// act
			r.Delete(s);

			// assert
			m_session.Verify(x => x.Delete((object)s));
		}

		[TestMethod]
		public void ShouldInsert()
		{
			// arrange 
			Repository<string> r = new Repository<string>(m_session.Object);
			string s = "this";

			// act
			r.Insert(s);

			// assert
			m_session.Verify(x => x.Save((object)s));
		}

		[TestMethod]
		public void ShouldInsertAll()
		{
			// arrange 
			Repository<string> r = new Repository<string>(m_session.Object);
			string[] s = new string[] { "this", "that" };

			// act
			r.Insert(s);

			// assert
			m_session.Verify(x => x.Save(It.IsAny<object>()), Times.Exactly(2));
		}

		[TestMethod]
		public void ShouldUpdate()
		{
			// arrange 
			Repository<string> r = new Repository<string>(m_session.Object);
			string s = "this"; 

			// act - we're actually doing an insert here because the string has not
			// already been persisted. however, it's irrelevant as we're mocking the
			// session and don't actually go anywhere near NHibernate code
			r.Update(s);

			// assert
			m_session.Verify(x => x.Update((object)s));
		}

		[TestMethod]
		public void ShouldUpdateAll()
		{
			// arrange 
			Repository<string> r = new Repository<string>(m_session.Object);
			string[] s = new string[] { "this", "that" };

			// act - we're actually doing an insert here because the string has not
			// already been persisted. however, it's irrelevant as we're mocking the
			// session and don't actually go anywhere near NHibernate code
			r.Update(s);

			// assert
			m_session.Verify(x => x.Update(It.IsAny<object>()), Times.Exactly(2));
		}

		[TestInitialize]
		public void SetupTest()
		{
			m_session.Setup(x => x.BeginTransaction()).Returns(m_tx.Object);
		}
	}
}
