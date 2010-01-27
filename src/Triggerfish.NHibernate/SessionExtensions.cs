using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Session extension methods
	/// </summary>
	public static class SessionExtensions
	{
		/// <summary>
		/// Perform an action within a transaction
		/// </summary>
		/// <param name="session">The NHibernate session</param>
		/// <param name="del">The action delegate instance</param>
		public static void WithinTransaction(this ISession session, Action<ISession> del)
		{
			ITransaction tx = session.BeginTransaction();
			try
			{
				del(session);
				tx.Commit();
			}
			catch (Exception)
			{
				tx.Rollback();
				throw;
			}
			finally
			{
				tx.Dispose();
			}
		}
	}
}
