﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity_XORShift;

namespace IDFinder
{
	public class Slugcat
	{
		public int ID { get; private set; }
		public Personality Personality { get; private set; }
		public NPCStats Stats { get; private set; }
		public FoodPreferences FoodPreferences { get; private set; }
		public Slugcat(int ID)
		{
			this.ID = ID;
			Personality = new Personality(ID);
			Stats = new NPCStats(ID);
			FoodPreferences = new(Personality, ID);
		}
	}

	public class Personality
	{
		public float Sympathy { get; private set; }
		public float Energy { get; private set; }
		public float Bravery { get; private set; }
		public float Nervous { get; private set; }
		public float Aggression { get; private set; }
		public float Dominance { get; private set; }
		public Personality(int seed)
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
			//XORShift128.InitSeed(seed);
		}
		public static Personality GetPersonality(int seed)
		{
			Personality p = new(seed);
			return p;
		}
		

		//public static int FindPersonalityOwner(Personality inP) { }
	}
	public class NPCStats
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
	public class FoodPreferences
	{
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
		public FoodPreferences(Personality p, int ID)
		{
			float[] preferences = GetPreferences(p, ID).Values.ToArray();
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
		public FoodPreferences(int ID) : this(new(ID), ID) { }
		public static Dictionary<Food, float> GetPreferences(Personality p, int ID)
		{
			Dictionary<Food, float> foodPreference;
			XORShift128.InitSeed(ID);
			foodPreference = new();
			Food f;
			float num, num2;

			foreach (var i in Enum.GetValues<Food>())
			{
				f = i;
				switch (f)
				{
					default:
						num = num2 = 0f;
						break;
					case Food.DangleFruit:
						num = p.Nervous;
						num2 = p.Energy;
						break;
					case Food.WaterNut:
						num = p.Sympathy;
						num2 = p.Aggression;
						break;
					case Food.JellyFish:
						num = p.Energy;
						num2 = p.Nervous;
						break;
					case Food.SlimeMold:
						num = p.Energy;
						num2 = p.Aggression;
						break;
					case Food.EggBugEgg:
						num = p.Dominance;
						num2 = p.Energy;
						break;
					case Food.FireEgg:
						num = p.Aggression;
						num2 = p.Sympathy;
						break;
					case Food.Popcorn:
						num = p.Dominance;
						num2 = p.Bravery;
						break;
					case Food.GooieDuck:
						num = p.Sympathy;
						num2 = p.Bravery;
						break;
					case Food.LilyPuck:
						num = p.Aggression;
						num2 = p.Nervous;
						break;
					case Food.GlowWeed:
						num = p.Nervous;
						num2 = p.Energy;
						break;
					case Food.DandelionPeach:
						num = p.Bravery;
						num2 = p.Dominance;
						break;
					case Food.Neuron:
						num = p.Bravery;
						num2 = p.Nervous;
						break;
					case Food.Centipede:
						num = p.Bravery;
						num2 = p.Dominance;
						break;
					case Food.SmallCentipede:
						num = p.Energy;
						num2 = p.Aggression;
						break;
					case Food.VultureGrub:
						num = p.Dominance;
						num2 = p.Bravery;
						break;
					case Food.SmallNeedleWorm:
						num = p.Aggression;
						num2 = p.Sympathy;
						break;
					case Food.Hazer:
						num = p.Nervous;
						num2 = p.Sympathy;
						break;
				}
				num *= Custom.PushFromHalf(XORShift128.NextFloat(), 2f);
				num2 *= Custom.PushFromHalf(XORShift128.NextFloat(), 2f);
				foodPreference.Add(f, Math.Clamp(float.Lerp(num - num2, float.Lerp(-1f, 1f, Custom.PushFromHalf(XORShift128.NextFloat(), 2f)), Custom.PushFromHalf(XORShift128.NextFloat(), 2f)), -1f, 1f));
			}
			return foodPreference;
		}
	}
}
