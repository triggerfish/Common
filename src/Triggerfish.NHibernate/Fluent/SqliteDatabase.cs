using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Triggerfish.FluentNHibernate;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Sqlite database configuration
	/// </summary>
	public class SqliteDatabase : IDatabaseConfiguration
    {
		private readonly string m_strDbPath;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filename">The name of the SQLite database file</param>
		public SqliteDatabase(string filename)
		{
			Uri uri = new Uri(Assembly.GetCallingAssembly().CodeBase);
			m_strDbPath =  Path.Combine(Path.GetDirectoryName(uri.LocalPath), filename);
		}

		/// <summary>
		/// Returns the FluentNHibernate Sqlite configuration data
		/// </summary>
		/// <param name="assembly">The assembly containing the mappings</param>
		/// <returns>SQLite configuration</returns>
		IPersistenceConfigurer IDatabaseConfiguration.Create(Assembly assembly)
		{
			SQLiteConfiguration config = new SQLiteConfiguration()
											.UsingFile(m_strDbPath)
											.ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu")
											.ShowSql();

			if (!File.Exists(m_strDbPath))
			{
				PersistenceModel pm = new PersistenceModel();
				pm.AddMappingsFromAssembly(assembly);

				Session ss = new Session(config.ToProperties(), pm);
				ss.BuildSchema();
				ss.Close();
			}

			return config;
		}
    }
}
