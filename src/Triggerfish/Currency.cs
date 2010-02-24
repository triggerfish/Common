using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish
{
	/// <summary>
	/// Helper methods for converting to pounds and pence
	/// </summary>
	public static class Currency
	{
		/// <summary>
		/// Converts a raw decimal pounds and pence amount to the nearest penny
		/// </summary>
		/// <param name="rawPoundsAndPence">Raw decimal pounds and pence value</param>
		/// <param name="direction">Whether to round down or up to the nearest penny</param>
		/// <returns>The number of whole pounds and pennies</returns>
		public static decimal RoundToPoundsAndPence(decimal rawPoundsAndPence, RoundDirection direction)
		{
			return ToPence(rawPoundsAndPence, direction) / 100m;
		}

		/// <summary>
		/// Converts a raw decimal pence amount into pounds and pence
		/// </summary>
		/// <param name="rawAmount">Raw decimal pence amount</param>
		/// <returns>The number of whole pounds and pennies</returns>
		public static decimal ToPoundsAndPence(decimal rawPence)
		{
			return rawPence / 100m;
		}

		/// <summary>
		/// Converts a raw decimal pounds and pence to pennies
		/// </summary>
		/// <param name="rawPoundsAndPence">Raw decimal pounds and pence value</param>
		/// <param name="direction">Whether to round down or up to the nearest penny</param>
		/// <returns>The number of whole pennies in the amount</returns>
		public static int ToPence(decimal rawPoundsAndPence, RoundDirection direction)
		{
			decimal d = rawPoundsAndPence * 100m;
			if (direction == RoundDirection.Down)
			{
				return (int)Math.Truncate(d);
			}
			else
			{
				return (int)Math.Ceiling(d);
			}
		}
	}
}
