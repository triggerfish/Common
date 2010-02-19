using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Triggerfish.Database;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// An implementation of a repository query
	/// </summary>
	public class RepositoryQuery<T> : IRepositoryQuery<T> where T : class
	{
		private ICriteria m_criteria;

		internal RepositoryQuery(ISession session)
		{
			m_criteria = session.CreateCriteria<T>();
		}

		/// <summary>
		/// Get a specific entity
		/// </summary>
		/// <param name="id">The id of the entity</param>
		/// <returns>The amended query</returns>
		public IRepositoryQuery<T> ById(int id)
		{
			m_criteria.Add(Restrictions.Eq("Id", id));
			return this;
		}

		/// <summary>
		/// Get entities matching a where clause
		/// </summary>
		/// <param name="clause">The where clause</param>
		/// <returns>The amended query</returns>
		public IRepositoryQuery<T> Where(Expression<Func<T, bool>> clause)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Eagerly gets the specified child object
		/// </summary>
		/// <param name="path">Child object path</param>
		/// <returns>The amended query</returns>
		public IRepositoryQuery<T> With(Expression<Func<T, object>> path)
		{
			if (path.Body.NodeType == ExpressionType.MemberAccess)
			{
				m_criteria.SetFetchMode(((MemberExpression)path.Body).Member.Name, FetchMode.Eager);
			}
			return this;
		}

		/// <summary>
		/// Executes the query and returns the result
		/// </summary>
		/// <returns>The resultant object</returns>
		public T Result()
		{
			return m_criteria.UniqueResult<T>();
		}

		/// <summary>
		/// Executes the query and returns a list of results
		/// </summary>
		/// <returns>The resultant objects</returns>
		public IList<T> Results()
		{
			return m_criteria.List<T>();
		}
	}
}
