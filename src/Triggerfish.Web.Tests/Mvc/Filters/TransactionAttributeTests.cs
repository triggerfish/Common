using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Triggerfish.Web.Mvc;
using Triggerfish.Web.Testing;
using System.Web;
using Triggerfish.Database;

namespace Triggerfish.Web.Tests
{
	[TestClass]
	public class TransactionAttributeTests
	{
		private MockController m_controller = new MockController();
		private ResultExecutedContext m_context = new ResultExecutedContext();

		[TestMethod]
		public void ShouldNotCommitIfModelStateInvalid()
		{
			// arrange
			Resolver.Get();
			TransactionAttribute.FactoryResolver = Resolver.Get;
			TransactionAttribute attr = new TransactionAttribute();
			m_context.Controller.ViewData.ModelState.AddModelError("Data", "Error");
			//Resolver.Reset();

			// act
			attr.OnResultExecuted(m_context);

			// assert
			Resolver.Factory.Verify(x => x.GetCurrentUnitOfWork(), Times.Never());
		}

		[TestMethod]
		public void ShouldNotCommitIfNullUoW()
		{
			// arrange
			Resolver.Get();
			TransactionAttribute.FactoryResolver = Resolver.Get;
			TransactionAttribute attr = new TransactionAttribute();
			//Resolver.Reset();
			Resolver.ReturnNullUoW = true;

			// act
			attr.OnResultExecuted(m_context);

			// assert
			Resolver.Factory.Verify(x => x.GetCurrentUnitOfWork());
		}

		[TestMethod]
		public void ShouldCommit()
		{
			// arrange
			TransactionAttribute.FactoryResolver = Resolver.Get;
			TransactionAttribute attr = new TransactionAttribute();
			Resolver.Reset();

			// act
			attr.OnResultExecuted(m_context);

			// assert
			Resolver.Factory.Verify(x => x.GetCurrentUnitOfWork());
			Resolver.UoW.Verify(x => x.Commit());
		}

		[TestInitialize]
		public void SetupTest()
		{
			m_controller.ViewData = new ViewDataDictionary();
			m_context.Controller = m_controller;
		}
	}

	internal class Resolver
	{
		public static bool Called { get; set; }
		public static bool ReturnNull { get; set; }
		public static bool ReturnNullUoW { get; set; }
		public static Mock<IUnitOfWorkFactory> Factory { get; private set;  }
		public static Mock<IUnitOfWork> UoW { get; private set; }
		
		public static void Reset()
		{
			ReturnNullUoW = ReturnNull = Called = false;
			Factory = null;
			UoW = null;
		}

		public static IUnitOfWorkFactory Get()
		{
			IUnitOfWorkFactory factory = null;
			if (!ReturnNull)
			{
				Factory = new Mock<IUnitOfWorkFactory>();
				IUnitOfWork uow = null;
				if (!ReturnNullUoW)
				{
					UoW = new Mock<IUnitOfWork>();
					uow = UoW.Object;
				}

				Factory.Setup(x => x.GetCurrentUnitOfWork()).Returns(uow);
				factory = Factory.Object;
			}

			return factory;
		}
	}
}
