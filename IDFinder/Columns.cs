using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
		public SelectedColumns() : this(true) { }
		public SelectedColumns(bool all)
		{
			FieldInfo[] fiArr = typeof(SelectedColumns).GetFields();
			foreach (FieldInfo fi in fiArr)
			{
				if (all)
				{
					fi.SetValue(this, true);
				}
				else
				{
					fi.SetValue(this, false);
				}
			}
		}
		public List<string> GetDesiredColumnVals()
		{
			List<string> desired = new();
			foreach (FieldInfo name in typeof(SelectedColumns).GetFields())
			{
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
		public static SelectedColumns PersonalityOnly()
		{
			SelectedColumns sdc = new(false)
			{
				Sympathy = true,
				Energy = true,
				Bravery = true,
				Nervous = true,
				Aggression= true,
				Dominance = true
			};
			return sdc;
		}
		public static SelectedColumns StatsOnly()
		{
			SelectedColumns sdc = new(false)
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
			return sdc;
		}
		public static SelectedColumns FoodPrefOnly()
		{
			SelectedColumns sdc = new(false)
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
			return sdc;
		}
		public static SelectedColumns IDOnly()
		{
			return new(false) { ID = true };
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
