using System.Drawing;

namespace GdiPlusExtensions.Filters
{
	/// <summary>
	/// These filters process the image based on the process of convolution
	/// </summary>
	public struct ConvolutionFilter : IFilterType
	{
		/// <inheritdoc/>
		public string FilterName { get; private set; }
		/// <inheritdoc/>
		public byte KernelSize => (byte)_kernel.GetLength(0);
		private readonly int[,] _kernel;
		private readonly int _denominator;
		/// <summary>
		/// For creating a custom Convolution Filter
		/// </summary>
		/// <param name="name">The name for the filter</param>
		/// <param name="kernel">The rectangular kernel matrix</param>
		/// <param name="den">The common denominator</param>
		public ConvolutionFilter(string name, int[,] kernel, int den)
		{
			FilterName = name;
			_kernel = kernel;
			_denominator = den;
		}
		/// <inheritdoc/>
		public Color ProcessPixelOfKernel(ref Bitmap bitmap, int x, int y)
		{
			int w = bitmap.Width - 1, h = bitmap.Height - 1, kh = KernelSize / 2;
			(int R, int G, int B) = (0, 0, 0);
			for (int i1 = x - kh, i2 = KernelSize - 1, il = x + kh; i1 <= il; i1++, i2--)
			{
				for (int j1 = y - kh, j2 = KernelSize - 1, jl = y + kh; j1 < jl; j1++, j2--)
				{
					int i = Common.Clamp(i1, 0, w), j = Common.Clamp(j1, 0, h);
					Color c = bitmap.GetPixel(i, j);
					float val = _kernel[i2, j2] / (float)_denominator;
					R += (int)(val * c.R);
					G += (int)(val * c.G);
					B += (int)(val * c.B);
				}
			}
			return Color.FromArgb((byte)R, (byte)G, (byte)B);
		}
		/// <inheritdoc/>
		public void Setup() => Filters.DefaultSetup(this);
		/// <summary>
		/// a spatial domain linear filter in which each pixel in the resulting image has a value equal to the average value of its neighboring pixels in the input image. It is a form of low-pass ("blurring") filter.
		/// </summary>
		public static ConvolutionFilter BoxBlur => new ConvolutionFilter
		(
			"Box Blur Filter",
			new int[3, 3]
			{
				{1,1,1},
				{1,1,1},
				{1,1,1}
			},
			9
		);
		/// <summary>
		/// a Gaussian blur (also known as Gaussian smoothing)
		/// is the result of blurring an image by a Gaussian function.<br/>
		/// It is a widely used effect in graphics software, typically to reduce image noise
		/// and reduce detail.
		/// The visual effect of this blurring technique is a smooth blur resembling that of
		/// viewing the image through a translucent screen, distinctly different from the
		/// bokeh effect produced by an out-of-focus lens or the
		/// shadow of an object under usual illumination.
		/// </summary>
		public static ConvolutionFilter GaussianFilter => new ConvolutionFilter
		(
			"Gaussian Blur Filter",
			new int[3, 3]
			{
				{1,2,1},
				{2,4,2},
				{1,2,1}
			},
			16
		);
	}
}