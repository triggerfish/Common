using System;
using System.Collections.Generic;
using Triggerfish.Collections.Generic;

namespace Triggerfish.Linq
{
	/// <summary>
	/// Extension methods for IList
	/// </summary>
    public static class IListExtensions
    {
		/// <summary>
		/// Tests whether or not the given index is in bounds
		/// </summary>
		/// <typeparam name="T">The IList data type</typeparam>
		/// <param name="list">The list of objects</param>
		/// <param name="index">The index to test</param>
		/// <returns>True if the index is in the list bounds, false otherwise</returns>
        public static bool InBounds<T>(this IList<T> list, int index)
        {
			if (null == list)
				return false;

			return index >= 0 && index < list.Count;
		}

		/// <summary>
		/// Tests whether or not the given index is in bounds
		/// </summary>
		/// <typeparam name="T">The IReadOnlyList data type</typeparam>
		/// <param name="list">The list of objects</param>
		/// <param name="index">The index to test</param>
		/// <returns>True if the index is in the list bounds, false otherwise</returns>
		public static bool InBounds<T>(this IReadOnlyList<T> list, int index)
		{
			if (null == list)
				return false;

			return index >= 0 && index < list.Count;
		}
	}
}