using System.Diagnostics;

namespace IDFinder
{
	public class Slugcat
	{
		public int ID { get; private set; }
		public bool IsPup { get; private set; } = true;
		public Personality Personality { get; private set; }
		public NPCStats NPCStats { get; private set; }
		public SlugcatStats SlugcatStats { get; private set; }
		public FoodPreferences FoodPreferences { get; private set; }
		public Slugcat(int ID)
		{
			this.ID = ID;
			Personality = new(ID);
			NPCStats = new(ID);
			SlugcatStats = new(NPCStats);
			FoodPreferences = new(ID, Personality);
		}
	}

	public struct Personality
	{
		public float Sympathy { get; private set; }
		public float Energy { get; private set; }
		public float Bravery { get; private set; }
		public float Nervous { get; private set; }
		public float Aggression { get; private set; }
		public float Dominance { get; private set; }
		public Personality(int seed)
		{
			XORShift128.Shared.InitSeed(seed);

			Sympathy = XORShift128.Shared.NextFloat();
			Energy = XORShift128.Shared.NextFloat();
			Bravery = XORShift128.Shared.NextFloat();

			Sympathy = Custom.PushFromHalf(Sympathy, 1.5f);
			Energy = Custom.PushFromHalf(Energy, 1.5f);
			Bravery = Custom.PushFromHalf(Bravery, 1.5f);
			
			Nervous = float.Lerp(XORShift128.Shared.NextFloat(), float.Lerp(Energy, 1f - Bravery, 0.5f), float.Pow(XORShift128.Shared.NextFloat(), 0.25f));
			Aggression = float.Lerp(XORShift128.Shared.NextFloat(), (Energy + Bravery) / 2f * (1f - Sympathy), float.Pow(XORShift128.Shared.NextFloat(), 0.25f));
			Dominance = float.Lerp(XORShift128.Shared.NextFloat(), (Energy + Bravery + Aggression) / 3f, float.Pow(XORShift128.Shared.NextFloat(), 0.25f));

			Nervous = Custom.PushFromHalf(Nervous, 2.5f);

			Aggression = Custom.PushFromHalf(Aggression, 2.5f);
		}
		internal Personality(int seed, XORShift128 XORShift128)	// For multithreading
		{
			XORShift128.InitSeed(seed);

			Sympathy = XORShift128.NextFloat();
			Energy = XORShift128.NextFloat();
			Bravery = XORShift128.NextFloat();

			Sympathy = Custom.PushFromHalf(Sympathy, 1.5f);
			Energy = Custom.PushFromHalf(Energy, 1.5f);
			Bravery = Custom.PushFromHalf(Bravery, 1.5f);

			Nervous = float.Lerp(XORShift128.NextFloat(), float.Lerp(Energy, 1f - Bravery, 0.5f), float.Pow(XORShift128.NextFloat(), 0.25f));
			Aggression = float.Lerp(XORShift128.NextFloat(), (Energy + Bravery) / 2f * (1f - Sympathy), float.Pow(XORShift128.NextFloat(), 0.25f));
			Dominance = float.Lerp(XORShift128.NextFloat(), (Energy + Bravery + Aggression) / 3f, float.Pow(XORShift128.NextFloat(), 0.25f));

			Nervous = Custom.PushFromHalf(Nervous, 2.5f);

			Aggression = Custom.PushFromHalf(Aggression, 2.5f);
		}
	}
	public struct NPCStats
	{
		public float Met { get; private set; }
		public float Bal { get; private set; }
		public float Size { get; private set; }
		public float Stealth { get; private set; }
		public bool Dark { get; private set; }
		public float EyeColor { get; private set; }
		public float H { get; private set; }
		public float S { get; private set; }
		public float L { get; private set; }
		public float Wideness { get; private set; }
		public NPCStats(int ID)
		{
			XORShift128.Shared.InitSeed(ID);

			Bal = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 1.5f);
			Met = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 1.5f);
			Stealth = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 1.5f);
			Size = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 1.5f);
			Wideness = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 1.5f);
			H = float.Lerp(XORShift128.Shared.NextFloatRange(0.15f, 0.58f), XORShift128.Shared.NextFloat(), float.Pow(XORShift128.Shared.NextFloat(), 1.5f - this.Met));
			S = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 0.3f + this.Stealth * 0.3f);
			Dark = (XORShift128.Shared.NextFloatRange(0f, 1f) <= 0.3f + this.Stealth * 0.2f);
			L = float.Pow(XORShift128.Shared.NextFloatRange(this.Dark ? 0.9f : 0.75f, 1f), 1.5f - this.Stealth);   // Min val = Math.Pow(0.75f, 1.5f) or 0.649519f
			EyeColor = float.Pow(XORShift128.Shared.NextFloatRange(0f, 1f), 2f - this.Stealth * 1.5f);

			switch (ID)
			{
				case 1000:
					H = 0.6111111f;
					S = 0.6f;
					L = 0.75f;
					break;

				case 1001:
					H = 0.4743589f;
					S = 0.6f;
					L = 0.675f;
					break;

				case 1002:
					H = 0.9458887f;
					S = 0.5238261f;
					L = 0.288235f;
					break;
			}
		}
		internal NPCStats(int ID, XORShift128 XORShift128)
		{
			XORShift128.InitSeed(ID);

			Bal = float.Pow(XORShift128.NextFloatRange(0f, 1f), 1.5f);
			Met = float.Pow(XORShift128.NextFloatRange(0f, 1f), 1.5f);
			Stealth = float.Pow(XORShift128.NextFloatRange(0f, 1f), 1.5f);
			Size = float.Pow(XORShift128.NextFloatRange(0f, 1f), 1.5f);
			Wideness = float.Pow(XORShift128.NextFloatRange(0f, 1f), 1.5f);
			H = float.Lerp(XORShift128.NextFloatRange(0.15f, 0.58f), XORShift128.NextFloat(), float.Pow(XORShift128.NextFloat(), 1.5f - this.Met));
			S = float.Pow(XORShift128.NextFloatRange(0f, 1f), 0.3f + this.Stealth * 0.3f);
			Dark = (XORShift128.NextFloatRange(0f, 1f) <= 0.3f + this.Stealth * 0.2f);
			L = float.Pow(XORShift128.NextFloatRange(this.Dark ? 0.9f : 0.75f, 1f), 1.5f - this.Stealth);
			EyeColor = float.Pow(XORShift128.NextFloatRange(0f, 1f), 2f - this.Stealth * 1.5f);

			switch (ID)
			{
				case 1000:
					H = 0.6111111f;
					S = 0.6f;
					L = 0.75f;
					break;

				case 1001:
					H = 0.4743589f;
					S = 0.6f;
					L = 0.675f;
					break;

				case 1002:
					H = 0.9458887f;
					S = 0.5238261f;
					L = 0.288235f;
					break;
			}
		}
	}
	public struct FoodPreferences
	{
		public enum Food
		{
			DangleFruit,
			WaterNut,
			JellyFish,
			SlimeMold,
			EggBugEgg,
			FireEgg,
			Popcorn,
			GooieDuck,
			LilyPuck,
			GlowWeed,
			DandelionPeach,
			Neuron,
			Centipede,
			SmallCentipede,
			VultureGrub,
			SmallNeedleWorm,
			Hazer,
			NotCounted
		}

		#region Properties
		public float DangleFruit {get; private set; }
		public float WaterNut {get; private set; }
		public float JellyFish {get; private set; }
		public float SlimeMold {get; private set; }
		public float EggBugEgg {get; private set; }
		public float FireEgg {get; private set; }
		public float Popcorn {get; private set; }
		public float GooieDuck {get; private set; }
		public float LilyPuck {get; private set; }
		public float GlowWeed {get; private set; }
		public float DandelionPeach {get; private set; }
		public float Neuron {get; private set; }
		public float Centipede {get; private set; }
		public float SmallCentipede {get; private set; }
		public float VultureGrub {get; private set; }
		public float SmallNeedleWorm {get; private set; }
		public float Hazer {get; private set; }
		public float NotCounted {get; private set; }
		#endregion
		public FoodPreferences(int ID, Personality p)
		{
			float[] preferences = GetPreferences(ID, p);
			DangleFruit = preferences[0];
			WaterNut = preferences[1];
			JellyFish = preferences[2];
			SlimeMold = preferences[3];
			EggBugEgg = preferences[4];
			FireEgg = preferences[5];
			Popcorn = preferences[6];
			GooieDuck = preferences[7];
			LilyPuck = preferences[8];
			GlowWeed = preferences[9];
			DandelionPeach = preferences[10];
			Neuron = preferences[11];
			Centipede = preferences[12];
			SmallCentipede = preferences[13];
			VultureGrub = preferences[14];
			SmallNeedleWorm = preferences[15];
			Hazer = preferences[16];
			NotCounted = preferences[17];
		}
		public FoodPreferences(int ID) : this(ID, new(ID)) { }
		internal FoodPreferences(int ID, Personality p, XORShift128 XORShift128)
		{
			Span<float> preferences = stackalloc float[18];
			GetPreferencesRNGParam(ID, p, preferences, XORShift128);
			unsafe
			{
				fixed (float* ptr = preferences)	// Ensure no bounds checks. I'm fairly certain the compiler would do this anyway, but it doesn't hurt to force it
				{
					DangleFruit = ptr[0];
					WaterNut = ptr[1];
					JellyFish = ptr[2];
					SlimeMold = ptr[3];
					EggBugEgg = ptr[4];
					FireEgg = ptr[5];
					Popcorn = ptr[6];
					GooieDuck = ptr[7];
					LilyPuck = ptr[8];
					GlowWeed = ptr[9];
					DandelionPeach = ptr[10];
					Neuron = ptr[11];
					Centipede = ptr[12];
					SmallCentipede = ptr[13];
					VultureGrub = ptr[14];
					SmallNeedleWorm = ptr[15];
					Hazer = ptr[16];
					NotCounted = ptr[17];
				}
			}
		}
		public static float[] GetPreferences(int ID, Personality p)
		{
			float[] foodPreference = new float[18];
			XORShift128.Shared.InitSeed(ID);
			FoodPreferences.Food f;
			float num, num2;

			int c = 0;
			foreach (FoodPreferences.Food i in Enum.GetValues<FoodPreferences.Food>())
			{
				f = i;
				switch (f)
				{
					default:
						num = num2 = 0f;
						break;
					case FoodPreferences.Food.DangleFruit:
						num = p.Nervous;
						num2 = p.Energy;
						break;
					case FoodPreferences.Food.WaterNut:
						num = p.Sympathy;
						num2 = p.Aggression;
						break;
					case FoodPreferences.Food.JellyFish:
						num = p.Energy;
						num2 = p.Nervous;
						break;
					case FoodPreferences.Food.SlimeMold:
						num = p.Energy;
						num2 = p.Aggression;
						break;
					case FoodPreferences.Food.EggBugEgg:
						num = p.Dominance;
						num2 = p.Energy;
						break;
					case FoodPreferences.Food.FireEgg:
						num = p.Aggression;
						num2 = p.Sympathy;
						break;
					case FoodPreferences.Food.Popcorn:
						num = p.Dominance;
						num2 = p.Bravery;
						break;
					case FoodPreferences.Food.GooieDuck:
						num = p.Sympathy;
						num2 = p.Bravery;
						break;
					case FoodPreferences.Food.LilyPuck:
						num = p.Aggression;
						num2 = p.Nervous;
						break;
					case FoodPreferences.Food.GlowWeed:
						num = p.Nervous;
						num2 = p.Energy;
						break;
					case FoodPreferences.Food.DandelionPeach:
						num = p.Bravery;
						num2 = p.Dominance;
						break;
					case FoodPreferences.Food.Neuron:
						num = p.Bravery;
						num2 = p.Nervous;
						break;
					case FoodPreferences.Food.Centipede:
						num = p.Bravery;
						num2 = p.Dominance;
						break;
					case FoodPreferences.Food.SmallCentipede:
						num = p.Energy;
						num2 = p.Aggression;
						break;
					case FoodPreferences.Food.VultureGrub:
						num = p.Dominance;
						num2 = p.Bravery;
						break;
					case FoodPreferences.Food.SmallNeedleWorm:
						num = p.Aggression;
						num2 = p.Sympathy;
						break;
					case FoodPreferences.Food.Hazer:
						num = p.Nervous;
						num2 = p.Sympathy;
						break;
				}
				num *= Custom.PushFromHalf(XORShift128.Shared.NextFloat(), 2f);
				num2 *= Custom.PushFromHalf(XORShift128.Shared.NextFloat(), 2f);
				foodPreference[c] = Math.Clamp(float.Lerp(num - num2, float.Lerp(-1f, 1f, Custom.PushFromHalf(XORShift128.Shared.NextFloat(), 2f)), Custom.PushFromHalf(XORShift128.Shared.NextFloat(), 2f)), -1f, 1f);
				c++;
			}
			return foodPreference;
		}
		internal static unsafe void GetPreferencesRNGParam(int ID, Personality p, Span<float> foodPrefBuffer, XORShift128 XORShift128)
		{
			Debug.Assert(foodPrefBuffer.Length == 18);
			XORShift128.InitSeed(ID);
			FoodPreferences.Food f;
			float num, num2;

			int count = 0;
			fixed (float* ptr = foodPrefBuffer)	// Avoid bounds checks
			{
				foreach (FoodPreferences.Food i in Enum.GetValues<FoodPreferences.Food>())
				{
					f = i;
					switch (f)
					{
						default:
							num = num2 = 0f;
							break;
						case FoodPreferences.Food.DangleFruit:
							num = p.Nervous;
							num2 = p.Energy;
							break;
						case FoodPreferences.Food.WaterNut:
							num = p.Sympathy;
							num2 = p.Aggression;
							break;
						case FoodPreferences.Food.JellyFish:
							num = p.Energy;
							num2 = p.Nervous;
							break;
						case FoodPreferences.Food.SlimeMold:
							num = p.Energy;
							num2 = p.Aggression;
							break;
						case FoodPreferences.Food.EggBugEgg:
							num = p.Dominance;
							num2 = p.Energy;
							break;
						case FoodPreferences.Food.FireEgg:
							num = p.Aggression;
							num2 = p.Sympathy;
							break;
						case FoodPreferences.Food.Popcorn:
							num = p.Dominance;
							num2 = p.Bravery;
							break;
						case FoodPreferences.Food.GooieDuck:
							num = p.Sympathy;
							num2 = p.Bravery;
							break;
						case FoodPreferences.Food.LilyPuck:
							num = p.Aggression;
							num2 = p.Nervous;
							break;
						case FoodPreferences.Food.GlowWeed:
							num = p.Nervous;
							num2 = p.Energy;
							break;
						case FoodPreferences.Food.DandelionPeach:
							num = p.Bravery;
							num2 = p.Dominance;
							break;
						case FoodPreferences.Food.Neuron:
							num = p.Bravery;
							num2 = p.Nervous;
							break;
						case FoodPreferences.Food.Centipede:
							num = p.Bravery;
							num2 = p.Dominance;
							break;
						case FoodPreferences.Food.SmallCentipede:
							num = p.Energy;
							num2 = p.Aggression;
							break;
						case FoodPreferences.Food.VultureGrub:
							num = p.Dominance;
							num2 = p.Bravery;
							break;
						case FoodPreferences.Food.SmallNeedleWorm:
							num = p.Aggression;
							num2 = p.Sympathy;
							break;
						case FoodPreferences.Food.Hazer:
							num = p.Nervous;
							num2 = p.Sympathy;
							break;
					}
					num *= Custom.PushFromHalf(XORShift128.NextFloat(), 2f);
					num2 *= Custom.PushFromHalf(XORShift128.NextFloat(), 2f);
					ptr[count] = Math.Clamp(float.Lerp(num - num2, float.Lerp(-1f, 1f, Custom.PushFromHalf(XORShift128.NextFloat(), 2f)), Custom.PushFromHalf(XORShift128.NextFloat(), 2f)), -1f, 1f);
					count++;
				}
			}
		}
	}
	public struct SlugcatStats
	{
		public float runSpeedFac { get; private set; } = 1f;
		public float bodyWeightFac { get; private set; } = 1f;
		public float generalVisibilityBonus { get; private set; }
		public float visualStealthInSneakMode { get; private set; } = 0.5f;
		public float loudnessFac { get; private set; } = 1f;
		public float lungsFac { get; private set; } = 1f;
		public int throwingSkill { get; private set; } = 1;
		public float poleClimbSpeedFac { get; private set; } = 1f;
		public float corridorClimbSpeedFac { get; private set; } = 1f;
		public SlugcatStats(int ID) : this(new NPCStats(ID)) { }
		public SlugcatStats(NPCStats stats, bool isSlugpup = true)
		{
			if (isSlugpup)
			{
				bodyWeightFac = 0.65f;
				generalVisibilityBonus = -0.2f;
				visualStealthInSneakMode = 0.6f;
				loudnessFac = 0.5f;
				lungsFac = 0.8f;
				throwingSkill = 0;
				poleClimbSpeedFac = 0.8f;
				corridorClimbSpeedFac = 0.8f;
				runSpeedFac = 0.8f;
			}

			runSpeedFac *= 0.85f + 0.15f * stats.Met + 0.15f * (1f - stats.Bal) + 0.1f * (1f - stats.Stealth);
			bodyWeightFac *= 0.85f + 0.15f * stats.Wideness + 0.1f * stats.Met;
			generalVisibilityBonus *= 0.8f + 0.2f * (1f - stats.Stealth) + 0.2f * stats.Met;
			visualStealthInSneakMode *= 0.75f + 0.35f * stats.Stealth + 0.15f * (1f - stats.Met);
			loudnessFac *= 0.8f + 0.2f * stats.Wideness + 0.2f * (1f - stats.Stealth);
			lungsFac *= 0.8f + 0.2f * (1f - stats.Met) + 0.2f * (1f - stats.Stealth);
			poleClimbSpeedFac *= 0.85f + 0.15f * stats.Met + 0.15f * stats.Bal + 0.1f * (1f - stats.Stealth);
			corridorClimbSpeedFac *= 0.85f + 0.15f * stats.Met + 0.15f * (1f - stats.Bal) + 0.1f * (1f - stats.Stealth);
		}
		internal SlugcatStats(int ID, XORShift128 XORShift128, bool isSlugpup = true)
		{
			NPCStats stats = new(ID, XORShift128);
			if (isSlugpup)
			{
				bodyWeightFac = 0.65f;
				generalVisibilityBonus = -0.2f;
				visualStealthInSneakMode = 0.6f;
				loudnessFac = 0.5f;
				lungsFac = 0.8f;
				throwingSkill = 0;
				poleClimbSpeedFac = 0.8f;
				corridorClimbSpeedFac = 0.8f;
				runSpeedFac = 0.8f;
			}

			runSpeedFac *= 0.85f + 0.15f * stats.Met + 0.15f * (1f - stats.Bal) + 0.1f * (1f - stats.Stealth);
			bodyWeightFac *= 0.85f + 0.15f * stats.Wideness + 0.1f * stats.Met;
			generalVisibilityBonus *= 0.8f + 0.2f * (1f - stats.Stealth) + 0.2f * stats.Met;
			visualStealthInSneakMode *= 0.75f + 0.35f * stats.Stealth + 0.15f * (1f - stats.Met);
			loudnessFac *= 0.8f + 0.2f * stats.Wideness + 0.2f * (1f - stats.Stealth);
			lungsFac *= 0.8f + 0.2f * (1f - stats.Met) + 0.2f * (1f - stats.Stealth);
			poleClimbSpeedFac *= 0.85f + 0.15f * stats.Met + 0.15f * stats.Bal + 0.1f * (1f - stats.Stealth);
			corridorClimbSpeedFac *= 0.85f + 0.15f * stats.Met + 0.15f * (1f - stats.Bal) + 0.1f * (1f - stats.Stealth);
		}
	}
}
