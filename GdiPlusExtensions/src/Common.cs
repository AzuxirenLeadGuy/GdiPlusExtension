using System.Drawing;

namespace GdiPlusExtensions
{
	/// <summary>This static class contains extension functions for Bitmap</summary>
	public static class Common
	{
		/// <summary>Creates a deep copy of a bitmap</summary>
		/// <param name="otherBitmap">The bitmap to copy</param>
		/// <returns>New Bitmap object that is a deep copy of the given parameter</returns>
		public static Bitmap DeepClone(this Bitmap otherBitmap)
		{
			int w = otherBitmap.Width, h = otherBitmap.Height;
			Bitmap copy = new Bitmap(w, h);
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					copy.SetPixel(i, j, otherBitmap.GetPixel(i, j));
				}
			}
			return copy;
		}
		/// <summary>Returns a value by clamping it in the specified bound</summary>
		/// <param name="value">The value to check if it is in the specified bound</param>
		/// <param name="min">The lowerbound</param>
		/// <param name="max">The upperbound</param>
		/// <returns>Returns the value if is inside bounds, else returns either min or max, depending on which is closer to the value</returns>
		public static int Clamp(int value, int min, int max)
		{
			if (value <= min) return min;
			else if (value >= max) return max;
			else return value;
		}
	}
}