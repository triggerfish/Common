using System.Reflection;
using System.Collections.Generic;
using System;
using FluentNHibernate.Conventions;

namespace Triggerfish.NHibernate
{
	internal class FKConstraintNameConvention : IHasManyConvention
	{
		public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
		{
			instance.Key.ForeignKey(String.Format("FK_{0}_{1}", instance.Member.Name, instance.EntityType.Name));
		}
	}
}
