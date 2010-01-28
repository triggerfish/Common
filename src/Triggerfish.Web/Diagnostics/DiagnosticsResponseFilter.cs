using System;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Triggerfish.Web.Diagnostics
{
	/// <summary>
	/// Filter for the diagnostics object. Appends the diagnostics
	/// HTML string to the foot of the current page
	/// </summary>
	public class DiagnosticsResponseFilter : MemoryStream
	{
		private Stream m_innerStream;
		private string m_text;

		/// <summary>
		/// Costructor
		/// </summary>
		/// <param name="inner">The stream to write the HTML to</param>
		/// <param name="text">The HTML to write</param>
		public DiagnosticsResponseFilter(Stream inner, string text)
		{
			m_innerStream = inner;
			m_text = text;
		}

		/// <summary>
		/// Write the HTML to the output stream
		/// </summary>
		/// <param name="buffer">Array of bytes</param>
		/// <param name="offset">The offset into the buffer at which to start writing data</param>
		/// <param name="count">The number of bytes to be written</param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			string str = UTF8Encoding.UTF8.GetString(buffer);
			Regex r = new Regex("</body>", RegexOptions.Compiled | RegexOptions.Multiline);
			if (r.IsMatch(str))
			{
				str = r.Replace(str, m_text + "</body>");
				byte[] b = UTF8Encoding.UTF8.GetBytes(str);
				m_innerStream.Write(b, offset, b.Length);
			}
			else
			{
				m_innerStream.Write(buffer, offset, count);
			}
		}
	}
}
