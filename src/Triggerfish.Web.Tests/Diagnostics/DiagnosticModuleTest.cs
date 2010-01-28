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
		HttpContext m_ctx = new HttpContext(new HttpRequest(null, "http://localhost/", null), new HttpResponse(new StringWriter()));

		[TestMethod]
		public void ShouldStartDiagnostics()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();

			// act
			module.StartDiagnostics(m_ctx);

			// assert
			Assert.IsTrue(m_ctx.Items.Contains(module.m_key));
			module.m_diagnostics.Verify(x => x.Start());
		}

		[TestMethod]
		public void ShouldNotStartDiagnosticsIfNoDiagnosticsProvided()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule(true);

			// act
			module.StartDiagnostics(m_ctx);

			// assert
			Assert.IsFalse(m_ctx.Items.Contains(module.m_key));
			module.m_diagnostics.Verify(x => x.Start(), Times.Never());
		}

		[TestMethod]
		public void ShouldStopDiagnostics()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();
			module.StartDiagnostics(m_ctx);

			// act
			try
			{
				module.StopDiagnostics(m_ctx);
			}
			catch (HttpException)
			{
				// Because we don't have a real HttpContext, setting the response filter
				// throws an exception, because it's not overriding a HttpWriter. Problem
				// is the HttpWriter can only be set internally to the HttpResponse assembly.

				// assert
				Assert.IsFalse(m_ctx.Items.Contains(module.m_key));
				module.m_diagnostics.Verify(x => x.Stop());
				return;
			}

			Assert.Fail("Setting the Filter didn't throw an HttpException");
		}

		[TestMethod]
		public void ShouldNotStopDiagnosticsIfIfKeyNull()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();

			// act
			module.StopDiagnostics(m_ctx);

			// assert
			Assert.IsFalse(m_ctx.Items.Contains(module.m_key));
			module.m_diagnostics.Verify(x => x.Stop(), Times.Never());
		}

		[TestMethod]
		public void ShouldNotStopDiagnosticsIfDiagnosticsDoNotExist()
		{
			// arrange
			MockDiagnosticsModule module = new MockDiagnosticsModule();
			module.StartDiagnostics(m_ctx);
			m_ctx.Items.Remove(module.m_key);

			// act
			module.StopDiagnostics(m_ctx);

			// assert
			Assert.IsFalse(m_ctx.Items.Contains(module.m_key));
			module.m_diagnostics.Verify(x => x.Stop(), Times.Never());
		}
	}

	internal class MockDiagnosticsModule : DiagnosticsModule
	{
		public readonly string m_key = "MockDiagnosticsModule";
		public readonly string m_htmlString = "<div>diagnostics data</div>";
		public Mock<IDiagnostics> m_diagnostics = new Mock<IDiagnostics>();
		private bool m_returnNull = false;

		public MockDiagnosticsModule() { }
		public MockDiagnosticsModule(bool returnNull)
		{
			m_returnNull = returnNull;
		}

		protected override IDiagnostics CreateDiagnostics()
		{
			if (m_returnNull)
				return null;

			m_diagnostics.Setup(d => d.Key).Returns(m_key);
			m_diagnostics.Setup(d => d.ToHtmlString()).Returns(m_htmlString);
			return m_diagnostics.Object;
		}
	}
}
