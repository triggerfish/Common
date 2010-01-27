using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Ninject.Modules;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Triggerfish.FluentNHibernate;

namespace Triggerfish.NHibernate.Ninject
{
	/// <summary>
	/// Sqlite Ninject dependency module
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SqliteModule<T> : DatabaseModule<T>
    {
		private readonly string m_strDbPath;
		private SQLiteConfiguration m_config;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filename">The name of the SQLite database file</param>
		protected SqliteModule(string filename)
		{
			Uri uri = new Uri(Assembly.GetCallingAssembly().CodeBase);
			m_strDbPath =  Path.Combine(Path.GetDirectoryName(uri.LocalPath), filename);
		}

		/// <summary>
		/// Ninject dependency loader
		/// </summary>
		public override void Load()
		{
			base.Load();

			if (!File.Exists(m_strDbPath))
			{
				PersistenceModel pm = new PersistenceModel();
				pm.AddMappingsFromAssembly(typeof(T).Assembly);

				Session ss = new Session(m_config.ToProperties(), pm);
				ss.BuildSchema();
				ss.Close();
			}
		}

		/// <summary>
		/// Returns the FluentNHibernate SQLite configuration data
		/// </summary>
		/// <returns>SQLite configuration</returns>
		protected override IPersistenceConfigurer CreateDatabase()
		{
			m_config = new SQLiteConfiguration()
						.UsingFile(m_strDbPath)
						.ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu")
						.ShowSql();
			return m_config;
		}
    }
}
