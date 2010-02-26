using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Triggerfish.Collections.Generic
{
	/// <summary>
	/// Interface for a read only list
	/// </summary>
	/// <typeparam name="T">The type of object the list contains</typeparam>
	public interface IReadOnlyList<T> : IEnumerable<T>, IEnumerable
	{
		/// <summary>
		/// Returns the number of items in the list
		/// </summary>
		int Count { get; }

		/// <summary>
		/// List indexer property
		/// </summary>
		/// <param name="index">The index in the list</param>
		/// <returns>The item at the specified index</returns>
		T this[int index] { get; }

		/// <summary>
		/// Tests if the list contains a specific item
		/// </summary>
		/// <param name="item">The item to test for</param>
		/// <returns>True if the list contains the item, false otherwise</returns>
		bool Contains(T item);

		/// <summary>
		/// Gets the index of a specific item in the list
		/// </summary>
		/// <param name="item">The item to lookup</param>
		/// <returns>The index into the list</returns>
		int IndexOf(T item);
	}
}
