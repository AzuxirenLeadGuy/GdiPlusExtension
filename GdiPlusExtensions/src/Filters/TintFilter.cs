using System.Drawing;

namespace GdiPlusExtensions.Filters
{
	/// <summary>
	/// Tints the image with respect to the given Color and intensity
	/// </summary>
	public struct TintFilter : IFilterType
	{
		/// <inheritdoc/>
		public string FilterName => "Tinting Filter";
		/// <inheritdoc/>
		public byte KernelSize => 1;
		public Color ColorToTint { get; private set; }
		internal (int R, int G, int B) WeightedColorTint;
		public byte Intensity { get; private set; }
		internal readonly byte Remaining;
		/// <summary>
		///
		/// </summary>
		/// <param name="tintColor"></param>
		/// <param name="intensity"></param>
		public TintFilter(Color tintColor, byte intensity)
		{
			ColorToTint = tintColor;
			Intensity = intensity;
			WeightedColorTint = (Intensity * ColorToTint.R, Intensity * ColorToTint.G, Intensity * ColorToTint.B);
			Remaining = (byte)(255 - Intensity);
		}
		/// <inheritdoc/>
		public Color ProcessPixelOfKernel(ref Bitmap bitmap, int x, int y)
		{
			Color initial = bitmap.GetPixel(x, y);
			(int R, int G, int B) = (initial.R * Remaining, initial.G * Remaining, initial.B * Remaining);
			return Color.FromArgb((R + WeightedColorTint.R) / 255, (G + WeightedColorTint.G) / 255, (B + WeightedColorTint.B) / 255);
		}
		/// <inheritdoc/>
		public void Setup()
		{

		}
	}
}