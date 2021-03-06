using System.Drawing;
using System;
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
		/// <summary>
		/// Applies a function on every pixel of the image
		/// </summary>
		/// <param name="bitmap">The image to affect</param>
		/// <param name="func">The function to apply</param>
		public static void ApplyFuncForeachPixel(this Bitmap bitmap, Func<Color, Color> func)
		{
			int w = bitmap.Width, h = bitmap.Height, i, j;
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					bitmap.SetPixel(i, j, func(bitmap.GetPixel(i, j)));
				}
			}
		}
		/// <summary>
		/// Applies brightness change to the image
		/// </summary>
		/// <param name="bitmap">The image for which the brightness is to be changed</param>
		/// <param name="bf">The brightness factor. Should be in the range [-255, 255]</param>
		public static void AdjustBrightness(this Bitmap bitmap, short bf)
		{
			if (bf >= 255) bf = 255;
			else if (bf <= -255) bf = -255;
			bitmap.ApplyFuncForeachPixel(adjust);
			Color adjust(Color c) => Color.FromArgb(Clamp(c.R + bf), Clamp(c.G + bf), Clamp(c.B + bf));
		}
		/// <summary>
		/// Applies contrast change to the image
		/// </summary>
		/// <param name="bitmap">The image for which the contrast is to be changed</param>
		/// <param name="cf">The contrast factor. Should be in the range [-255, 255]</param>
		public static void AdjustContrast(this Bitmap bitmap, short cf)
		{
			if (cf >= 255) cf = 255;
			else if (cf <= -255) cf = -255;
			float f = 259 * (cf + 255) / (255.0f * (259 - cf));
			bitmap.ApplyFuncForeachPixel(adjust);
			byte Eval(byte x) => Clamp((int)Math.Round(128 + f * (x - 128)));
			Color adjust(Color c) => Color.FromArgb(Eval(c.R), Eval(c.G), Eval(c.B));
		}
		internal static byte Clamp(int x)
		{
			if (x <= 0) return 0;
			else if (x >= 0xff) return 0xff;
			else return (byte)x;
		}
	}
}