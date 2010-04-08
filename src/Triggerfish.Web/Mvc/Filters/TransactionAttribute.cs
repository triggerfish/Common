using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Database;

namespace Triggerfish.Web.Mvc
{
	/// <summary>
	/// Filter attribute for a transaction. After the result has executed, the current
	/// unit of work will be committed
	/// </summary>
	public class TransactionAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// A resolver function to get the current IUnitOfWorkFactory - typically IoC
		/// </summary>
		public static Func<IUnitOfWorkFactory> FactoryResolver { get; set; }

		/// <summary>
		/// The handler when the result has executed
		/// </summary>
		/// <param name="filterContext">The filter context</param>
		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			base.OnResultExecuted(filterContext);

			if (filterContext.Controller.ViewData.ModelState.IsValid && FactoryResolver != null)
			{
				IUnitOfWorkFactory factory = FactoryResolver();
				if (factory != null)
				{
					IUnitOfWork uow = factory.GetCurrentUnitOfWork();
					if (uow != null)
					{
						uow.Commit();
					}
				}
			}
		}
	}
}
