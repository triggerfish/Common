using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using NHibernate.Criterion;

namespace Triggerfish.NHibernate.Tests
{
	[TestClass]
	public class RepositoryQueryTests
	{
		Mock<ISession> m_session = new Mock<ISession>();
		Mock<ICriteria> m_criteria = new Mock<ICriteria>();

		[TestMethod]
		public void ShouldGetById()
		{
			// arrange
			RepositoryQuery<QueryTest> query = new RepositoryQuery<QueryTest>(m_session.Object);

			// act
			query.ById(1).Result();

			// assert
			m_criteria.Verify(x => x.Add(It.IsAny<ICriterion>()));
			m_criteria.Verify(x => x.UniqueResult<QueryTest>());
		}

		[TestMethod]
		public void ShouldGetEagerly()
		{
			// arrange
			RepositoryQuery<QueryTest> query = new RepositoryQuery<QueryTest>(m_session.Object);

			// act
			query.With(x => x.ChildObj).Result();

			// assert
			m_criteria.Verify(x => x.SetFetchMode("ChildObj", FetchMode.Eager));
			m_criteria.Verify(x => x.UniqueResult<QueryTest>());
		}

		[TestMethod]
		public void ShouldGetEagerlyWithMultipleNestingLevels()
		{
			// arrange
			RepositoryQuery<QueryTest> query = new RepositoryQuery<QueryTest>(m_session.Object);

			// act
			query.With(x => x.ChildObj.GrandchildObj).Result();

			// assert
			m_criteria.Verify(x => x.SetFetchMode("ChildObj.GrandchildObj", FetchMode.Eager));
			m_criteria.Verify(x => x.UniqueResult<QueryTest>());
		}

		[TestInitialize]
		public void SetupTest()
		{
			m_session.Setup(x => x.CreateCriteria<QueryTest>()).Returns(m_criteria.Object);
		}
	}

	internal class QueryTest
	{
		public int Id { get; set; }
		public Child ChildObj { get; set; }
	}

	internal class Child
	{
		public int Id { get; set; }
		public Grandchild GrandchildObj { get; set; }
	}

	internal class Grandchild
	{
		public int Id { get; set; }
	}
}
