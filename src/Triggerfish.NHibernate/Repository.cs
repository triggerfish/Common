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
	/// Represents an NHibernate repository
	/// </summary>
	public class Repository
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
		/// Get an object from the objects ID
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="id">Object ID</param>
		/// <returns>The object</returns>
		public virtual T Get<T>(int id)
		{
			return Session.Get<T>(id);
		}

		/// <summary>
		/// Get a queryable list of objects
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <returns>The objects</returns>
		public virtual IOrderedQueryable<T> GetAll<T>()
		{
			return Session.Linq<T>();
		}

		/// <summary>
		/// Deletes an object from the repository
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="target">the object</param>
		public virtual void Delete<T>(T target)
		{
			Session.Delete(target);
		}

		/// <summary>
		/// Saves or updates an object to the repository
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="target">The object to save or update</param>
		public virtual void Save<T>(T target)
		{
			Session.SaveOrUpdate(target);
		}

		/// <summary>
		/// Saves or updates a list of objects to the repository
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="targets">The list of objects to save or update</param>
		public virtual void Save<T>(IEnumerable<T> targets)
		{
			IEnumerable<object> objs = targets.Cast<object>();
			objs.ForEach(o => Session.SaveOrUpdate(o));
		}
	}
}
