using System.Numerics;
using System.Text.Json.Serialization;

namespace IDFinder
{
	
	public abstract class BackDecals
	{
		public enum BackPattern // There is a duplicate in Scavenger.cs. Sort that out later.
		{   // Name is prefixed with Back to avoid issues with two definitions of Pattern in BackDecals
			DoubleSpineRidge,
			RandomBackBlotch,
			SpineRidge
		}
		public float Top { get; protected set; }
		public float Bottom { get; protected set; }
		public BackPattern Pattern { get; protected set; }
		[JsonIgnore]
		public Vector2[] Positions { get; protected set; } = null!;
		public BackDecals() { }
		public void GeneratePattern(BackPattern inPattern, IndividualVariations iVars)
		{
			Pattern = inPattern;
			switch (Pattern)
			{
				case BackPattern.SpineRidge:
					Top = Custom.Lerp(0.07f, 0.3f, XORShift128.NextFloat());
					Bottom = Custom.Lerp(0.6f, 1f, XORShift128.NextFloat());
					float num = Custom.Lerp(2.5f, 12f, XORShift128.NextFloat());
					int num2 = (int)((Bottom - Top) * 100f / num);
					Positions = new Vector2[num2];
					for (int i = 0; i < Positions.Length; i++)
						Positions[i] = new Vector2(0f, Custom.Lerp(Top, Bottom, (float)i / (float)(num2 - 1)));

					break;

				case BackPattern.DoubleSpineRidge:
					Top = Custom.Lerp(0.07f, 0.3f, XORShift128.NextFloat());
					Bottom = Custom.Lerp(0.6f, 1f, XORShift128.NextFloat());
					if (this is WobblyBackTufts)
					{
						Bottom = Custom.Lerp(Bottom, 0.5f, XORShift128.NextFloat());
					}
					float num3 = Custom.Lerp(4.5f, 12f, XORShift128.NextFloat());
					int num4 = (int)((Bottom - Top) * 100f / num3);
					Positions = new Vector2[num4 * 2];
					for (int j = 0; j < num4; j++)
					{
						Positions[j * 2] = new Vector2(-0.9f, Custom.Lerp(Top, Bottom, (float)j / (float)(num4 - 1)));
						Positions[j * 2 + 1] = new Vector2(0.9f, Custom.Lerp(Top, Bottom, (float)j / (float)(num4 - 1)));
					}

					break;

				case BackPattern.RandomBackBlotch:
					float value = XORShift128.NextFloat();
					int num5 = (int)Custom.Lerp(Custom.Lerp(20f, 4f, iVars.Scruffy), 40f, Custom.Lerp(value, XORShift128.NextFloat(), 0.5f * XORShift128.NextFloat()));
					Positions = new Vector2[num5];
					for (int k = 0; k < num5; k++)
					{
						Positions[k] = Custom.RNV();
					}
					Top = Custom.Lerp(0.02f, 0.2f, XORShift128.NextFloat());
					Bottom = Custom.Lerp(0.4f, 0.9f, float.Pow(XORShift128.NextFloat(), 1.5f));
					for (int l = 0; l < num5; l++)
					{
						Positions[l].Y = Custom.LerpMap(Positions[l].Y, -1f, 1f, Top, Bottom);
					}

					break;
			}

			List<Vector2> list = new();
			for (int m = 0; m < Positions.Length; m++)
			{
				list.Add(Positions[m]);
			}
			IEnumerable<Vector2> source = from pet in list
										  orderby pet.Y descending
										  select pet;
			Positions = source.ToArray();
		}
		internal void GeneratePatternRNGParam(BackPattern inPattern, IndividualVariations iVars, InstanceXORShift128 XORShift128)
		{
			Pattern = inPattern;
			switch (Pattern)
			{
				case BackPattern.SpineRidge:
					Top = Custom.Lerp(0.07f, 0.3f, XORShift128.NextFloat());
					Bottom = Custom.Lerp(0.6f, 1f, XORShift128.NextFloat());
					float num = Custom.Lerp(2.5f, 12f, XORShift128.NextFloat());
					int num2 = (int)((Bottom - Top) * 100f / num);
					Positions = new Vector2[num2];
					for (int i = 0; i < Positions.Length; i++)
						Positions[i] = new Vector2(0f, Custom.Lerp(Top, Bottom, (float)i / (float)(num2 - 1)));

					break;

				case BackPattern.DoubleSpineRidge:
					Top = Custom.Lerp(0.07f, 0.3f, XORShift128.NextFloat());
					Bottom = Custom.Lerp(0.6f, 1f, XORShift128.NextFloat());
					if (this is WobblyBackTufts)
					{
						Bottom = Custom.Lerp(Bottom, 0.5f, XORShift128.NextFloat());
					}
					float num3 = Custom.Lerp(4.5f, 12f, XORShift128.NextFloat());
					int num4 = (int)((Bottom - Top) * 100f / num3);
					Positions = new Vector2[num4 * 2];
					for (int j = 0; j < num4; j++)
					{
						Positions[j * 2] = new Vector2(-0.9f, Custom.Lerp(Top, Bottom, (float)j / (float)(num4 - 1)));
						Positions[j * 2 + 1] = new Vector2(0.9f, Custom.Lerp(Top, Bottom, (float)j / (float)(num4 - 1)));
					}

					break;

				case BackPattern.RandomBackBlotch:
					float value = XORShift128.NextFloat();
					int num5 = (int)Custom.Lerp(Custom.Lerp(20f, 4f, iVars.Scruffy), 40f, Custom.Lerp(value, XORShift128.NextFloat(), 0.5f * XORShift128.NextFloat()));
					Positions = new Vector2[num5];
					for (int k = 0; k < num5; k++)
					{
						Positions[k] = Custom.RNVRNGParam(XORShift128);
					}
					Top = Custom.Lerp(0.02f, 0.2f, XORShift128.NextFloat());
					Bottom = Custom.Lerp(0.4f, 0.9f, float.Pow(XORShift128.NextFloat(), 1.5f));
					for (int l = 0; l < num5; l++)
					{
						Positions[l].Y = Custom.LerpMap(Positions[l].Y, -1f, 1f, Top, Bottom);
					}

					break;
			}

			List<Vector2> list = new();
			for (int m = 0; m < Positions.Length; m++)
			{
				list.Add(Positions[m]);
			}
			IEnumerable<Vector2> source = from pet in list
										  orderby pet.Y descending
										  select pet;
			Positions = source.ToArray();
		}
	}
	public abstract class BackTuftsAndRidges : BackDecals
	{
		public enum ColorTypeEnum
		{
			None,
			Decoration,
			Head
		}
		public string Type { get => this.GetType().Name; }
		public ColorTypeEnum ColorType { get => IsColored ? (UseDetailColor ? ColorTypeEnum.Decoration : ColorTypeEnum.Head) : ColorTypeEnum.None; }
		public bool IsColored { get => Colored > 0f; }
		public int ScaleGraf { get; protected set; }
		[JsonIgnore]
		public float ScaleGrafHeight { get; protected set; }
		public float GeneralSize { get; protected set; }
		[JsonIgnore]
		public float XFlip { get; protected set; }
		public float Colored { get; protected set; }
		public int NumberOfSpines
		{
			get	// I'm pretty sure I can just use BackDecals.Positions? 
			{
				if (ColorAlphas is not null)
					return ColorAlphas.Length;
				if (this is WobblyBackTufts tufts)
					return tufts.Scales.Length;
				if (this is HardBackSpikes spikes)
					return spikes.Sizes.Length;
				throw new Exception("NumberOfSpines failed to be counted.");
			}
		}
		[JsonIgnore]
		public float[] ColorAlphas { get; protected set; } = null!;
		public bool UseDetailColor { get; protected set; }
		public BackTuftsAndRidges(IndividualVariations iVars)
		{
			ScaleGraf = XORShift128.NextIntRange(0, 7);
			XFlip = -1f;
			if (ScaleGraf == 3)
			{
				XFlip = 1f;
			}
			if (XORShift128.NextFloat() < 0.025f)
			{
				XFlip = -XFlip;
			}
			if (XORShift128.NextFloat() < 0.5f)
			{
				XFlip *= 0.5f + 0.5f * XORShift128.NextFloat();
			}
			if (XORShift128.NextFloat() > iVars.GeneralMelanin)
			{
				Colored = float.Pow(XORShift128.NextFloat(), 0.5f);
			}
			UseDetailColor = (XORShift128.NextFloat() < 0.5f);
		}
		internal BackTuftsAndRidges(IndividualVariations iVars, InstanceXORShift128 XORShift128)
		{
			ScaleGraf = XORShift128.NextIntRange(0, 7);
			XFlip = -1f;
			if (ScaleGraf == 3)
			{
				XFlip = 1f;
			}
			if (XORShift128.NextFloat() < 0.025f)
			{
				XFlip = -XFlip;
			}
			if (XORShift128.NextFloat() < 0.5f)
			{
				XFlip *= 0.5f + 0.5f * XORShift128.NextFloat();
			}
			if (XORShift128.NextFloat() > iVars.GeneralMelanin)
			{
				Colored = float.Pow(XORShift128.NextFloat(), 0.5f);
			}
			UseDetailColor = (XORShift128.NextFloat() < 0.5f);
		}
	}
	public class HardBackSpikes : BackTuftsAndRidges
	{
		[JsonIgnore]
		public float[] Sizes { get; private set; }
		public HardBackSpikes(IndividualVariations iVars, Personality personality) : base(iVars)
		{
			Pattern = ((XORShift128.NextFloat() < 0.6f) ? BackPattern.SpineRidge : BackPattern.DoubleSpineRidge);
			if (XORShift128.NextFloat() < 0.1f)
			{
				Pattern = BackPattern.RandomBackBlotch;
			}
			GeneratePattern(Pattern, iVars);
			//int totalSprites = Positions.Length * (IsColored ? 2 : 1);	// Is this necessary?
			if (XORShift128.NextFloat() < 0.5f)
			{
				if (XORShift128.NextFloat() < 0.85f)
					ScaleGraf = XORShift128.NextIntRange(0, 4);
				else
					ScaleGraf = 6;
			}
			Sizes = new float[Positions.Length];
			float a = Custom.Lerp(0.1f, 0.6f, XORShift128.NextFloat());
			float p = Custom.Lerp(0.3f, 1f, XORShift128.NextFloat());
			GeneralSize = Custom.LerpMap((float)Positions.Length, 5f, 35f, 1f, 0.2f);
			GeneralSize = Custom.Lerp(GeneralSize, personality.Dominance, XORShift128.NextFloat());
			GeneralSize = Custom.Lerp(GeneralSize, float.Pow(XORShift128.NextFloat(), 0.75f), XORShift128.NextFloat());
			for (int i = 0; i < Sizes.Length; i++)
			{
				Sizes[i] = Custom.Lerp(a, 1f, float.Sin(float.Pow(Custom.InverseLerp(Top, Bottom, Positions[i].Y), p) * 3.1415927f));
			}
			if (IsColored)
			{
				ColorAlphas = new float[Positions.Length];
				if (XORShift128.NextFloat() < 0.25f + 0.5f * Colored)
				{
					float a2 = float.MaxValue;
					float num = float.MinValue;
					for (int j = 0; j < Positions.Length; j++)
					{
						a2 = float.Min(a2, Positions[j].Y);
						num = float.Max(num, Positions[j].Y);
					}
					float p2 = Custom.Lerp(0.2f, 1.2f, XORShift128.NextFloat());
					for (int k = 0; k < ColorAlphas.Length; k++)
					{
						ColorAlphas[k] = Custom.Lerp(Colored, 0f, float.Pow(Custom.InverseLerp(a2, num, Positions[k].Y), p2));
					}
					return;
				}
				for (int l = 0; l < ColorAlphas.Length; l++)
				{
					ColorAlphas[l] = Colored;
				}
			}

		}
		internal HardBackSpikes(IndividualVariations iVars, Personality personality, InstanceXORShift128 XORShift128) : base(iVars, XORShift128)
		{
			Pattern = ((XORShift128.NextFloat() < 0.6f) ? BackPattern.SpineRidge : BackPattern.DoubleSpineRidge);
			if (XORShift128.NextFloat() < 0.1f)
			{
				Pattern = BackPattern.RandomBackBlotch;
			}
			GeneratePatternRNGParam(Pattern, iVars, XORShift128);
			//int totalSprites = Positions.Length * (IsColored ? 2 : 1);	// Is this necessary?
			if (XORShift128.NextFloat() < 0.5f)
			{
				if (XORShift128.NextFloat() < 0.85f)
					ScaleGraf = XORShift128.NextIntRange(0, 4);
				else
					ScaleGraf = 6;
			}
			Sizes = new float[Positions.Length];
			float a = Custom.Lerp(0.1f, 0.6f, XORShift128.NextFloat());
			float p = Custom.Lerp(0.3f, 1f, XORShift128.NextFloat());
			GeneralSize = Custom.LerpMap((float)Positions.Length, 5f, 35f, 1f, 0.2f);
			GeneralSize = Custom.Lerp(GeneralSize, personality.Dominance, XORShift128.NextFloat());
			GeneralSize = Custom.Lerp(GeneralSize, float.Pow(XORShift128.NextFloat(), 0.75f), XORShift128.NextFloat());
			for (int i = 0; i < Sizes.Length; i++)
			{
				Sizes[i] = Custom.Lerp(a, 1f, float.Sin(float.Pow(Custom.InverseLerp(Top, Bottom, Positions[i].Y), p) * 3.1415927f));
			}
			if (IsColored)
			{
				ColorAlphas = new float[Positions.Length];
				if (XORShift128.NextFloat() < 0.25f + 0.5f * Colored)
				{
					float a2 = float.MaxValue;
					float num = float.MinValue;
					for (int j = 0; j < Positions.Length; j++)
					{
						a2 = float.Min(a2, Positions[j].Y);
						num = float.Max(num, Positions[j].Y);
					}
					float p2 = Custom.Lerp(0.2f, 1.2f, XORShift128.NextFloat());
					for (int k = 0; k < ColorAlphas.Length; k++)
					{
						ColorAlphas[k] = Custom.Lerp(Colored, 0f, float.Pow(Custom.InverseLerp(a2, num, Positions[k].Y), p2));
					}
					return;
				}
				for (int l = 0; l < ColorAlphas.Length; l++)
				{
					ColorAlphas[l] = Colored;
				}
			}

		}
	}
	public class WobblyBackTufts : BackTuftsAndRidges
	{
		public float DownAlongSpine { get; private set; }
		public float OutToSides { get; private set; }
		[JsonIgnore]
		public Vector2[] RandomDirs { get; private set; }
		[JsonIgnore]
		public Scale[] Scales { get; private set; }
		public WobblyBackTufts(IndividualVariations iVars, Personality personality) : base(iVars)
		{
			Pattern = BackPattern.RandomBackBlotch;
			if (XORShift128.NextFloat() < 0.25f && (iVars.Scruffy == 0f || XORShift128.NextFloat() < 0.05f))
			{
				if (XORShift128.NextFloat() < 0.5f)
				{
					Pattern = BackPattern.DoubleSpineRidge;
				}
				else
				{
					Pattern = BackPattern.SpineRidge;
				}
			}
			if (XORShift128.NextFloat() < 0.2f)
			{
				ScaleGraf = 0;
			}
			else if (XORShift128.NextFloat() < 0.5f)
			{
				if (XORShift128.NextFloat() < 1.1764705f)	// This will trigger always? Do not remove as to maintain correct RNG state
				{
					ScaleGraf = XORShift128.NextIntRange(3, 6);
				}
				else
				{
					ScaleGraf = 0;
				}
			}
			else
			{
				XFlip *= 0.5f + XORShift128.NextFloat() * 0.5f;
			}
			GeneratePattern(Pattern, iVars);
			if (Pattern == BackPattern.RandomBackBlotch)
			{
				OutToSides = XORShift128.NextFloat();
			}
			else
			{
				OutToSides = 0f;
			}
			DownAlongSpine = XORShift128.NextFloat();
			//int TotalSprites = Positions.Length * (IsColored ? 2 : 1);
			Scales = new Scale[Positions.Length];
			GeneralSize = Custom.Lerp(XORShift128.NextFloat(), personality.Dominance, XORShift128.NextFloat());
			GeneralSize = Custom.Lerp(GeneralSize, XORShift128.NextFloat(), XORShift128.NextFloat());
			GeneralSize = float.Pow(GeneralSize, Custom.Lerp(2f, 0.65f, personality.Dominance));
			float grav = Custom.Lerp(0f, 0.9f, XORShift128.NextFloat());
			float airFric = Custom.Lerp(0.2f, 0.95f, XORShift128.NextFloat());
			float num = Custom.Lerp(0.1f, 9f, float.Pow(XORShift128.NextFloat(), 0.2f));
			float rigidGradRad = Custom.Lerp(float.Max(4f, num * 1.5f), 37f, float.Pow(XORShift128.NextFloat(), 2f));
			float rigidExp = Custom.Lerp(1f, 6f, float.Pow(XORShift128.NextFloat(), 5f));
			Scale.ScaleStats stats = new(grav, airFric, num, rigidGradRad, rigidExp, 0.5f + XORShift128.NextFloat() * 0.5f);
			float num2 = Custom.Lerp(0.1f, 0.6f, XORShift128.NextFloat());
			float p = Custom.Lerp(1.2f, 0.3f, XORShift128.NextFloat());
			float a = num2 * XORShift128.NextFloat();
			RandomDirs = new Vector2[Scales.Length];
			for (int i = 0; i < Scales.Length; i++)
			{
				RandomDirs[i] = Custom.RNV() * XORShift128.NextFloat() * iVars.Scruffy;
				float num3;
				if (Pattern == BackPattern.SpineRidge || Pattern == BackPattern.DoubleSpineRidge)
				{
					num3 = Custom.Lerp(num2, 1f, float.Sin(float.Pow(Custom.InverseLerp(Top, Bottom, Positions[i].Y), p) * 3.1415927f));
				}
				else if (Pattern == BackPattern.RandomBackBlotch)
				{
					num3 = Custom.Lerp(num2, 1f, XORShift128.NextFloat());
					num3 = float.Min(num3, Custom.Lerp(a, 1f, float.Sin(float.Pow(Custom.InverseLerp(Top, Bottom, Positions[i].Y), p) * 3.1415927f)));
					RandomDirs[i] = Custom.RNV() * (1f - 2f * float.Abs(0.5f - Custom.InverseLerp(Top, Bottom, Positions[i].Y)));
				}
				else
				{
					num3 = Custom.Lerp(1f, num2, Custom.InverseLerp(Top, Bottom, Positions[i].Y));
				}
				if (XORShift128.NextFloat() < iVars.Scruffy)
				{
					num3 = Custom.Lerp(num3, XORShift128.NextFloat(), float.Pow(XORShift128.NextFloat(), Custom.Lerp(4f, 0.5f, iVars.Scruffy)));
				}
				Scales[i] = new Scale(i, stats, 40f * num3 * Custom.Lerp(0.1f, 1f, GeneralSize));
			}
			if (IsColored)
			{
				ColorAlphas = new float[Positions.Length];
				if (XORShift128.NextFloat() < 0.25f + 0.5f * Colored)
				{
					float a2 = float.MaxValue;
					float num4 = float.MinValue;
					for (int j = 0; j < Positions.Length; j++)
					{
						a2 = float.Min(a2, Positions[j].Y);
						num4 = float.Max(num4, Positions[j].Y);
					}
					float p2 = Custom.Lerp(0.2f, 1.2f, XORShift128.NextFloat());
					for (int k = 0; k < ColorAlphas.Length; k++)
					{
						ColorAlphas[k] = Custom.Lerp(Colored, 0f, float.Pow(Custom.InverseLerp(a2, num4, Positions[k].Y), p2));
					}
					return;
				}
				for (int l = 0; l < ColorAlphas.Length; l++)
				{
					ColorAlphas[l] = Colored;
				}
			}
		}
		internal WobblyBackTufts(IndividualVariations iVars, Personality personality, InstanceXORShift128 XORShift128) : base(iVars, XORShift128)
		{
			Pattern = BackPattern.RandomBackBlotch;
			if (XORShift128.NextFloat() < 0.25f && (iVars.Scruffy == 0f || XORShift128.NextFloat() < 0.05f))
			{
				if (XORShift128.NextFloat() < 0.5f)
				{
					Pattern = BackPattern.DoubleSpineRidge;
				}
				else
				{
					Pattern = BackPattern.SpineRidge;
				}
			}
			if (XORShift128.NextFloat() < 0.2f)
			{
				ScaleGraf = 0;
			}
			else if (XORShift128.NextFloat() < 0.5f)
			{
				if (XORShift128.NextFloat() < 1.1764705f)   // This will trigger always? Do not remove as to maintain correct RNG state
				{
					ScaleGraf = XORShift128.NextIntRange(3, 6);
				}
				else
				{
					ScaleGraf = 0;
				}
			}
			else
			{
				XFlip *= 0.5f + XORShift128.NextFloat() * 0.5f;
			}
			GeneratePatternRNGParam(Pattern, iVars, XORShift128);
			if (Pattern == BackPattern.RandomBackBlotch)
			{
				OutToSides = XORShift128.NextFloat();
			}
			else
			{
				OutToSides = 0f;
			}
			DownAlongSpine = XORShift128.NextFloat();
			//int TotalSprites = Positions.Length * (IsColored ? 2 : 1);
			Scales = new Scale[Positions.Length];
			GeneralSize = Custom.Lerp(XORShift128.NextFloat(), personality.Dominance, XORShift128.NextFloat());
			GeneralSize = Custom.Lerp(GeneralSize, XORShift128.NextFloat(), XORShift128.NextFloat());
			GeneralSize = float.Pow(GeneralSize, Custom.Lerp(2f, 0.65f, personality.Dominance));
			float grav = Custom.Lerp(0f, 0.9f, XORShift128.NextFloat());
			float airFric = Custom.Lerp(0.2f, 0.95f, XORShift128.NextFloat());
			float num = Custom.Lerp(0.1f, 9f, float.Pow(XORShift128.NextFloat(), 0.2f));
			float rigidGradRad = Custom.Lerp(float.Max(4f, num * 1.5f), 37f, float.Pow(XORShift128.NextFloat(), 2f));
			float rigidExp = Custom.Lerp(1f, 6f, float.Pow(XORShift128.NextFloat(), 5f));
			Scale.ScaleStats stats = new(grav, airFric, num, rigidGradRad, rigidExp, 0.5f + XORShift128.NextFloat() * 0.5f);
			float num2 = Custom.Lerp(0.1f, 0.6f, XORShift128.NextFloat());
			float p = Custom.Lerp(1.2f, 0.3f, XORShift128.NextFloat());
			float a = num2 * XORShift128.NextFloat();
			RandomDirs = new Vector2[Scales.Length];
			for (int i = 0; i < Scales.Length; i++)
			{
				RandomDirs[i] = Custom.RNVRNGParam(XORShift128) * XORShift128.NextFloat() * iVars.Scruffy;
				float num3;
				if (Pattern == BackPattern.SpineRidge || Pattern == BackPattern.DoubleSpineRidge)
				{
					num3 = Custom.Lerp(num2, 1f, float.Sin(float.Pow(Custom.InverseLerp(Top, Bottom, Positions[i].Y), p) * 3.1415927f));
				}
				else if (Pattern == BackPattern.RandomBackBlotch)
				{
					num3 = Custom.Lerp(num2, 1f, XORShift128.NextFloat());
					num3 = float.Min(num3, Custom.Lerp(a, 1f, float.Sin(float.Pow(Custom.InverseLerp(Top, Bottom, Positions[i].Y), p) * 3.1415927f)));
					RandomDirs[i] = Custom.RNVRNGParam(XORShift128) * (1f - 2f * float.Abs(0.5f - Custom.InverseLerp(Top, Bottom, Positions[i].Y)));
				}
				else
				{
					num3 = Custom.Lerp(1f, num2, Custom.InverseLerp(Top, Bottom, Positions[i].Y));
				}
				if (XORShift128.NextFloat() < iVars.Scruffy)
				{
					num3 = Custom.Lerp(num3, XORShift128.NextFloat(), float.Pow(XORShift128.NextFloat(), Custom.Lerp(4f, 0.5f, iVars.Scruffy)));
				}
				Scales[i] = new Scale(i, stats, 40f * num3 * Custom.Lerp(0.1f, 1f, GeneralSize));
			}
			if (IsColored)
			{
				ColorAlphas = new float[Positions.Length];
				if (XORShift128.NextFloat() < 0.25f + 0.5f * Colored)
				{
					float a2 = float.MaxValue;
					float num4 = float.MinValue;
					for (int j = 0; j < Positions.Length; j++)
					{
						a2 = float.Min(a2, Positions[j].Y);
						num4 = float.Max(num4, Positions[j].Y);
					}
					float p2 = Custom.Lerp(0.2f, 1.2f, XORShift128.NextFloat());
					for (int k = 0; k < ColorAlphas.Length; k++)
					{
						ColorAlphas[k] = Custom.Lerp(Colored, 0f, float.Pow(Custom.InverseLerp(a2, num4, Positions[k].Y), p2));
					}
					return;
				}
				for (int l = 0; l < ColorAlphas.Length; l++)
				{
					ColorAlphas[l] = Colored;
				}
			}
		}
	}
	public class Scale
	{
		public ScaleStats Stats { get; set; }
		public Vector2 Pos { get; set; }
		public Vector2 LastPos { get; set; }
		public Vector2 Vel { get; set; }
		public int Index { get; set; }
		public float Length { get; set; }
		public Scale(int index, ScaleStats stats, float length)
		{
			Index = index;
			Stats = stats;
			Length = length;
		}
		public class ScaleStats
		{
			public float Grav { get; set; }
			public float AirFric { get; set; }
			public float Rigid { get; set; }
			public float RigidGradRad { get; set; }
			public float RigidExp { get; set; }
			public float Elastic { get; set; }
			public ScaleStats(float grav, float airFric, float rigid, float rigidGradRad, float rigidExp, float elastic)
			{
				Grav = grav;
				AirFric = airFric;
				Rigid = rigid;
				RigidGradRad = rigidGradRad;
				RigidExp = rigidExp;
				Elastic = elastic;
			}
		}
	}
}
