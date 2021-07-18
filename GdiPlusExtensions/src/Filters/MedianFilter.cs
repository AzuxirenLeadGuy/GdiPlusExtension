using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace GdiPlusExtensions.Filters
{
	/// <summary>static class for MedianFilter extension method</summary>
	public struct MedianFilter : IFilterType
	{
		/// <inheritdoc/>
		public string FilterName => "Median Filter";
		/// <inheritdoc/>
		[DefaultValue(0x03)] public byte KernelSize { get; set; }
		/// <inheritdoc/>
		public Color ProcessPixelOfKernel(ref Bitmap source, int x, int y)
		{
			List<byte> r = new List<byte>(), g = new List<byte>(), b = new List<byte>();
			int kerhalf = KernelSize >> 1, w = source.Width - 1, h = source.Height - 1;
			for (int tx = x - kerhalf, xl = x + kerhalf; tx < xl; tx++)
			{
				for (int ty = y - kerhalf, yl = y + kerhalf; ty < yl; ty++)
				{
					int i = Common.Clamp(tx, 0, w), j = Common.Clamp(ty, 0, h);
					Color color = source.GetPixel(i, j);
					r.Add(color.R);
					g.Add(color.G);
					b.Add(color.B);
				}
			}
			kerhalf = KernelSize * KernelSize / 2;
			r.Sort();
			g.Sort();
			b.Sort();
			return Color.FromArgb(r[kerhalf], g[kerhalf], b[kerhalf]);
		}
		/// <inheritdoc/>
		public void Setup() => this.DefaultSetup();
	}
}