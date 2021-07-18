using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace GdiPlusExtensions.Dithering
{
	/// <summary>This enum is used to specify the color pallete to use for dithering</summary>
	public enum DefaultDitherPalettes : byte
	{
		/// <summary>1 bit per color: Black and White</summary>
		_1Bit,
		/// <summary>2 bit per color: Red, Blue, Green and White</summary>
		_2Bit,
		/// <summary>2 bit per color: Cyan, Magenta, Yellow and Black</summary>
		_2Bit_CMYK,
		/// <summary>3 bit per color: Shades of Red, Blue and Green</summary>
		_3Bit,
		/// <summary>4 bit per color: Shades of Red, Blue and Green</summary>
		_4bit,
		/// <summary>6 bit per color: Shades of Red, Blue and Green</summary>
		_6Bit,
		/// <summary>8 bit per color: Shades of Red, Blue and Green</summary>
		_8Bit
	}
	/// <summary>The static class containing all methods for dithering</summary>
	public static class Dithering
	{
		/// <summary>
		/// Returns a complete color palette for the given DefaultColorpalette enum
		/// </summary>
		/// <param name="p">The DefaultColorPalette enum</param>
		/// <returns>Color[] aray representing the palette of colors</returns>
		public static Color[] GetDefaultColorPalette(DefaultDitherPalettes p)
		{
			switch (p)
			{
				case DefaultDitherPalettes._1Bit: return ColorManager.ColorPalette_1Bit;
				case DefaultDitherPalettes._2Bit: return ColorManager.ColorPalette_2Bit;
				case DefaultDitherPalettes._2Bit_CMYK: return ColorManager.ColorPalette_2BitCMYK;
				case DefaultDitherPalettes._3Bit: return ColorManager.ColorPalette_nBit(1, 1, 1);
				case DefaultDitherPalettes._4bit: return ColorManager.ColorPalette_nBit(4);
				case DefaultDitherPalettes._6Bit: return ColorManager.ColorPalette_nBit(2, 2, 2);
				case DefaultDitherPalettes._8Bit:
				default:
					return ColorManager.ColorPalette_nBit(8);
			}
		}
		/// <summary>
		/// Dithers the given bitmap by selecting a color scheme from the available default color palettes
		/// </summary>
		/// <param name="bitmap">The image to Dither</param>
		/// <param name="algorithm">The Dithering Algorithm to use</param>
		/// <param name="type">The ColorPalette enum</param>
		/// <returns>Dithered bitmap</returns>
		public static void PatterningDither(this Bitmap bitmap, PatterningDitherAlgorithm algorithm, DefaultDitherPalettes type) => PatterningDither(bitmap, algorithm, GetDefaultColorPalette(type));
		/// <summary>Modifies the given bitmap image by applieg dithering on it</summary>
		/// <param name="bitmapimage">The image to dither</param>
		/// <param name="algorithm">The Dithering algorithm used</param>
		/// <param name="palette">The color palette used to dither</param>
		/// <returns>The modified input image with dithering applied on it</returns>
		public static void PatterningDither(this Bitmap bitmapimage, PatterningDitherAlgorithm algorithm, Color[] palette)
		{
			int i, j, w = bitmapimage.Width, h = bitmapimage.Height;
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					Color Orig = bitmapimage.GetPixel(i, j);
					Color ClosestColor = GetClosestColor(ref Orig, ref palette);
					bitmapimage.SetPixel(i, j, ClosestColor);
					foreach (((sbyte X, sbyte Y) Position, byte Numenator) in algorithm.Numenators)
					{
						int newX = i + Position.X, newY = j + Position.Y;
						if (newX < 0 || newX >= w || newY >= h || newY < 0) continue;
						Color col = bitmapimage.GetPixel(newX, newY);
						byte r = (byte)(col.R + (Orig.R - ClosestColor.R) * Numenator / algorithm.Denominator);
						byte g = (byte)(col.G + (Orig.G - ClosestColor.G) * Numenator / algorithm.Denominator);
						byte b = (byte)(col.B + (Orig.B - ClosestColor.B) * Numenator / algorithm.Denominator);
						bitmapimage.SetPixel(newX, newY, Color.FromArgb(r, g, b));
					}
				}
			}
		}
		private static Color GetClosestColor(ref Color x, ref Color[] palette)
		{
			float z = x.ColorDifference(palette[0]), min, temp;
			int l = palette.Length, minindex = 0;
			min = z;
			for (int ii = 1; ii < l && min != 0; ii++)
			{
				temp = x.ColorDifference(palette[ii]);
				if (min > temp)
				{
					min = temp;
					minindex = ii;
				}
			}
			return palette[minindex];
		}
		/// <summary>
		/// Dithers an image using Thresholding, using only Black and White as Color palette.<br/> This method is not suitable for most applications. It is implemented for the sake of completion
		/// </summary>
		/// <param name="bitmap">The image to Dither using Threshold approach</param>
		/// <param name="threshold">The minimum value of square distance a pixel should have to be represented  as white</param>
		public static void ThresholdingDither(this Bitmap bitmap, byte threshold)
		{
			int i, j, w = bitmap.Width, h = bitmap.Height;
			int mindist = threshold * threshold * 3;
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					Color c = bitmap.GetPixel(i, j);
					int dist = c.R * c.R + c.G * c.G + c.B * c.B;
					bitmap.SetPixel(i, j, dist >= mindist ? Color.White : Color.Black);
				}
			}
		}
		/// <summary>
		/// Dithers an image using Random dithering
		/// </summary>
		/// <param name="bitmap">The image to Dither using Random dithering</param>
		public static void RandomDither(this Bitmap bitmap)
		{
			int i, j, w = bitmap.Width, h = bitmap.Height;
			Random random = new Random();
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					Color c = bitmap.GetPixel(i, j);
					int dist = c.R * c.R + c.G * c.G + c.B * c.B;
					bitmap.SetPixel(i, j, dist >= random.Next(195075) ? Color.White : Color.Black);
				}
			}
		}
		/// <summary>
		///
		/// </summary>
		/// <param name="bitmap"></param>
		/// <param name="bayerMatrixLength"></param>
		/// <param name="colorOutput"></param>
		public static void OrderedDithering(this Bitmap bitmap, byte bayerMatrixLength = 2, bool colorOutput = false)
		{
			if (bayerMatrixLength < 2) throw new ArgumentException("Length for Bayer Matrix should be at least 2", nameof(bayerMatrixLength));
			else if ((bayerMatrixLength & (bayerMatrixLength - 1)) != 0) throw new ArgumentException("Expected the Bayer matrix length to be in powers of 2", nameof(bayerMatrixLength));
			else if (bayerMatrixLength > 8) throw new ArgumentException("Bayer Matrix Length should not be larger than 8", nameof(bayerMatrixLength));
			int w = bitmap.Width, h = bitmap.Height;
			float[,] matrix = new float[bayerMatrixLength, bayerMatrixLength];//matrix[0, 0]=0;
			for (int i = 1; i < bayerMatrixLength; i <<= 1)
			{
				for (int x = 0; x < i; x++)
				{
					for (int y = 0; y < i; y++)
					{
						matrix[x, y] *= 4.0f;
						matrix[x + i, y + i] = matrix[x, y];
						matrix[x, y + i] = ++matrix[x + i, y + i];
						matrix[x + i, y] = ++matrix[x, y + i];
						matrix[x + i, y]++;
					}
				}
			}
			for (int i = 0; i < bayerMatrixLength; i++)
			{
				for (int j = 0; j < bayerMatrixLength; j++)
				{
					matrix[i, j] *= 256 / (bayerMatrixLength * bayerMatrixLength);
				}
			}
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					int threshold = (int)matrix[i % bayerMatrixLength, j % bayerMatrixLength];
					Color pixel = bitmap.GetPixel(i, j);
					if (colorOutput)
					{
						bitmap.SetPixel(i, j, Color.FromArgb(pixel.R < threshold ? 0 : 255, pixel.G < threshold ? 0 : 255, pixel.B < threshold ? 0 : 255));
					}
					else
					{
						threshold *= 3 * threshold;
						int intensity = pixel.R * pixel.R + pixel.G * pixel.G + pixel.B * pixel.B;
						bitmap.SetPixel(i, j, intensity < threshold ? Color.Black : Color.White);
					}
				}
			}
		}
	}
}