using System;
using System.ComponentModel;
using System.Drawing;

namespace GdiPlusExtensions.Filters
{
	/// <summary>
	/// Filter Type to apply the bilateral filter to an image.
	/// bilateral filtering is applied to the input image, as described in http://www.dai.ed.ac.uk/CVonline/LOCAL_COPIES/MANDUCHI1/Bilateral_Filtering.html
	/// <br/>bilateralFilter can reduce unwanted noise very well while keeping edges fairly sharp. However, it is very slow compared to most filters.
	/// <br/>Sigma values: For simplicity, you can set the 2 sigma values to be the same.
	/// If they are small (less than 10), the filter will not have much effect, whereas if they are large (> 150), they will have a very strong effect, making the image look "cartoonish".
	/// <br/>Filter size: Large filters (d > 5) are very slow, so it is recommended to use d=5 for real-time applications, and perhaps d=9 for offline applications that need heavy noise filtering.
	/// </summary>
	public struct BilateralFilter : IFilterType
	{
		/// <inheritdoc/>
		public string FilterName => "Bilateral Filter";
		/// <summary>
		/// Size of a square k * k kernel. Must be an odd number.<br/>
		/// Not recommended to be taken above 5 for most real-time applications,
		/// as kernel size larger than 5 slows the process. Also, k>9 can be used for heavy noise filtering
		/// </summary>
		/// <value></value>
		[DefaultValue(0x05)] public byte KernelSize { get; set; }
		/// <summary>
		/// Filter sigma in the coordinate space.<br/>
		/// A larger value of the parameter means that farther pixels will influence each other
		/// as long as their colors are close enough.
		/// </summary>
		[DefaultValue(45.0)] public double SigmaSpace;
		/// <summary>
		/// Filter sigma in the color space. <br/>
		/// A larger value of the parameter means that farther colors within the pixel neighborhood
		/// will be mixed together, resulting in larger areas of semi-equal color.
		/// </summary>
		[DefaultValue(45.0)] public double SigmaColor;
		/// <inheritdoc/>
		public void Setup()
		{
			this.DefaultSetup();
			if (SigmaColor <= 1.0) throw new ArgumentException("SigmaColor value is too low");
			if (SigmaSpace <= 1.0) throw new ArgumentException("SigmaSpace value is too low");
		}
		/// <inheritdoc/>
		public Color ProcessPixelOfKernel(ref Bitmap originalImage, int i, int j)
		{
			int kerhalf = KernelSize / 2, w = originalImage.Width - 1, h = originalImage.Height - 1;
			Color currentPixel = originalImage.GetPixel(i, j);
			DoublePixel weightCumulative = new DoublePixel(), filteredDoublePixel = new DoublePixel();
			for (int tx = i - kerhalf, xl = i + kerhalf; tx < xl; tx++)
			{
				for (int ty = j - kerhalf, yl = j + kerhalf; ty < yl; ty++)
				{
					int x = Common.Clamp(tx, 0, w), y = Common.Clamp(ty, 0, h);
					int dx = x - i, dy = y - j;
					double distance = Math.Sqrt(dx * dx + dy * dy);
					Color neighbourPixel = originalImage.GetPixel(x, y);
					DoublePixel gaussianIntensity = new DoublePixel(currentPixel, neighbourPixel, SigmaColor);
					double gaussianSpace = Gaussian(distance, SigmaSpace);
					gaussianIntensity.Multiply(gaussianSpace);
					DoublePixel currentWeight = new DoublePixel();
					(currentWeight.R, currentWeight.G, currentWeight.B) = (gaussianIntensity.R, gaussianIntensity.G, gaussianIntensity.B);
					gaussianIntensity.Multiply(neighbourPixel);
					filteredDoublePixel.Add(gaussianIntensity);
					weightCumulative.Add(currentWeight);
				}
			}
			filteredDoublePixel.Divide(weightCumulative);
			return Color.FromArgb
			(
				(int)Math.Floor(filteredDoublePixel.R),
				(int)Math.Floor(filteredDoublePixel.G),
				(int)Math.Floor(filteredDoublePixel.B)
			);
		}
		private static double Gaussian(double x, double sigma)
		{
			sigma = sigma * sigma * 2;
			return Math.Exp(-(x * x) / sigma) / (Math.PI * sigma);
		}
		private struct DoublePixel
		{
			[DefaultValue(0)] public double R, G, B;
			public DoublePixel(Color current, Color neighbour, double sigmaColor)
			{
				R = Gaussian(neighbour.R - current.R, sigmaColor);
				G = Gaussian(neighbour.G - current.G, sigmaColor);
				B = Gaussian(neighbour.B - current.B, sigmaColor);
			}
			public void Add(DoublePixel other)
			{
				R += other.R;
				G += other.G;
				B += other.B;
			}
			public void Multiply(double other)
			{
				R *= other;
				G *= other;
				B *= other;
			}
			public void Multiply(Color c)
			{
				R *= c.R;
				G *= c.G;
				B *= c.B;
			}
			public void Divide(DoublePixel other)
			{
				R /= other.R;
				G /= other.G;
				B /= other.B;
			}
		}
	}
}