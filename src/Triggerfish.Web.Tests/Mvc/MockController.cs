using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Triggerfish.Web.Tests
{
	internal class MockController : ControllerBase
	{
		protected override void ExecuteCore()
		{
			throw new NotImplementedException();
		}
	}
}
