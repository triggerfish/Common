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
		/// Closes the current unit of work
		/// </summary>
		void CloseCurrentUnitOfWork();
	}
}
