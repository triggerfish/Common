using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Generic binder resolver. Will create a binder class for the expected type
	/// (if one has been registered with the resolver function)
	/// </summary>
	public class BinderResolver : DefaultModelBinder
	{
		private Func<Type, IModelBinder> m_resolver;
		private readonly Type m_binderOpenType = typeof(ModelBinder<>);

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="resolver">Resolver function - typically IoC</param>
		public BinderResolver(Func<Type, IModelBinder> resolver)
		{
			m_resolver = resolver;
		}

		/// <summary>
		/// Invoked by Mvc framework to bind the context data to an object
		/// </summary>
		/// <param name="controllerContext">The controller context</param>
		/// <param name="bindingContext">The model binding context</param>
		/// <returns>An object bound from the context data</returns>
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.ModelType.IsValueType)
			{
				return base.BindModel(controllerContext, bindingContext);
			}

			Type t = m_binderOpenType.MakeGenericType(bindingContext.ModelType);
			var binder = m_resolver(t);

			if (null == binder)
			{
				return base.BindModel(controllerContext, bindingContext);
			}

			return binder.BindModel(controllerContext, bindingContext);
		}
	}
}
