using System.Reflection;
using FluentNHibernate.Cfg.Db;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Interface for an NHibernate database configuration
	/// </summary>
	public interface IDatabaseConfiguration
	{
		/// <summary>
		/// Returns the FluentNHibernate database configuration data
		/// </summary>
		/// <param name="assembly">The assembly containing the mappings</param>
		/// <returns>Database configuration</returns>
		IPersistenceConfigurer Create(Assembly assembly);
	}
}
