using System;
using Xamarin.Forms.Maps;

namespace KAS.Trukman
{
	public static class PositionExtensions
	{
		/// <summary>
		/// The equator radius.
		/// </summary>
		public const int EquatorRadius = 6378137;

		/// <summary>
		/// Calculates distance between two locations.
		/// </summary>
		/// <param name="a">Location a</param>
		/// <param name="b">Location b</param>
		/// <returns>The <see cref="System.Double" />The distance in meters</returns>
		public static double DistanceFrom(this Position a, Position b)
		{
			/*
			double distance = Math.Acos(
				(Math.Sin(a.Latitude) * Math.Sin(b.Latitude)) +
				(Math.Cos(a.Latitude) * Math.Cos(b.Latitude))
				Math.Cos(b.Longitude - a.Longitude));
			*/

			var dLat = b.Latitude.DegreesToRadians() - a.Latitude.DegreesToRadians();
			var dLon = b.Longitude.DegreesToRadians() - a.Longitude.DegreesToRadians();

			var a1 = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(a.Latitude.DegreesToRadians()) * Math.Cos(b.Latitude.DegreesToRadians()) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			var distance = 2 * Math.Atan2(Math.Sqrt(a1), Math.Sqrt(1 - a1));

			return EquatorRadius * distance;
		}


		/// <summary>
		/// Degreeses to radians.
		/// </summary>
		/// <param name="deg">The deg.</param>
		/// <returns>System.Double.</returns>
		public static double DegreesToRadians(this double deg)
		{
			return Math.PI * deg / 180.0;
		}
	}
}

