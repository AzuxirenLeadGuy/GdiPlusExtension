using GdiPlusExtensions;
using GdiPlusExtensions.Dithering;
using GdiPlusExtensions.Filters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Example
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("The path to image is required");
			}
			Bitmap img;
			try
			{
				img = Image.FromFile(args[0]) as Bitmap;
			}
			catch
			{
				Console.WriteLine($"Given image file path is invalid!");
				return;
			}
			string DirPath = Directory.GetParent(args[0]).FullName;
			string filename = Path.GetFileNameWithoutExtension(args[0]);
			#region FilterShowcase
			Console.WriteLine("Beginning filtering showcase");
			DirectoryInfo fdinfo = Directory.CreateDirectory($"{DirPath}/FilteredImages");
			IFilterType[] filters = new IFilterType[]
			{
				ConvolutionFilter.BoxBlur,
				ConvolutionFilter.GaussianFilter,
				new BilateralFilter() {KernelSize=9, SigmaColor=50, SigmaSpace=40},
				new MedianFilter() {KernelSize=7},
				new TintFilter(Color.Yellow, 80),
			};
			foreach (IFilterType filter in filters)
			{
				using Bitmap copy = img.DeepClone();
				string filepath = $"{fdinfo.FullName}/{filename}-{filter.FilterName.Replace(' ', '_')}.png";
				Console.WriteLine($"Saving file {filepath}");
				copy.Filter(filter);
				copy.Save(filepath, ImageFormat.Png);
			}
			Console.WriteLine("Filter showcase complete!");
			#endregion
			#region DitheringShowcase
			Console.WriteLine("Beginning dithering showcase\n-----------------------------");
			DirectoryInfo ditherfolder = Directory.CreateDirectory($"{DirPath}/Dithering");
			using (Bitmap copy = img.DeepClone())
			{
				copy.ThresholdingDither(96);
				string path = $"{ditherfolder}/Thresholding.png";
				Console.WriteLine($"Saving {path}");
				copy.Save(path, ImageFormat.Png);
			}
			using (Bitmap copy = img.DeepClone())
			{
				copy.RandomDither();
				string path = $"{ditherfolder}/RandomDithering.png";
				Console.WriteLine($"Saving {path}");
				copy.Save(path, ImageFormat.Png);
			}
			using (Bitmap copy = img.DeepClone())
			{
				copy.OrderedDithering(4, false);
				string path = $"{ditherfolder}/Ordered4x4-bw.png";
				Console.WriteLine($"Saving {path}");
				copy.Save(path, ImageFormat.Png);
			}
			using (Bitmap copy = img.DeepClone())
			{
				copy.OrderedDithering(8, true);
				string path = $"{ditherfolder}/Ordered8x8-color.png";
				Console.WriteLine($"Saving {path}");
				copy.Save(path, ImageFormat.Png);
			}
			using (Bitmap copy = img.DeepClone())
			{
				copy.PatterningDither(PatterningDitherAlgorithm.Atkinson, ColorManager.ColorPalette_nBit(0, 0, 4));
				string path = $"{ditherfolder}/Dithered-custom-palette.png";
				Console.WriteLine($"Saving {path}");
				copy.Save(path, ImageFormat.Png);
			}
			Console.WriteLine("Pattern dithering algorithms:");
			PatterningDitherAlgorithm[] AlgoList = new PatterningDitherAlgorithm[]
			{
					PatterningDitherAlgorithm.Atkinson,
					PatterningDitherAlgorithm.Brukes,
					PatterningDitherAlgorithm.FalseFloydSteinberg,
					PatterningDitherAlgorithm.FloydSteinberg,
					PatterningDitherAlgorithm.JarvisJudeNinke,
					PatterningDitherAlgorithm.Sierra,
					PatterningDitherAlgorithm.SierraLite,
					PatterningDitherAlgorithm.SierraTwoRow,
					PatterningDitherAlgorithm.Stucki
			};
			DefaultDitherPalettes[] PaletteList = Enum.GetValues(typeof(DefaultDitherPalettes)) as DefaultDitherPalettes[];
			foreach (PatterningDitherAlgorithm algo in AlgoList)
			{
				DirectoryInfo dinfo = Directory.CreateDirectory($"{ditherfolder.FullName}/Dithered-{algo.Title.Replace(' ', '_')}");
				foreach (DefaultDitherPalettes pal in PaletteList)
				{
					using Bitmap copy = img.DeepClone();
					string filepath = $"{dinfo.FullName}/{filename}-Dithered-{algo.Title.Replace(' ', '_')}-{Enum.GetName(pal)}.png";
					Console.WriteLine($"Saving file {filepath}");
					copy.PatterningDither(algo, pal);
					copy.Save(filepath, ImageFormat.Png);
				}
			}
			Console.WriteLine("Dithering showcase complete!");
			#endregion
			#region ColorLerpShowcase
			DirectoryInfo ldinfo = Directory.CreateDirectory($"{DirPath}/ScaledImages");
			(Color, Color)[] CustomScaleArgs = 
			{
				(Color.Black, Color.White),
				(Color.Black, Color.Red),
				(Color.DarkGreen, Color.GreenYellow),
			};
			foreach((Color, Color) tuple in CustomScaleArgs)
			{
				using Bitmap copy = img.DeepClone();
				copy.CustomScale(tuple.Item1, tuple.Item2);
				string filepath = $"{ldinfo.FullName}/{filename}-{tuple}.png";
				Console.WriteLine($"Saving file {filepath}");
				copy.Save(filepath, ImageFormat.Png);
			}
			#endregion
			img.Dispose();
		}
	}
}
