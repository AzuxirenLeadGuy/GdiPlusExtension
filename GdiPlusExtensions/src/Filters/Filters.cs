using System;
using System.Drawing;

namespace GdiPlusExtensions.Filters
{
	/// <summary>static class for Filter extension method</summary>
	public static class Filters
	{
		/// <summary>Applies filter to an image</summary>
		/// <param name="sourceImage">The image to apply filter upon</param>
		/// <param name="filterType">The Type of filter to use</param>
		/// <returns>The original image, modified by the applied filter</returns>
		public static void Filter(this Bitmap sourceImage, IFilterType filterType)
		{
			filterType.Setup();
			int w = sourceImage.Width, h = sourceImage.Height;
			using (Bitmap filtered = new Bitmap(w, h))
			{
				for (int i = 0; i < w; i++)
				{
					for (int j = 0; j < h; j++)
					{
						filtered.SetPixel(i, j, filterType.ProcessPixelOfKernel(ref sourceImage, i, j));
					}//Filter according to the provided algorithm
				}
				for (int i = 0; i < w; i++)
				{
					for (int j = 0; j < h; j++)
					{
						sourceImage.SetPixel(i, j, filtered.GetPixel(i, j));
					}//Overwrite the source
				}
			}
		}
		/// <summary>Default methods for IFilterType.Setup()</summary>
		/// <param name="filterType">The object to setup</param>
		public static void DefaultSetup(this IFilterType filterType)
		{
			if (filterType.KernelSize < 1) throw new ArgumentException("Kernel Size cannot be less than 1", nameof(filterType));
			else if ((filterType.KernelSize & 1) == 0) throw new ArgumentException("Kernel Size must be odd", nameof(filterType));
		}
	}
}