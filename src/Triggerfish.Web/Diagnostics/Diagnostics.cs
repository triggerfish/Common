using System;
using System.Web;
using System.Text;

namespace Triggerfish.Web.Diagnostics
{
	/// <summary>
	/// Interface for a type that implements some diagnostics routines
	/// </summary>
	public interface IDiagnostics
	{
		/// <summary>
		/// A unique key to identify this specific diagnostic object
		/// </summary>
		string Key { get; }

		/// <summary>
		/// Start tracking/recording diagnostic data
		/// </summary>
		void Start();

		/// <summary>
		/// Stop tracking/recording diagnostic data
		/// </summary>
		void Stop();

		/// <summary>
		/// Render the diagnostic data as an HTML string
		/// </summary>
		/// <returns>An HTML string</returns>
		string ToHtmlString();
	}
}
