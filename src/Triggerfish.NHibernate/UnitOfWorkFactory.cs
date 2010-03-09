using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using NHibernate.Context;
using Triggerfish.Database;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// Factory class to manage UnitOfWork objects and NHibernate sessions
	/// </summary>
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private static ISessionFactory m_sessionFactory;

		/// <summary>
		/// Property for the UoW storage mechanism
		/// </summary>
		public static IUnitOfWorkStorage Storage { get; private set; }

		/// <summary>
		/// Initialiser method
		/// </summary>
		/// <param name="config">The fluent NHibernate configuration from which to build a session factory</param>
		/// <param name="storageType">The storage mechanism to use</param>
		public static void Initialise(FluentConfiguration config, UnitOfWorkStorageType storageType)
		{
			Initialise(config.BuildSessionFactory(), storageType);
		}

		/// <summary>
		/// Initialiser method
		/// </summary>
		/// <param name="config">The fluent NHibernate configuration from which to build a session factory</param>
		/// <param name="storage">The storage mechanism to use</param>
		public static void Initialise(FluentConfiguration config, IUnitOfWorkStorage storage)
		{
			Initialise(config.BuildSessionFactory(), storage);
		}

		/// <summary>
		/// Initialiser method
		/// </summary>
		/// <param name="factory">The NHibernate session factory</param>
		/// <param name="storageType">The storage mechanism to use</param>
		public static void Initialise(ISessionFactory factory, UnitOfWorkStorageType storageType)
		{
			IUnitOfWorkStorage storageObj;
			switch (storageType)
			{
				case UnitOfWorkStorageType.Simple:
					storageObj = new SimpleSessionStorage();
					break;
				case UnitOfWorkStorageType.Web:
					storageObj = new WebSessionStorage();
					break;
				default:
					throw new ArgumentOutOfRangeException("Unknown storage type specified");
			}

			Initialise(factory, storageObj);
		}

		/// <summary>
		/// Initialiser method
		/// </summary>
		/// <param name="factory">The NHibernate session factory</param>
		/// <param name="storage">The storage mechanism to use</param>
		public static void Initialise(ISessionFactory factory, IUnitOfWorkStorage storage)
		{
			m_sessionFactory = factory;
			Storage = storage;
		}

		/// <summary>
		/// Gets the current open session
		/// </summary>
		/// <returns>An ISession</returns>
		public static ISession GetCurrentSession()
		{
			return ((UnitOfWork)CreateUnitOfWork()).Session;
		}

		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current unit of work object</returns>
		public static IUnitOfWork GetCurrentUnitOfWork()
		{
			return Storage.GetCurrentUnitOfWork();
		}

		/// <summary>
		/// Creates the current UoW. The method will only
		/// create a new UoW if one is not already active. 
		/// Otherwise the existing UoW is returned
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		public static IUnitOfWork CreateUnitOfWork()
		{
			UnitOfWork uow = GetCurrentUnitOfWork() as UnitOfWork;

			if (null == uow || !uow.IsActive)
			{
				uow = new UnitOfWork(m_sessionFactory.OpenSession());
				uow.Begin();
				Storage.SetCurrentUnitOfWork(uow);
			}

			return uow;
		}

		/// <summary>
		/// Closes the unit of work currently open
		/// </summary>
		public static void CloseCurrentUnitOfWork()
		{
			IUnitOfWork uow = GetCurrentUnitOfWork();
			if (null != uow)
			{
				uow.End();
				Storage.DeleteCurrentUnitOfWork();
			}
		}

		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current unit of work object</returns>
		IUnitOfWork IUnitOfWorkFactory.GetCurrentUnitOfWork()
		{
			return UnitOfWorkFactory.GetCurrentUnitOfWork();
		}

		/// <summary>
		/// Closes the unit of work currently open
		/// </summary>
		void IUnitOfWorkFactory.CloseCurrentUnitOfWork()
		{
			UnitOfWorkFactory.CloseCurrentUnitOfWork();
		}

		/// <summary>
		/// Creates the current UoW. The method will only
		/// create a new UoW if one is not already active. 
		/// Otherwise the existing UoW is returned
		/// </summary>
		/// <returns>The current IUnitOfWork</returns>
		IUnitOfWork IUnitOfWorkFactory.CreateUnitOfWork()
		{
			return UnitOfWorkFactory.CreateUnitOfWork();
		}
	}
}
