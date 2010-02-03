using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Cfg;
using Triggerfish.Database;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Encapsulates an NHibernate session and transaction
	/// </summary>
	public sealed class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// The underlying NHibernate session
		/// </summary>
		public ISession Session { get; private set; }

		/// <summary>
		/// Returns trus if the unit of work is active, false if not
		/// </summary>
		public bool IsActive 
		{
			get { return null != Session && Session.Transaction != null && Session.Transaction.IsActive; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="session">A NHibernate session</param>
		public UnitOfWork(ISession session)
		{
			Session = session;

			if (session != null)
			{
				Session.FlushMode = FlushMode.Commit;
			}
		}

		/// <summary>
		/// Begins the unit of work (transaction)
		/// </summary>
		public void Begin()
		{
			if (null == Session) return;

			if (IsActive)
				return;

			try
			{
				Session.BeginTransaction();
			}
			catch (HibernateException)
			{
				End();
				throw;
			}
		}

		/// <summary>
		/// Ends the unit of work (transaction). The default behaviour 
		/// should be to rollback and close any sessions. Commits 
		/// should only occur by explicit invocation of Commit()
		/// </summary>
		public void End()
		{
			if (null == Session) return;

			if (IsActive)
				Session.Transaction.Rollback();
			Session.Close();
			Session.Dispose();
			Session = null;
		}

		/// <summary>
		/// Commits the data changes made in the unit of work (transaction)
		/// </summary>
		public void Commit()
		{
			if (null == Session) return;
		
			ITransaction tx = Session.Transaction;
			if (tx.IsActive && !tx.WasCommitted && !tx.WasRolledBack)
			{
				try
				{
					tx.Commit();
					Session.BeginTransaction();
				}
				catch (HibernateException)
				{
					End();
					throw;
				}
			}
		}
	}
}
