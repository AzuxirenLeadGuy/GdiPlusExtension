using System;
using System.Drawing;
namespace GdiPlusExtensions
{
	/// <summary>
	/// Extension methods for Color scaling and Lerp operations
	/// </summary>
	public static class ColorScaling
	{
		/// <summary>
		/// Returns the Color obtained using Linear Interpolation(Lerp)
		/// between the two colors.
		///
		/// </summary>
		/// <param name="x">Color at value=0</param>
		/// <param name="y">Color at value=1</param>
		/// <param name="value">a float value between 0 and 1 (inclusive)</param>
		/// <returns></returns>
		public static Color Lerp(Color x, Color y, float value)
		{
			if (value < 0 || value > 1)
				throw new ArgumentException($"Lerp value should be between 0 and 1 (inclusive), but recieved vaiue={value}", nameof(value));
			byte r = (byte)(x.R + value * (y.R - x.R)), g = (byte)(x.G + value * (y.G - x.G)), b = (byte)(x.B + value * (y.B - x.B));
			return Color.FromArgb(r, g, b);
		}
		/// <summary>
		/// Returns an image of which every pixel is
		/// interpolated between given two colors and
		/// the interpolation value is their intensity
		/// </summary>
		/// <param name="bitmap">The image to interpolate</param>
		/// <param name="x">Interpolation color</param>
		/// <param name="y">Interpolation color</param>
		/// <returns>Grayscaled image</returns>
		public static Bitmap CustomScale(this Bitmap bitmap, Color x, Color y)
		{
			int w = bitmap.Width, h = bitmap.Height, i, j;
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					bitmap.SetPixel(i, j, Lerp(x, y, Dist(bitmap.GetPixel(i, j))));
				}
			}
			return bitmap;
			float Dist(Color color)
			{
				byte r = color.R, g = color.G, b = color.B;
				float scaled = r * r + g * g + b * b;
				return scaled / 195075;
			}
		}
		/// <summary>
		/// Returns grayscaled image
		/// </summary>
		/// <param name="bitmap">Image to grayscale</param>
		/// <returns>Grayscaled image</returns>
		public static Bitmap Grayscale(this Bitmap bitmap) => CustomScale(bitmap, Color.Black, Color.White);
	}
}