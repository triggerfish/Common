using System;
using System.Text;
using NHibernate;
using System.Web;
using Triggerfish.Database;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Stores the current UnitOfWork object
	/// </summary>
	public class SimpleSessionStorage : IUnitOfWorkStorage
	{
		private IUnitOfWork m_uow;

		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		public IUnitOfWork GetCurrentUnitOfWork()
		{
			return m_uow;
		}

		/// <summary>
		/// Sets a new unit of work as the current
		/// </summary>
		/// <param name="uow">A new IUnitOfWork</param>
		public void SetCurrentUnitOfWork(IUnitOfWork uow)
		{
			m_uow = uow;
		}

		/// <summary>
		/// Deletes the current unit of work
		/// </summary>
		public void DeleteCurrentUnitOfWork()
		{
			m_uow = null;
		}
	}
}
