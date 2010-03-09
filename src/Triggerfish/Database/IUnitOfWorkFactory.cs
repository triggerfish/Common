using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Database
{
	/// <summary>
	/// Interface for a unit of work store
	/// </summary>
	public interface IUnitOfWorkFactory
	{
		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		IUnitOfWork GetCurrentUnitOfWork();

		/// <summary>
		/// Creates the current UoW. The method will only
		/// create a new UoW if one is not already active. 
		/// Otherwise the existing UoW is returned
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		IUnitOfWork CreateUnitOfWork();

		/// <summary>
		/// Closes the current unit of work
		/// </summary>
		void CloseCurrentUnitOfWork();
	}
}
