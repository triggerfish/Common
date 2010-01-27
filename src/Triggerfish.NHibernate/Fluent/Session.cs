using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;

namespace Triggerfish.FluentNHibernate
{
	/// <summary>
	/// Interface for a database session
	/// </summary>
	public interface IDbSession
	{
		/// <summary>
		/// Create a new session or return an existing open session
		/// </summary>
		/// <returns>A NHibernate session</returns>
		ISession CreateSession();

		/// <summary>
		/// Close the session
		/// </summary>
		void Close();
	}

	/// <summary>
	/// A FluentNHibernate session implementation
	/// </summary>
	public class Session : global::FluentNHibernate.SessionSource, IDbSession
	{
		private ISession m_session;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="config">The FluentNHibernate configuration</param>
		public Session(global::FluentNHibernate.Cfg.FluentConfiguration config)
			: base(config)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="properties">A list of configuration properties</param>
		/// <param name="model">The assembly FluentNHibernate mappings</param>
		public Session(IDictionary<string, string> properties, PersistenceModel model)
			: base(properties, model)
		{
		}

		/// <summary>
		/// Create a new session or return an existing open session
		/// </summary>
		/// <returns>A NHibernate session</returns>
		public override ISession CreateSession()
		{
			if (null == m_session || !m_session.IsOpen)
			{
				m_session = base.CreateSession();
			}

			return m_session;
		}

		/// <summary>
		/// Close the session
		/// </summary>
		public void Close()
		{
			if (null != m_session)
			{
				m_session.Dispose();
				m_session = null;
			}
		}
	}
}
