using System;
using System.Collections.Generic;

namespace Triggerfish.Linq
{
	/// <summary>
	/// Extension methods for IEnumerable
	/// </summary>
    public static class IEnumerableExtensions
    {
		/// <summary>
		/// Iterates over each element in the IEnumerable and invokes the
		/// delegate instance on it
		/// </summary>
		/// <typeparam name="T">The IEnumerable data type</typeparam>
		/// <param name="list">The IEnumerable list of objects</param>
		/// <param name="action">The delegate instance to invoke on each element</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
			if (null == list)
				return;

            foreach(T l in list)
            {
                action(l);
            }
        }
    }
}