using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Triggerfish.Web.Diagnostics;
using Triggerfish.Linq;

namespace Triggerfish.Web.Tests.Diagnostics
{
	[TestClass]
	public class DiagnosticsResponseFilterTest
	{
		[TestMethod]
		public void ShouldNotInsertDiagnosticText()
		{
			// arrange
			byte[] buffer = new byte[256]; 
			MemoryStream inner = new MemoryStream(buffer, true);
			string textToInsert = "[inserted]";
			string bodyText = @"<body>[orignal]";
			byte[] bytes = UTF8Encoding.UTF8.GetBytes(bodyText);

			DiagnosticsResponseFilter filter = new DiagnosticsResponseFilter(inner, textToInsert);

			// act
			filter.Write(bytes, 0, bytes.Length);

			// assert
			string str = UTF8Encoding.UTF8.GetString(buffer).Replace('\0', ' ').Trim();
			Assert.AreEqual(bodyText, str);
		}
	
		[TestMethod]
		public void ShouldInsertDiagnosticText()
		{
			// arrange
			byte[] buffer = new byte[256];
			MemoryStream inner = new MemoryStream(buffer, true);
			string textToInsert = "[inserted]";
			string bodyText = "<body>[orignal]</body>";
			byte[] bytes = UTF8Encoding.UTF8.GetBytes(bodyText);

			DiagnosticsResponseFilter filter = new DiagnosticsResponseFilter(inner, textToInsert);

			// act
			filter.Write(bytes, 0, bytes.Length);

			// assert
			string str = UTF8Encoding.UTF8.GetString(buffer).Replace('\0', ' ').Trim();
			Assert.AreEqual("<body>[orignal][inserted]</body>", str);
		}
	}
}
