using System;
using System.Web;
using System.Text;

namespace Triggerfish.Web.Diagnostics
{
	/// <summary>
	/// An abstract HTTP module for tracking diagnostic data
	/// </summary>
	public abstract class DiagnosticsModule : IHttpModule
	{
		private string m_key;

		/// <summary>
		/// Initialised per request
		/// </summary>
		/// <param name="appContext">The application context</param>
		public void Init(HttpApplication appContext)
		{
			appContext.PreRequestHandlerExecute += (sender, e) =>
			{
				StartDiagnostics(((HttpApplication)sender).Context);
			};

			appContext.PostRequestHandlerExecute += (sender, e) =>
			{
				StopDiagnostics(((HttpApplication)sender).Context);
			};
		}

		/// <summary>
		/// Start the diagnostics running
		/// </summary>
		/// <param name="context">HttpContext object</param>
		public void StartDiagnostics(HttpContext context)
		{
			IDiagnostics d = CreateDiagnostics();
			if (null != d)
			{
				m_key = d.Key;
				context.Items[m_key] = d;
				d.Start();
			}
		}

		/// <summary>
		/// Stop the diagnostics running and write data to HTML page
		/// </summary>
		/// <param name="context">HttpContext object</param>
		public void StopDiagnostics(HttpContext context)
		{
			if (!String.IsNullOrEmpty(m_key) && context.Items.Contains(m_key))
			{
				IDiagnostics d = context.Items[m_key] as IDiagnostics;
				d.Stop();
				context.Items.Remove(m_key);
				context.Response.Filter = new DiagnosticsResponseFilter(context.Response.Filter, d.ToHtmlString());
			}
		}

		/// <summary>
		/// Dispose of the module
		/// </summary>
		public void Dispose() { /* Not needed */ }

		/// <summary>
		/// Implement to return the specific diagnostics object to run
		/// </summary>
		/// <returns></returns>
		protected abstract IDiagnostics CreateDiagnostics();
	}
}
