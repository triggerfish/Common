using Ninject.Modules;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using System.Reflection;
using Triggerfish.FluentNHibernate;

namespace Triggerfish.NHibernate.Ninject
{
	/// <summary>
	/// NHibernate Ninject dependency module
	/// </summary>
	/// <typeparam name="T">A type from the assembly containing FluentNHibernate mappings</typeparam>
	public abstract class DatabaseModule<T> : NinjectModule
	{
		/// <summary>
		/// Module Load handler
		/// </summary>
		public override void Load()
		{
			log4net.Config.XmlConfigurator.Configure();

			ValidatorEngine ve = new ValidatorEngine();
			FluentConfiguration cfg = Fluently.Configure()
										.Database(CreateDatabase())
										.Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
										.ExposeConfiguration(c => ConfigureValidator(c, ve));

			Bind<IDbSession>()
				.To<Session>()
				.InRequestScope()
				.WithConstructorArgument("config", cfg);

			Bind<Triggerfish.Validator.IValidator>()
				.To<Triggerfish.NHibernate.Validator.Validator>()
				.InRequestScope()
				.WithConstructorArgument("engine", ve);
		}

		/// <summary>
		/// Derived classes must implement to provide the specific
		/// database configuration data
		/// </summary>
		/// <returns>Database configuration data</returns>
		protected abstract IPersistenceConfigurer CreateDatabase();

		/// <summary>
		/// Derived classes must implement to perform custom Ninject bindings
		/// </summary>
		protected abstract void SetupBindings();

		private void ConfigureValidator(global::NHibernate.Cfg.Configuration a_config, ValidatorEngine a_engine)
		{
			XmlConfiguration nhvc = new XmlConfiguration();
			nhvc.Properties[global::NHibernate.Validator.Cfg.Environment.ApplyToDDL] = "true";
			nhvc.Properties[global::NHibernate.Validator.Cfg.Environment.AutoregisterListeners] = "true";
			nhvc.Properties[global::NHibernate.Validator.Cfg.Environment.ValidatorMode] = "UseAttribute";

			a_engine.Configure(nhvc);
			ValidatorInitializer.Initialize(a_config, a_engine);
		}
	}
}
