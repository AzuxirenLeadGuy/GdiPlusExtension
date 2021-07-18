namespace GdiPlusExtensions.Dithering
{
	/// <summary>Describes the mechanism to dither</summary>
	public struct PatterningDitherAlgorithm
	{
		/// <summary>The name of this algorithm</summary>
		public string Title;
		/// <summary>The denominator of the kernel/matrix</summary>
		public byte Denominator;
		/// <summary>Tuples of non-zero elements and their positions</summary>
		public ((sbyte X, sbyte Y) Position, byte Numenator)[] Numenators;
		/// <summary>Floyd Steinberg Dithering algorithm</summary>
		public static PatterningDitherAlgorithm FloydSteinberg => new PatterningDitherAlgorithm()
		{
			Denominator = 16,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),7),
				((-1,1),3),
				(( 0,1),5),
				(( 1,1),1)
			},
			Title = "Floyd-Steinberg"
		};
		/// <summary>False Floyd Steinberg Dithering algorithm</summary>
		public static PatterningDitherAlgorithm FalseFloydSteinberg => new PatterningDitherAlgorithm()
		{
			Denominator = 8,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),3),
				(( 0,1),3),
				(( 1,1),2)
			},
			Title = "False_Floyd-Steinberg"
		};
		/// <summary>Jarvis-Jude-Ninke algorithm</summary>
		public static PatterningDitherAlgorithm JarvisJudeNinke => new PatterningDitherAlgorithm()
		{
			Denominator = 48,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),7),
				(( 2,0),5),
				((-2,1),3),
				((-1,1),5),
				(( 0,1),7),
				(( 1,1),5),
				(( 2,1),3),
				((-2,2),1),
				((-1,2),3),
				(( 0,2),5),
				(( 1,2),3),
				(( 2,2),1)
			},
			Title = "Jarvis-Jude-Ninke"
		};
		/// <summary>Stuki Dithering algorithm</summary>
		public static PatterningDitherAlgorithm Stucki => new PatterningDitherAlgorithm()
		{
			Denominator = 42,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),8),
				(( 2,0),4),
				((-2,1),2),
				((-1,1),4),
				(( 0,1),8),
				(( 1,1),4),
				(( 2,1),2),
				((-2,2),1),
				((-1,2),2),
				(( 0,2),4),
				(( 1,2),2),
				(( 2,2),1)
			},
			Title = "Stuki"
		};
		/// <summary>Atkinson Dithering algorithm</summary>
		public static PatterningDitherAlgorithm Atkinson => new PatterningDitherAlgorithm()
		{
			Denominator = 8,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),1),
				(( 2,0),1),
				((-1,1),1),
				(( 0,1),1),
				(( 1,1),1),
				(( 0,2),1)
			},
			Title = "Atkinson"
		};
		/// <summary>Brukes Dithering algorithm</summary>
		public static PatterningDitherAlgorithm Brukes => new PatterningDitherAlgorithm()
		{
			Denominator = 32,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),8),
				(( 2,0),4),
				((-2,1),2),
				((-1,1),4),
				(( 0,1),8),
				(( 1,1),4),
				(( 2,1),2)
			},
			Title = "Brukes"
		};
		/// <summary>Sierra Dithering algorithm</summary>
		public static PatterningDitherAlgorithm Sierra => new PatterningDitherAlgorithm()
		{
			Denominator = 32,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),5),
				(( 2,0),3),
				((-2,1),2),
				((-1,1),4),
				(( 0,1),5),
				(( 1,1),4),
				(( 2,1),2),
				((-1,2),2),
				(( 0,2),3),
				(( 1,2),2)
			},
			Title = "Sierra"
		};
		/// <summary>Sierra Two-Row Dithering algorithm</summary>
		public static PatterningDitherAlgorithm SierraTwoRow => new PatterningDitherAlgorithm()
		{
			Denominator = 16,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),4),
				(( 2,0),3),
				((-2,1),1),
				((-1,1),2),
				(( 0,1),3),
				(( 1,1),2),
				(( 2,1),1)
			},
			Title = "Sierra Two-Row"
		};
		/// <summary>Sierra-Lite Dithering algorithm</summary>
		public static PatterningDitherAlgorithm SierraLite => new PatterningDitherAlgorithm()
		{
			Denominator = 4,
			Numenators = new ((sbyte, sbyte) Position, byte Numenator)[]
			{
				(( 1,0),2),
				((-1,1),1),
				(( 0,1),1)
			},
			Title = "Sierra-Lite"
		};
	}
}