using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish
{
	/// <summary>
	/// Enum specifiying which direction to round a number
	/// </summary>
	public enum RoundDirection
	{
		/// <summary>
		/// Round up
		/// </summary>
		Up,
		/// <summary>
		/// Round down
		/// </summary>
		Down
	}

	/// <summary>
	/// Helper maths methods
	/// </summary>
	public static class Maths
	{
		/// <summary>
		/// Rounds a number to the nearest multiple
		/// </summary>
		/// <param name="number">The number to round</param>
		/// <param name="multiple">The multiple to round to</param>
		/// <param name="direction">The direction to round</param>
		/// <returns>A rounded number</returns>
		public static int RoundToNearest(int number, int multiple, RoundDirection direction)
		{
			double d = number / (double)multiple;
			int i = 0;
			if (direction == RoundDirection.Up)
			{
				i = (int)Math.Ceiling(d);
			}
			else
			{
				i = (int)Math.Floor(d);
			}
			return multiple * i;
		}

		/// <summary>
		/// Converts a percentage to basis points (hundredths of percent)
		/// </summary>
		/// <param name="percentage">The percentage</param>
		/// <returns>The number of basis points</returns>
		public static int ToBasisPoints(decimal percentage)
		{
			return (int)(percentage * 100m);
		}

		/// <summary>
		/// Converts basis points (hundredths of a percent) to a percentage
		/// </summary>
		/// <param name="basisPoints">The number of basis points</param>
		/// <returns>The precentage</returns>
		public static decimal ToPercentage(int basisPoints)
		{
			return basisPoints / 100m;
		}

		/// <summary>
		/// Converts basis points (hundredths of a percent) to a percentage
		/// multiplier that can be applied directly to an amount
		/// e.g. 5000bp = 50% = 0.5 multiplier
		/// </summary>
		/// <param name="basisPoints">The number of basis points</param>
		/// <returns>The precentage multiplier</returns>
		public static decimal ToPercentageMultiplier(int basisPoints)
		{
			return basisPoints / 10000m;
		}
	}
}
