using System.Reflection;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using System;

namespace Triggerfish.NHibernate
{
	/// <summary>
	/// NHibernate configuration
	/// </summary>
	public class Configuration
	{
		private IDatabaseConfiguration m_database;

		/// <summary>
		/// The FluentNHibernate configuration
		/// </summary>
		public FluentConfiguration Config { get; private set; }

		/// <summary>
		/// The validator engine
		/// </summary>
		public ValidatorEngine Validator { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="database">The database configuration object</param>
		public Configuration(IDatabaseConfiguration database)
		{
			m_database = database;
		}

		/// <summary>
		/// Create the configuration
		/// </summary>
		public virtual void Create<T>()
		{
			log4net.Config.XmlConfigurator.Configure();

			Config = Fluently.Configure()
						.Database(m_database.Create(typeof(T).Assembly))
						.Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>());
		}

		/// <summary>
		/// Create the NHibernate ValidatorEngine
		/// </summary>
		public virtual void CreateValidator()
		{
			if (null != Config)
			{
				ValidatorEngine ve = new ValidatorEngine();
				Config.ExposeConfiguration(c => ConfigureValidator(c, ve));
				Validator = ve;
			}
		}

		/// <summary>
		/// Sets the properties of the NHibernate validator engine
		/// </summary>
		/// <param name="properties">Dictionary of properties</param>
		protected virtual void SetValidatorEngineProperties(IDictionary<string, string> properties)
		{
			properties[global::NHibernate.Validator.Cfg.Environment.ApplyToDDL] = "true";
			properties[global::NHibernate.Validator.Cfg.Environment.AutoregisterListeners] = "false";
			properties[global::NHibernate.Validator.Cfg.Environment.ValidatorMode] = "UseAttribute";
		}

		private void ConfigureValidator(global::NHibernate.Cfg.Configuration config, ValidatorEngine engine)
		{
			XmlConfiguration nhvc = new XmlConfiguration();
			SetValidatorEngineProperties(nhvc.Properties);
			engine.Configure(nhvc);
			ValidatorInitializer.Initialize(config, engine);
		}
	}
}
