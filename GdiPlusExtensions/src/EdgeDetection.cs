using System.Drawing;
using System;
using GdiPlusExtensions.Filters;
namespace GdiPlusExtensions
{
	public static class EdgeDetection
	{
		private static ConvolutionFilter _gX = new ConvolutionFilter
		(
			"Sobel-X",
			new int[,]
			{
				{ -1, -2, -1 },
				{ 0, 0, 0 },
				{ 1, 2, 1 }
			},
			1
		);
		private static ConvolutionFilter _gY = new ConvolutionFilter
		(
			"Sobel-Y",
			new int[,]
			{
				{ -1, 0, 1 },
				{ -2, 0, 2 },
				{ -1, 0, 1 }
			},
			1
		);
		public static Bitmap SobelEdgeDetection(this Bitmap bitmap)
		{
			bitmap.Grayscale();
			bitmap.Filter(ConvolutionFilter.GaussianFilter);
			int w = bitmap.Width, h = bitmap.Height, i, j;
			Bitmap b = new Bitmap(w, h);
			double gx, gy, GMax = 0;
			double[,] Img = new double[w, h];
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					gx = _gX.ProcessPixelOfKernel(ref bitmap, i, j).R;
					gy = _gY.ProcessPixelOfKernel(ref bitmap, i, j).G;
					Img[i, j] = Math.Sqrt(gx * gx + gy * gy);
					if (Img[i, j] > GMax) GMax = Img[i, j];
				}
			}
			byte c;
			for (i = 0; i < w; i++)
			{
				for (j = 0; j < h; j++)
				{
					c = (byte)Math.Round(Img[i, j] * 255 / GMax);
					b.SetPixel(i, j, Color.FromArgb(c, c, c));
				}
			}
			return b;
		}
	}
}