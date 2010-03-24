using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using Triggerfish.Database;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// SQL Server database configuration
	/// </summary>
	public class SqlServerDatabase : IDatabaseConfiguration
    {
		private readonly IConnectionParameters m_connectionParams;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionParams">The database connection parameters</param>
		public SqlServerDatabase(IConnectionParameters connectionParams)
		{
			m_connectionParams = connectionParams;
		}

		/// <summary>
		/// Returns the FluentNHibernate SQL Server configuration data
		/// </summary>
		/// <param name="assembly">The assembly containing the mappings</param>
		/// <returns>SQL Server configuration</returns>
		IPersistenceConfigurer IDatabaseConfiguration.Create(Assembly assembly)
		{
			MsSqlConfiguration config = MsSqlConfiguration.MsSql2005
						.ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu")
						.ShowSql();

			if (m_connectionParams.NTauth)
			{
				config.ConnectionString(c => c
							.Server(m_connectionParams.Server)
							.Database(m_connectionParams.Database)
							.TrustedConnection());
			}
			else
			{
				config.ConnectionString(c => c
							.Server(m_connectionParams.Server)
							.Database(m_connectionParams.Database)
							.Username(m_connectionParams.Username)
							.Password(m_connectionParams.Password));
			}

			return config;
		}
    }
}
