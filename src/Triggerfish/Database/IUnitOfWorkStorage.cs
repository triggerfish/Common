using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Database
{
	/// <summary>
	/// Interface for a unit of work store
	/// </summary>
	public interface IUnitOfWorkStorage
	{
		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		IUnitOfWork GetCurrentUnitOfWork();

		/// <summary>
		/// Sets a new unit of work as the current
		/// </summary>
		/// <param name="uow">A new IUnitOfWork</param>
		void SetCurrentUnitOfWork(IUnitOfWork uow);

		/// <summary>
		/// Deletes the current unit of work
		/// </summary>
		void DeleteCurrentUnitOfWork();
	}
}
