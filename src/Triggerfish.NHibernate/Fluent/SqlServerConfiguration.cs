using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// SQL Server database configuration
	/// </summary>
	public class SqlServerModule : IDatabaseConfiguration
    {
		private readonly string m_server;
		private readonly string m_database;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="server">The path to the server</param>
		/// <param name="database">The name of the database</param>
		public SqlServerModule(string server, string database)
		{
			m_server = server;
			m_database = database;
		}

		/// <summary>
		/// Returns the FluentNHibernate SQL Server configuration data
		/// </summary>
		/// <param name="assembly">The assembly containing the mappings</param>
		/// <returns>SQL Server configuration</returns>
		IPersistenceConfigurer IDatabaseConfiguration.Create(Assembly assembly)
		{
			return MsSqlConfiguration.MsSql2005
						.ConnectionString(c => c
							.Server(m_server)
							.Database(m_database)
							.TrustedConnection())
						.ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu")
						.ShowSql();
		}
    }
}
