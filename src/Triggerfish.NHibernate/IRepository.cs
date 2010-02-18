using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NHibernate;
using NHibernate.Linq;
using Triggerfish.NHibernate;
using NHibernate.Validator.Exceptions;
using Triggerfish.Linq;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Represents an NHibernate repository fr a specific type
	/// </summary>
	public interface IRepository<T>
	{
		/// <summary>
		/// The NHibernate session
		/// </summary>
		ISession Session { get; }

		/// <summary>
		/// Get an object from the objects ID
		/// </summary>
		/// <param name="id">Object ID</param>
		/// <returns>The object</returns>
		T Get(int id);

		/// <summary>
		/// Get a queryable list of objects
		/// </summary>
		/// <returns>The objects</returns>
		INHibernateQueryable<T> GetAll();

		/// <summary>
		/// Deletes an object from the repository
		/// </summary>
		/// <param name="target">the object</param>
		void Delete(T target);

		/// <summary>
		/// Inserts a new transient object into the repository
		/// </summary>
		/// <param name="target">The object to insert</param>
		void Insert(T target);

		/// <summary>
		/// Inserts a list of new transient object into the repository
		/// </summary>
		/// <param name="targets">The list of objects to insert</param>
		void Insert(IEnumerable<T> targets);

		/// <summary>
		/// Updates an detached object. The object will be added to the current 
		/// session and the database updated with the objects values
		/// </summary>
		/// <param name="target">The object to update</param>
		void Update(T target);

		/// <summary>
		/// Updates a list of detached object. The object will be added to the current 
		/// session and the database updated with the objects values
		/// </summary>
		/// <param name="targets">The list of objects to update</param>
		void Update(IEnumerable<T> targets);
	}
}
