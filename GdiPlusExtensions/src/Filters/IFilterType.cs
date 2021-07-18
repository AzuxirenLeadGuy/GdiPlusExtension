using System.Drawing;

namespace GdiPlusExtensions.Filters
{
	/// <summary>Represets a Filter Type. </summary>
	public interface IFilterType
	{
		/// <summary>The name of this filter</summary>
		/// <value>string value of the filter name</value>
		string FilterName { get; }
		/// <summary>Size of the square kernel. Must be odd</summary>
		byte KernelSize { get; }
		/// <summary>Describes what the filter must do with its kernel at the given pixel locations</summary>
		/// <param name="bitmap">The bitmap to process</param>
		/// <param name="x">The x-coordinate of the location of kernel</param>
		/// <param name="y">The y-coordinate of the location of kernel</param>
		/// <returns>The color on processing the kernel for the given pixel</returns>
		Color ProcessPixelOfKernel(ref Bitmap bitmap, int x, int y);
		/// <summary>Validates and prepares the filter and throws Exception if invalid</summary>
		void Setup();
	}
}