using System;
using System.Text;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Web.Diagnostics;
using Moq;
using System.Web;
using System.IO;

namespace Triggerfish.Web.Tests.Diagnostics
{
	[TestClass]
	public class DiagnosticModuleTest
	{
		HttpContext m_ctx = new HttpContext(new System.Web.HttpRequest(null, "http://localhost/", null), new HttpResponse(new StringWriter()));

		[TestMethod]
		public void ShouldStart()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();

			// act
			module.TestStart(m_ctx);

			// assert
			module.m_diagnostics.Verify(x => x.Start());
		}

		[TestMethod]
		public void ShouldNotStartIfNoDiagnosticsProvided()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule(true);

			// act
			module.TestStart(m_ctx);

			// assert
			module.m_diagnostics.Verify(x => x.Start(), Times.Never());
		}

		[TestMethod]
		public void ShouldStop()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();
			module.TestStart(m_ctx);

			// act
			try
			{
				module.TestStop(m_ctx);
			}
			catch (HttpException)
			{
				// Because we don't have a real HttpContext, setting the response filter
				// throws an exception, because it's not overriding a HttpWriter. Problem
				// is the HttpWriter can only be set internally to the HttpResponse assembly.

				// assert
				module.m_diagnostics.Verify(x => x.Stop());
				return;
			}

			Assert.Fail("Setting the Filter didn't throw an HttpException");
		}

		[TestMethod]
		public void ShouldNotStopIfContentTypeIsNotHtml()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();
			m_ctx.Response.ContentType = "other";
			module.TestStart(m_ctx);

			// act
			module.TestStop(m_ctx);

			// assert
			module.m_diagnostics.Verify(x => x.Stop(), Times.Never());
		}
	}

	internal class MockDiagnosticsModule : DiagnosticsModule
	{
		public Mock<IDiagnostics> m_diagnostics = new Mock<IDiagnostics>();
		private bool m_returnNull = false;

		public MockDiagnosticsModule() { }
		public MockDiagnosticsModule(bool returnNull)
		{
			m_returnNull = returnNull;
		}

		public void TestStart(HttpContext context)
		{
			base.Start(context);
		}

		public void TestStop(HttpContext context)
		{
			base.Stop(context);
		}

		protected override IDiagnostics CreateDiagnostics()
		{
			if (m_returnNull)
				return null;

			return m_diagnostics.Object;
		}
	}
}
