using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Triggerfish.Linq
{
	/// <summary>
	/// A visitor class to crawl over each node in an expression tree
	/// </summary>
	public abstract class ExpressionVisitor
	{
		/// <summary>
		/// Visits a node in the expression tree and recurses into 
		/// child nodes where they exist
		/// </summary>
		/// <param name="exp">The expression</param>
		public virtual void Visit(Expression exp)
		{
			if (exp == null) return;

			switch (exp.NodeType)
			{
				case ExpressionType.Negate:
					break;
				case ExpressionType.NegateChecked:
					break;
				case ExpressionType.Not:
					break;
				case ExpressionType.Convert:
					break;
				case ExpressionType.ConvertChecked:
					break;
				case ExpressionType.ArrayLength:
					break;
				case ExpressionType.Quote:
					break;
				case ExpressionType.TypeAs:
					break;
				case ExpressionType.Add:
					break;
				case ExpressionType.AddChecked:
					break;
				case ExpressionType.Subtract:
					break;
				case ExpressionType.SubtractChecked:
					break;
				case ExpressionType.Multiply:
					break;
				case ExpressionType.MultiplyChecked:
					break;
				case ExpressionType.Divide:
					break;
				case ExpressionType.Modulo:
					break;
				case ExpressionType.And:
					break;
				case ExpressionType.AndAlso:
					break;
				case ExpressionType.Or:
					break;
				case ExpressionType.OrElse:
					break;
				case ExpressionType.LessThan:
					break;
				case ExpressionType.LessThanOrEqual:
					break;
				case ExpressionType.GreaterThan:
					break;
				case ExpressionType.GreaterThanOrEqual:
					break;
				case ExpressionType.Equal:
					break;
				case ExpressionType.NotEqual:
					break;
				case ExpressionType.Coalesce:
					break;
				case ExpressionType.ArrayIndex:
					break;
				case ExpressionType.RightShift:
					break;
				case ExpressionType.LeftShift:
					break;
				case ExpressionType.ExclusiveOr:
					break;
				case ExpressionType.TypeIs:
					break;
				case ExpressionType.Conditional:
					break;
				case ExpressionType.Constant:
					break;
				case ExpressionType.Parameter:
					break;
				case ExpressionType.MemberAccess:
					VisitMemberAccess((MemberExpression)exp);
					break;
				case ExpressionType.Call:
					VisitMethodCall((MethodCallExpression)exp);
					break;
				case ExpressionType.Lambda:
					VisitLambda((LambdaExpression)exp);
					break;
				case ExpressionType.New:
					break;
				case ExpressionType.NewArrayInit:
					break;
				case ExpressionType.NewArrayBounds:
					break;
				case ExpressionType.Invoke:
					break;
				case ExpressionType.MemberInit:
					break;
				case ExpressionType.ListInit:
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Override to perform custom processing for a lambda expression
		/// </summary>
		/// <param name="lambda">A lambda expression</param>
		protected virtual void VisitLambda(LambdaExpression lambda)
		{
			Visit(lambda.Body);
		}

		/// <summary>
		/// Override to perform custom processing for a method call expression
		/// </summary>
		/// <param name="method">A method call expression</param>
		protected virtual void VisitMethodCall(MethodCallExpression method)
		{
			Visit(method.Object);
		}

		/// <summary>
		/// Override to perform custom processing for a method access expression
		/// </summary>
		/// <param name="member">A method access expression</param>
		protected virtual void VisitMemberAccess(MemberExpression member)
		{
			Visit(member.Expression);
		}
	}
}
