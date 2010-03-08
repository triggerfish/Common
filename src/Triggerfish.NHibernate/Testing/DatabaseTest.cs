using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using Triggerfish.NHibernate;
using Triggerfish.Database;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Event;


namespace Triggerfish.NHibernate.Testing
{
	/// <summary>
	/// Base class for running a unit test against a database using NHibernate
	/// </summary>
	public abstract class DatabaseTest
	{
		/// <summary>
		/// The current unit of work
		/// </summary>
		protected UnitOfWork UnitOfWork { get; private set; }

		/// <summary>
		/// The current NHibernate session
		/// </summary>
		protected ISession Session { get; private set; }

		/// <summary>
		/// Setup test method. Should invoke from base class [TestInitialize] method
		/// </summary>
		public void SetupTest<T>()
		{
			FluentConfiguration configuration =
				Fluently.Configure()
					.Database(SQLiteConfiguration.Standard
						.InMemory()
						.ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu")
						.ShowSql())
					.Mappings(x => x.FluentMappings.AddFromAssemblyOf<T>());

			configuration.BuildConfiguration();

			SingleConnectionSessionSourceForSQLiteInMemoryTesting ss = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);
			ss.BuildSchema();

			Session = ss.CreateSession();
			UnitOfWork = new UnitOfWork(Session);
			UnitOfWork.Begin();

			InitialiseData(Session);
			UnitOfWork.Commit();

			Session.Clear();
		}

		/// <summary>
		/// Setup test method. Should invoke from base class [TestCleanup] method
		/// </summary>
		public void TearDownTest()
		{
			UnitOfWork.End();
		}

		/// <summary>
		/// Template method pattern for derived classes to perform custom data insertion
		/// after the database connection is created and before the first commit
		/// </summary>
		/// <param name="session">The NHibernate session</param>
		protected virtual void InitialiseData(ISession session)
		{
		}
	}
}
