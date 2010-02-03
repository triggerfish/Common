using System;
using System.Text;
using NHibernate;
using System.Web;
using Triggerfish.Database;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Stores UnitOfWork objects in the current HttpContext
	/// </summary>
	public class WebSessionStorage : IUnitOfWorkStorage
	{
		private readonly string m_key = "nhibernate_uow";

		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		public IUnitOfWork GetCurrentUnitOfWork()
		{
			HttpContext context = HttpContext.Current;
			if (context.Items.Contains(m_key))
				return context.Items[m_key] as IUnitOfWork;
			return null;
		}

		/// <summary>
		/// Sets a new unit of work as the current
		/// </summary>
		/// <param name="uow">A new IUnitOfWork</param>
		public void SetCurrentUnitOfWork(IUnitOfWork uow)
		{
			HttpContext.Current.Items[m_key] = uow;
		}

		/// <summary>
		/// Deletes the current unit of work
		/// </summary>
		public void DeleteCurrentUnitOfWork()
		{
			HttpContext.Current.Items.Remove(m_key);
		}
	}
}
