using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Triggerfish.Linq
{
	/// <summary>
	/// A visitor class to build a path to a class property (eg class.Child.Grandchild.Member)
	/// </summary>
	public class PropertyPathVisitor : ExpressionVisitor
	{
		private List<string> m_paths = new List<string>();

		/// <summary>
		/// Returns the full path to the property
		/// </summary>
		public string Path
		{
			get
			{
				StringBuilder str = new StringBuilder();
				foreach (string path in m_paths)
				{
					if (str.Length == 0)
						str.Append(path);
					else
						str.AppendFormat(".{0}", path);
				}
				return str.ToString();
			}
		}

		/// <summary>
		/// Override to perform custom processing for a method call expression
		/// </summary>
		/// <param name="method">A method call expression</param>
		protected override void VisitMethodCall(MethodCallExpression method)
		{
			throw new NotSupportedException("Method calls are not supported, only property access is allowed");
		}

		/// <summary>
		/// Override to perform custom processing for a method access expression
		/// </summary>
		/// <param name="member">A method access expression</param>
		protected override void VisitMemberAccess(MemberExpression member)
		{
			if (member.Member.MemberType != MemberTypes.Property)
			{
				throw new NotSupportedException(String.Format("{0} calls are not supported, only property access is allowed", member.Member.MemberType.ToString()));
			}

			m_paths.Add(member.Member.Name);

			base.VisitMemberAccess(member);
		}
	}
}
