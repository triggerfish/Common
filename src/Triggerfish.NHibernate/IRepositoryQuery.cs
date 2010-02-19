using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// An interface to a repository query
	/// </summary>
	public interface IRepositoryQuery<T> where T : class
	{
		/// <summary>
		/// Get a specific entity
		/// </summary>
		/// <param name="id">The id of the entity</param>
		/// <returns>The amended query</returns>
		IRepositoryQuery<T> ById(int id);

		/// <summary>
		/// Get entities matching a where clause
		/// </summary>
		/// <param name="clause">The where clause</param>
		/// <returns>The amended query</returns>
		IRepositoryQuery<T> Where(Expression<Func<T, bool>> clause);

		/// <summary>
		/// Eagerly gets the specified child object
		/// </summary>
		/// <param name="path">Child object path</param>
		/// <returns>The amended query</returns>
		IRepositoryQuery<T> With(Expression<Func<T, object>> path);

		/// <summary>
		/// Executes the query and returns the result
		/// </summary>
		/// <returns>The resultant object</returns>
		T Result();

		/// <summary>
		/// Executes the query and returns a list of results
		/// </summary>
		/// <returns>The resultant objects</returns>
		IList<T> Results();
	}
}
