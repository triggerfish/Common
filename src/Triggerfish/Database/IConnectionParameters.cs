using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Database
{
	/// <summary>
	/// Generic interface for database server connection parameters
	/// </summary>
	public interface IConnectionParameters
	{
		/// <summary>
		/// The database server
		/// </summary>
		string Server { get; set; }

		/// <summary>
		/// The database
		/// </summary>
		string Database { get; set; }

		/// <summary>
		/// True if use NT authentication to connect, False if
		/// use username and passwords
		/// </summary>
		bool NTauth { get; set; }

		/// <summary>
		/// The username to connect with
		/// </summary>
		string Username { get; set; }

		/// <summary>
		/// The password to connect with
		/// </summary>
		string Password { get; set; }
	}
}
