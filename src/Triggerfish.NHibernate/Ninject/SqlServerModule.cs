using Ninject.Modules;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace Triggerfish.NHibernate.Ninject
{
	/// <summary>
	/// Provides a connection to a SQL Server database
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SqlServerModule<T> : DatabaseModule<T>
    {
		private string m_server;
		private string m_database;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="server">The path to the server</param>
		/// <param name="database">The name of the database</param>
		protected SqlServerModule(string server, string database)
		{
			m_server = server;
			m_database = database;
		}

		/// <summary>
		/// Returns the FluentNHibernate SQL Server configuration data
		/// </summary>
		/// <returns>SQL Server configuration</returns>
		protected override IPersistenceConfigurer CreateDatabase()
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
