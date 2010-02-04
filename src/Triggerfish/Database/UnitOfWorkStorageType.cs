using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Database
{
	/// <summary>
	/// Specifies which storage type is used for the current 
	/// unit of work object
	/// </summary>
	public enum UnitOfWorkStorageType
	{
		/// <summary>
		/// Stores the unit of work in an internal reference
		/// </summary>
		Simple,

		/// <summary>
		/// Stores the unit of work in the current HttpContext
		/// </summary>
		Web
	}
}
