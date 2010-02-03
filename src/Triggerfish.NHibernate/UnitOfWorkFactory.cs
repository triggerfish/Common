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
		private static ISessionFactory m_factory;
		private static IUnitOfWorkStorage m_storage;

		/// <summary>
		/// Initialiser method
		/// </summary>
		/// <param name="config">The fluent NHibernate configuration from which to build a session factory</param>
		public static void Initialise(FluentConfiguration config)
		{
			Initialise(config, new WebSessionStorage());
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
		/// <param name="factory">ISessionFactory interface instance</param>
		/// <param name="storage">IUnitOfWorkStorage interface instance</param>
		public static void Initialise(ISessionFactory factory, IUnitOfWorkStorage storage)
		{
			m_factory = factory;
			m_storage = storage;
		}

		/// <summary>
		/// Gets the current open session
		/// </summary>
		/// <returns>An ISession</returns>
		public static ISession GetCurrentSession()
		{
			UnitOfWorkFactory factory = new UnitOfWorkFactory();
			UnitOfWork uow = factory.GetCurrentUnitOfWork() as UnitOfWork;

			if (null == uow || !uow.IsActive)
			{
				uow = new UnitOfWork(m_factory.OpenSession());
				uow.Begin();
				m_storage.SetCurrentUnitOfWork(uow);
			}

			return uow.Session;
		}

		/// <summary>
		/// Gets the current unit of work
		/// </summary>
		/// <returns>The current unit of work object</returns>
		public IUnitOfWork GetCurrentUnitOfWork()
		{
			return m_storage.GetCurrentUnitOfWork();
		}

		/// <summary>
		/// Closes the unit of work currently open
		/// </summary>
		public void CloseCurrentUnitOfWork()
		{
			IUnitOfWork uow = GetCurrentUnitOfWork();
			if (null != uow)
			{
				uow.End();
				m_storage.DeleteCurrentUnitOfWork();
			}
		}
	}
}
