using System.Reflection;
using System.Collections.Generic;
using System;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Interface for a configuration creator
	/// </summary>
	public interface ISchemaExporter
	{
		/// <summary>
		/// The name of the exporter
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Specify what string parameters are required by the exporter
		/// </summary>
		IList<string> ParameterNames { get; }

		/// <summary>
		/// Generates the schema script
		/// </summary>
		/// <param name="scriptAction">A delegate instance to receive the generated script text</param>
		/// <param name="parameterValues">Parameter values required by the exporter</param>
		void GenerateScript(Action<string> scriptAction, IList<string> parameterValues);
	}
}
