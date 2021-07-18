using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace GdiPlusExtensions.Dithering
{
	/// <summary>The static class for generating Color palettes for dithering</summary>
	public static class ColorManager
	{
		/// <summary>1 bit per color: Black and White</summary>
		public static Color[] ColorPalette_1Bit => new[] { Color.Black, Color.White };
		/// <summary>2 bit per color: Red, Blue, Green and White</summary>
		public static Color[] ColorPalette_2Bit => new[] { Color.Red, Color.Blue, Color.Green, Color.Black };
		/// <summary>2 bit per color: Cyan, Magenta, Yellow and Black</summary>
		public static Color[] ColorPalette_2BitCMYK => new[] { Color.Black, Color.Cyan, Color.Magenta, Color.Yellow };
		/// <summary>Provides a color palette with specific number of bits of each RGB color </summary>
		/// <param name="redBits">The bits to use for shades of red</param>
		/// <param name="greenBits">The bits to use for shades of green</param>
		/// <param name="blueBits">The bits to use for shades of blue</param>
		/// <returns>A color palette with 2^(redBits+blueBits+greenBits) colors</returns>
		public static Color[] ColorPalette_nBit(byte redBits, byte greenBits, byte blueBits)
		{
			int len = 1 << (redBits + blueBits + greenBits), i = 0;
			if (len == 0) return new Color[] { };
			var pallete = new Color[len];
			if (redBits > 8 || greenBits > 8 || blueBits > 8) throw new ArgumentOutOfRangeException();
			int RLim = 1 << redBits, BLim = 1 << blueBits, GLim = 1 << greenBits;
			int RMul = GetMul(RLim), GMul = GetMul(GLim), BMul = GetMul(BLim);
			for (int r = 0; r < RLim; r++)
			{
				for (int g = 0; g < GLim; g++)
				{
					for (int b = 0; b < BLim; b++)
					{
						pallete[i++] = Color.FromArgb(GetVal(r, RMul), GetVal(g, GMul), GetVal(b, BMul));
					}
				}
			}
			return pallete;
			int GetMul(int lim) => lim == 1 ? 0 : 256 / (lim - 1);
			int GetVal(int v, int mul) => v == 0 ? 0 : (v * mul - 1);
		}
		/// <summary>Provides a color paletts with specified bits of color for Red, Blue and Green</summary>
		/// <param name="n">The number of bits to use for Red, Blue and Green</param>
		/// <returns>The color palette with 2(3*n) colors of shades of Red, Green and Blue</returns>
		public static Color[] ColorPalette_nBit(byte n)
		{
			if (n == 0) return new Color[] { };
			else if (n == 1) return ColorPalette_1Bit;
			else if (n == 2) return ColorPalette_2Bit;
			int len = 1 << n, i = 0;
			var palette = new Color[len];
			palette[i++] = Color.Black;
			if ((len - 1) % 3 != 0) palette[i++] = Color.White;
			int portion = len / 3;
			for (int j = 1; j <= portion; j++)
			{
				palette[i++] = Color.FromArgb(0, 0, (int)(255.0 * j / portion));
				palette[i++] = Color.FromArgb(0, (int)(255.0 * j / portion), 0);
				palette[i++] = Color.FromArgb((int)(255.0 * j / portion), 0, 0);
			}
			return palette;
		}
		/// <summary>Obtains a palette of all colors used in a given bitmap</summary>
		/// <param name="bitmap">The bitmap to obtain palette from</param>
		/// <returns>Palette of all colors in the given bitmap</returns>
		public static Color[] GetPaletteFromBitmap(Bitmap bitmap)
		{
			var pallete = new SortedSet<Color>(new ColorComparer());
			for (int i = 0, w = bitmap.Width, j, h = bitmap.Height; i < w; i++) { for (j = 0; j < h; j++) pallete.Add(bitmap.GetPixel(i, j)); }
			return pallete.ToArray();
		}
		internal static float ColorDifference(this Color x, Color y)
		{
			int dx = x.R - y.R, dy = x.G - y.G, dz = x.B - y.B;
			return dx * dx + dy * dy + dz * dz;
		}
		private class ColorComparer : IComparer<Color>
		{
			public int Compare(Color a, Color b)
			{
				if (a.R != b.R) return a.R.CompareTo(b.R);
				else if (a.G != b.G) return a.G.CompareTo(b.G);
				else return a.B.CompareTo(b.B);
			}
		}
	}
}