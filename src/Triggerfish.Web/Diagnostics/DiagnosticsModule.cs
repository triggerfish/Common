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
		private IDiagnostics m_diagnostics;

		/// <summary>
		/// Initialised per request
		/// </summary>
		/// <param name="application">The application context</param>
		public void Init(HttpApplication application)
		{
			application.PreRequestHandlerExecute += new EventHandler(Application_PreRequestHandlerExecute);
			application.PostRequestHandlerExecute += new EventHandler(Application_PostRequestHandlerExecute);
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

		/// <summary>
		/// Starts the diagnostics
		/// </summary>
		/// <param name="context">HttpContext</param>
		protected virtual void Start(HttpContext context)
		{
			if (null == m_diagnostics)
			{
				m_diagnostics = CreateDiagnostics();
				if (null != m_diagnostics)
				{
					m_diagnostics.Start();
				}
			}		
		}

		/// <summary>
		/// Stops the diagnostics
		/// </summary>
		/// <param name="context">HttpContext</param>
		protected virtual void Stop(HttpContext context)
		{
			if (null != m_diagnostics && context.Response.ContentType == "text/html")
			{
				m_diagnostics.Stop();
				context.Response.Filter = new DiagnosticsResponseFilter(context.Response.Filter, m_diagnostics.ToHtmlString());
				m_diagnostics = null;
			}
		}

		private void Application_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			Start(((HttpApplication)sender).Context);
		}

		private void Application_PostRequestHandlerExecute(object sender, EventArgs e)
		{
			Stop(((HttpApplication)sender).Context);
		}
	}
}
