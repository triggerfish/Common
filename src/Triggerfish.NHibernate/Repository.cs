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
	public class Repository<T> : IRepository<T>
	{
		/// <summary>
		/// The NHibernate session
		/// </summary>
		public ISession Session { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="session">A NHibernate session</param>
		public Repository(ISession session)
		{
		    Session = session;
		}

		/// <summary>
		/// Gets the number of entities in the database table
		/// </summary>
		public virtual int Count
		{
			get { return GetAll().Count(); }
		}

		/// <summary>
		/// Get an object from the objects ID
		/// </summary>
		/// <param name="id">Object ID</param>
		/// <returns>The object</returns>
		public virtual T Get(int id)
		{
			return Session.Get<T>(id);
		}

		/// <summary>
		/// Get a queryable list of objects
		/// </summary>
		/// <returns>The objects</returns>
		public virtual INHibernateQueryable<T> GetAll()
		{
			return Session.Linq<T>();
		}

		/// <summary>
		/// Deletes an object from the repository
		/// </summary>
		/// <param name="target">the object</param>
		public virtual void Delete(T target)
		{
			Session.Delete(target);
		}

		/// <summary>
		/// Inserts a new transient object into the repository
		/// </summary>
		/// <param name="target">The object to insert</param>
		public virtual void Insert(T target)
		{
			Session.Save(target);
		}

		/// <summary>
		/// Inserts a list of new transient object into the repository
		/// </summary>
		/// <param name="targets">The list of objects to insert</param>
		public virtual void Insert(IEnumerable<T> targets)
		{
			IEnumerable<object> objs = targets.Cast<object>();
			objs.ForEach(o => Session.Save(o));
		}

		/// <summary>
		/// Updates an detached object. The object will be added to the current 
		/// session and the database updated with the objects values
		/// </summary>
		/// <param name="target">The object to update</param>
		public virtual void Update(T target)
		{
			Session.Update(target);
		}

		/// <summary>
		/// Updates a list of detached object. The object will be added to the current 
		/// session and the database updated with the objects values
		/// </summary>
		/// <param name="targets">The list of objects to update</param>
		public virtual void Update(IEnumerable<T> targets)
		{
			IEnumerable<object> objs = targets.Cast<object>();
			objs.ForEach(o => Session.Update(o));
		}
	}
}
