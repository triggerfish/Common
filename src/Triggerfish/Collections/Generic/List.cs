using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Collections.Generic
{
	/// <summary>
	/// Custom List implementation providing a read only interface
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class List<T> : System.Collections.Generic.List<T>, IReadOnlyList<T>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public List()
			: base()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="collection">Collection of items to initialise the list with</param>
		public List(IEnumerable<T> collection)
			: base(collection)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="capacity">The initial list capacity</param>
		public List(int capacity)
			: base(capacity)
		{
		}
	}
}
