using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Database
{
	/// <summary>
	/// Interface for a unit of work, i.e. a transaction
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Returns trus if the unit of work is active, false if not
		/// </summary>
		bool IsActive { get; }

		/// <summary>
		/// Begins the unit of work (transaction)
		/// </summary>
		void Begin();

		/// <summary>
		/// Ends the unit of work (transaction). The default behaviour 
		/// should be to rollback and close any sessions. Commits 
		/// should only occur by explicit invocation of Commit()
		/// </summary>
		void End();

		/// <summary>
		/// Commits the data changes made in the unit of work (transaction)
		/// </summary>
		void Commit();
	}
}
