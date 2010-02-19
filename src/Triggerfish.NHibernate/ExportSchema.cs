using System.Reflection;
using System.Collections.Generic;
using System;
using NHibernate.Tool.hbm2ddl;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Class to export a schema based on a configuration
	/// </summary>
	public class ExportSchema
	{
		/// <summary>
		/// Generates the schema script
		/// </summary>
		/// <param name="config">The configuration from which to export the schema</param>
		/// <param name="scriptAction">A delegate instance to receive the generated script text</param>
		public static void FromConfiguration(Configuration config, Action<string> scriptAction)
		{
			SchemaExport exporter = new SchemaExport(config.Config.BuildConfiguration());
			exporter.Create(scriptAction, false);
		}
	}
}
