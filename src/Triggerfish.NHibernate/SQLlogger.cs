using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using log4net.Core;
using System.IO;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Logs to log4net SQL commands executed by NHibernate
	/// </summary>
	public class SQLlogger : AppenderSkeleton
	{
		private static TextWriter m_logger;

		/// <summary>
		/// Setter method for the stream to write the SQL commands to
		/// </summary>
		public static TextWriter Log 
		{
			set { m_logger = value; }
		}

		/// <summary>
		/// Append a logging event to the log
		/// </summary>
		/// <param name="loggingEvent">The logging event</param>
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (null != m_logger && (loggingEvent.LoggerName == "NHibernate.SQL"))
			{
				m_logger.WriteLine(loggingEvent.MessageObject);
			}
		}
	}
}
