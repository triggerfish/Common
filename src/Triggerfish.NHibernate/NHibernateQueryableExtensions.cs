using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;
using Triggerfish.Linq;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Extension methods for 
	/// </summary>
	public static class NHibernateQueryableExtensions
	{
		/// <summary>
		/// Expands the query to eagerly get the specified child object(s)
		/// </summary>
		/// <typeparam name="TObj">The parent object</typeparam>
		/// <param name="query">The LINQ to NHibernate query</param>
		/// <param name="path">Expression indicating the child property to eagerly get</param>
		/// <returns>An amended INHibernateQueryable</returns>
		public static INHibernateQueryable<TObj> With<TObj>(this INHibernateQueryable<TObj> query, Expression<Func<TObj, object>> path)
		{
			PropertyPathVisitor visitor = new PropertyPathVisitor();
			visitor.Visit(path);
			query.QueryOptions.AddExpansion(visitor.Path);
			return query;
		}
	}
}
