using System.Reflection;

namespace IDFinder
{
	public static class ColumnNames
	{
		public const string ID = "ID";
		public const string Sympathy = "sym";
		public const string Energy = "nrg";
		public const string Bravery = "brv";
		public const string Nervous = "nrv";
		public const string Aggression = "agg";
		public const string Dominance = "dom";

		public const string Met = "Met";
		public const string Bal = "Bal";
		public const string Size = "Size";
		public const string Stealth = "Stealth";
		public const string Dark = "Dark?";
		public const string EyeColor = "Eye";
		public const string H = "H";
		public const string S = "S";
		public const string L = "L";
		public const string Wideness = "Wide";

		public const string bodyWeightFac = "Weight";
		public const string generalVisibilityBonus = "StandStealth";
		public const string visualStealthInSneakMode = "CrouchStealth";
		public const string loudnessFac = "Loudness";
		public const string lungsFac = "Lungs";
		public const string throwingSkill = "ThrowSkill";
		public const string poleClimbSpeedFac = "PoleClimb";
		public const string corridorClimbSpeedFac = "CorridorClimb";
		public const string runSpeedFac = "RunSpeed";

		public const string DangleFruit = "BlueFruit";
		public const string WaterNut = "BubbleFruit";
		public const string JellyFish = "Jellyfish";
		public const string SlimeMold = "Mold";
		public const string EggBugEgg = "Egg";
		public const string FireEgg = "FireEgg";
		public const string Popcorn = "Popcorn";
		public const string GooieDuck = "Gooieduck";
		public const string LilyPuck = "Lilypuck";
		public const string GlowWeed = "Glowweed";
		public const string DandelionPeach = "DPeach";
		public const string Neuron = "Neuron";
		public const string Centipede = "Centipede";
		public const string SmallCentipede = "SmCentipede";
		public const string VultureGrub = "Grub";
		public const string SmallNeedleWorm = "NoodleFly";
		public const string Hazer = "Hazer";
		public const string NotCounted = "NotCounted";
	}
	public class SelectedColumns
	{
		public static readonly SelectedColumns All = new(true);
		public static readonly SelectedColumns PersonalityOnly = new(false)
		{
			Sympathy = true,
			Energy = true,
			Bravery = true,
			Nervous = true,
			Aggression = true,
			Dominance = true
		};
		public static readonly SelectedColumns NPCStatsOnly = new(false)
		{
			Met = true,
			Bal = true,
			Size = true,
			Stealth = true,
			Dark = true,
			EyeColor = true,
			H = true,
			S = true,
			L = true,
			Wideness = true
		};
		public static readonly SelectedColumns SlugcatStatsOnly = new(false)
		{
			bodyWeightFac = true,
			generalVisibilityBonus = true,
			visualStealthInSneakMode = true,
			loudnessFac = true,
			lungsFac = true,
			throwingSkill = true,
			poleClimbSpeedFac = true,
			corridorClimbSpeedFac = true,
			runSpeedFac = true
		};
		public static readonly SelectedColumns FoodPrefsOnly = new(false)
		{
			DangleFruit = true,
			WaterNut = true,
			JellyFish = true,
			SlimeMold = true,
			EggBugEgg = true,
			FireEgg = true,
			Popcorn = true,
			GooieDuck = true,
			LilyPuck = true,
			GlowWeed = true,
			DandelionPeach = true,
			Neuron = true,
			Centipede = true,
			SmallCentipede = true,
			VultureGrub = true,
			SmallNeedleWorm = true,
			Hazer = true,
			NotCounted = true
		};
		public static readonly SelectedColumns IDOnly = new(false) { ID = true };
		public SelectedColumns() : this(true) { }
		public SelectedColumns(bool all)
		{
			ID = all;
			Sympathy = all;
			Energy = all;
			Bravery = all;
			Nervous = all;
			Aggression = all;
			Dominance = all;
			Met = all;
			Bal = all;
			Size = all;
			Stealth = all;
			Dark = all;
			EyeColor = all;
			H = all;
			S = all;
			L = all;
			Wideness = all;
			bodyWeightFac = all;
			generalVisibilityBonus = all;
			visualStealthInSneakMode = all;
			loudnessFac = all;
			lungsFac = all;
			throwingSkill = all;
			poleClimbSpeedFac = all;
			corridorClimbSpeedFac = all;
			runSpeedFac = all;
			DangleFruit = all;
			WaterNut = all;
			JellyFish = all;
			SlimeMold = all;
			EggBugEgg = all;
			FireEgg = all;
			Popcorn = all;
			GooieDuck = all;
			LilyPuck = all;
			GlowWeed = all;
			DandelionPeach = all;
			Neuron = all;
			Centipede = all;
			SmallCentipede = all;
			VultureGrub = all;
			SmallNeedleWorm = all;
			Hazer = all;
			NotCounted = all;
		}
		public List<string> GetDesiredColumnVals()
		{
			List<string> desired = new();
			foreach (FieldInfo name in typeof(SelectedColumns).GetFields())
			{
				if (name.IsStatic) continue;
				if ((bool?)name.GetValue(this) == true)
				{
					desired.Add(name.Name);
				}
			}
			return desired;
		}
		public static SelectedColumns Add(IEnumerable<SelectedColumns> set)
		{
			SelectedColumns sdc = new(false);
			bool? flag;
			foreach (SelectedColumns c in set)
			{
				foreach (FieldInfo fi in typeof(SelectedColumns).GetFields())
				{
					flag = (bool?)fi.GetValue(c);
					if ((bool?)fi.GetValue(sdc) == true || flag == false) continue;
					if (flag == true) fi.SetValue(sdc, true);
				}
			}
			return sdc; 
		}
		public bool ID;
		public bool Sympathy;
		public bool Energy;
		public bool Bravery;
		public bool Nervous;
		public bool Aggression;
		public bool Dominance;

		public bool Met;
		public bool Bal;
		public bool Size;
		public bool Stealth;
		public bool Dark;
		public bool EyeColor;
		public bool H;
		public bool S;
		public bool L;
		public bool Wideness;

		public bool bodyWeightFac;
		public bool generalVisibilityBonus;
		public bool visualStealthInSneakMode;
		public bool loudnessFac;
		public bool lungsFac;
		public bool throwingSkill;
		public bool poleClimbSpeedFac;
		public bool corridorClimbSpeedFac;
		public bool runSpeedFac;

		public bool DangleFruit;
		public bool WaterNut;
		public bool JellyFish;
		public bool SlimeMold;
		public bool EggBugEgg;
		public bool FireEgg;
		public bool Popcorn;
		public bool GooieDuck;
		public bool LilyPuck;
		public bool GlowWeed;
		public bool DandelionPeach;
		public bool Neuron;
		public bool Centipede;
		public bool SmallCentipede;
		public bool VultureGrub;
		public bool SmallNeedleWorm;
		public bool Hazer;
		public bool NotCounted;
	}
}
