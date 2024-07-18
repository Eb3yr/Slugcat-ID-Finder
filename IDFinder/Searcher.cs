using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IDFinder
{
	public class Searcher
	{
		public class SearchParams
		{
			#region Properties
			public (float target, float weight)? Sympathy = null;
			public (float target, float weight)? Energy = null;
			public (float target, float weight)? Bravery = null;
			public (float target, float weight)? Nervous = null;
			public (float target, float weight)? Aggression = null;
			public (float target, float weight)? Dominance = null;

			public (float target, float weight)? Met = null;
			public (float target, float weight)? Bal = null;
			public (float target, float weight)? Size = null;
			public (float target, float weight)? Stealth = null;
			public (bool target, float weight)? Dark = null;
			public (float target, float weight)? EyeColor = null;
			public (float target, float weight)? H = null;
			public (float target, float weight)? S = null;
			public (float target, float weight)? L = null;
			public (float target, float weight)? Wideness = null;

			public (float target, float weight)? BodyWeightFac = null;
			public (float target, float weight)? GeneralVisibilityBonus = null;
			public (float target, float weight)? VisualStealthInSneakMode = null;
			public (float target, float weight)? LoudnessFac = null;
			public (float target, float weight)? LungsFac = null;
			public (int target, float weight)? ThrowingSkill = null;
			public (float target, float weight)? PoleClimbSpeedFac = null;
			public (float target, float weight)? CorridorClimbSpeedFac = null;
			public (float target, float weight)? RunSpeedFac = null;

			public (float target, float weight)? DangleFruit = null;
			public (float target, float weight)? WaterNut = null;
			public (float target, float weight)? JellyFish = null;
			public (float target, float weight)? SlimeMold = null;
			public (float target, float weight)? EggBugEgg = null;
			public (float target, float weight)? FireEgg = null;
			public (float target, float weight)? Popcorn = null;
			public (float target, float weight)? GooieDuck = null;
			public (float target, float weight)? LilyPuck = null;
			public (float target, float weight)? GlowWeed = null;
			public (float target, float weight)? DandelionPeach = null;
			public (float target, float weight)? Neuron = null;
			public (float target, float weight)? Centipede = null;
			public (float target, float weight)? SmallCentipede = null;
			public (float target, float weight)? VultureGrub = null;
			public (float target, float weight)? SmallNeedleWorm = null;
			public (float target, float weight)? Hazer = null;
			public (float target, float weight)? NotCounted = null;
			#endregion
		}

		SearchParams sParams;
		public Searcher(SearchParams sParams)
		{
			this.sParams = sParams;
		}
		public Searcher() : this(new()) { }
		public void SetParams(SearchParams sParams)
		{
			this.sParams = sParams;
		}

		private float PersonalityWeight(Personality p)
		{
			float weight = 0f;
			if (sParams.Sympathy != null)
				weight += sParams.Sympathy.Value.weight * Math.Abs(p.Sympathy - sParams.Sympathy.Value.target);
			if (sParams.Energy != null)
				weight += sParams.Energy.Value.weight * Math.Abs(p.Energy - sParams.Energy.Value.target);
			if (sParams.Bravery != null)
				weight += sParams.Bravery.Value.weight * Math.Abs(p.Bravery - sParams.Bravery.Value.target);
			if (sParams.Nervous != null)
				weight += sParams.Nervous.Value.weight * Math.Abs(p.Nervous - sParams.Nervous.Value.target);
			if (sParams.Aggression != null)
				weight += sParams.Aggression.Value.weight * Math.Abs(p.Aggression - sParams.Aggression.Value.target);
			if (sParams.Dominance != null)
				weight += sParams.Dominance.Value.weight * Math.Abs(p.Dominance - sParams.Dominance.Value.target);
			return weight;
		}
		private float NPCStatsWeight(NPCStats npc)
		{
			float weight = 0f;
			if (sParams.Met != null)
				weight += sParams.Met.Value.weight * Math.Abs(npc.Met - sParams.Met.Value.target);
			if (sParams.Bal != null)
				weight += sParams.Bal.Value.weight * Math.Abs(npc.Bal - sParams.Bal.Value.target);
			if (sParams.Size != null)
				weight += sParams.Size.Value.weight * Math.Abs(npc.Size - sParams.Size.Value.target);
			if (sParams.Stealth != null)
				weight += sParams.Stealth.Value.weight * Math.Abs(npc.Stealth - sParams.Stealth.Value.target);
			if (sParams.Dark != null)
				weight += sParams.Dark.Value.weight * (npc.Dark == sParams.Dark.Value.target ? 1 : 0);
			if (sParams.EyeColor != null)
				weight += sParams.EyeColor.Value.weight * Math.Abs(npc.EyeColor - sParams.EyeColor.Value.target);
			if (sParams.H != null)
				weight += sParams.H.Value.weight * Math.Abs(npc.H - sParams.H.Value.target);
			if (sParams.S != null)
				weight += sParams.S.Value.weight * Math.Abs(npc.S - sParams.S.Value.target);
			if (sParams.L != null)
				weight += sParams.L.Value.weight * Math.Abs(npc.L - sParams.L.Value.target);
			if (sParams.Wideness != null)
				weight += sParams.Wideness.Value.weight * Math.Abs(npc.Wideness - sParams.Wideness.Value.target);
			return weight;
		}
		private float SlugcatStatsWeight(SlugcatStats slug)
		{
			float weight = 0f;
			if (sParams.BodyWeightFac != null)
				weight += sParams.BodyWeightFac.Value.weight * Math.Abs(slug.bodyWeightFac - sParams.BodyWeightFac.Value.target);
			if (sParams.GeneralVisibilityBonus != null)
				weight += sParams.GeneralVisibilityBonus.Value.weight * Math.Abs(slug.generalVisibilityBonus - sParams.GeneralVisibilityBonus.Value.target);
			if (sParams.VisualStealthInSneakMode != null)
				weight += sParams.VisualStealthInSneakMode.Value.weight * Math.Abs(slug.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.Value.target);
			if (sParams.LoudnessFac != null )
				weight += sParams.LoudnessFac.Value.weight * Math.Abs(slug.loudnessFac - sParams.LoudnessFac.Value.target);
			if (sParams.LungsFac != null)
				weight += sParams.LungsFac.Value.weight * Math.Abs(slug.lungsFac - sParams.LungsFac.Value.target);
			if (sParams.ThrowingSkill != null)
				weight += sParams.ThrowingSkill.Value.weight * Math.Abs(slug.throwingSkill - sParams.ThrowingSkill.Value.target);
			if (sParams.PoleClimbSpeedFac != null)
				weight += sParams.PoleClimbSpeedFac.Value.weight * Math.Abs(slug.poleClimbSpeedFac - sParams.PoleClimbSpeedFac.Value.target);
			if (sParams.CorridorClimbSpeedFac != null)
				weight += sParams.CorridorClimbSpeedFac.Value.weight * Math.Abs(slug.corridorClimbSpeedFac - sParams.CorridorClimbSpeedFac.Value.target);
			if (sParams.RunSpeedFac != null)
				weight += sParams.RunSpeedFac.Value.weight * Math.Abs(slug.runSpeedFac - sParams.RunSpeedFac.Value.target);
			return weight;
		}

		private float FoodPreferencesWeight(FoodPreferences foodPref)
		{
			float weight = 0f;
			if (sParams.DangleFruit != null)
				weight += sParams.DangleFruit.Value.weight * Math.Abs(foodPref.DangleFruit - sParams.DangleFruit.Value.target);
			if (sParams.WaterNut != null)
				weight += sParams.WaterNut.Value.weight * Math.Abs(foodPref.WaterNut - sParams.WaterNut.Value.target);
			if (sParams.JellyFish != null)
				weight += sParams.JellyFish.Value.weight * Math.Abs(foodPref.JellyFish - sParams.JellyFish.Value.target);
			if (sParams.SlimeMold != null)
				weight += sParams.SlimeMold.Value.weight * Math.Abs(foodPref.SlimeMold - sParams.SlimeMold.Value.target);
			if (sParams.EggBugEgg != null)
				weight += sParams.EggBugEgg.Value.weight * Math.Abs(foodPref.EggBugEgg - sParams.EggBugEgg.Value.target);
			if (sParams.FireEgg != null)
				weight += sParams.FireEgg.Value.weight * Math.Abs(foodPref.FireEgg - sParams.FireEgg.Value.target);
			if (sParams.Popcorn != null)
				weight += sParams.Popcorn.Value.weight * Math.Abs(foodPref.Popcorn - sParams.Popcorn.Value.target);
			if (sParams.GooieDuck != null)
				weight += sParams.GooieDuck.Value.weight * Math.Abs(foodPref.GooieDuck - sParams.GooieDuck.Value.target);
			if (sParams.LilyPuck != null)
				weight += sParams.LilyPuck.Value.weight * Math.Abs(foodPref.LilyPuck - sParams.LilyPuck.Value.target);
			if (sParams.GlowWeed != null)
				weight += sParams.GlowWeed.Value.weight * Math.Abs(foodPref.GlowWeed - sParams.GlowWeed.Value.target);
			if (sParams.DandelionPeach != null)
				weight += sParams.DandelionPeach.Value.weight * Math.Abs(foodPref.DandelionPeach - sParams.DandelionPeach.Value.target);
			if (sParams.Neuron != null)
				weight += sParams.Neuron.Value.weight * Math.Abs(foodPref.Neuron - sParams.Neuron.Value.target);
			if (sParams.Centipede != null)
				weight += sParams.Centipede.Value.weight * Math.Abs(foodPref.Centipede - sParams.Centipede.Value.target);
			if (sParams.SmallCentipede != null)
				weight += sParams.SmallCentipede.Value.weight * Math.Abs(foodPref.SmallCentipede - sParams.SmallCentipede.Value.target);
			if (sParams.VultureGrub != null)
				weight += sParams.VultureGrub.Value.weight * Math.Abs(foodPref.VultureGrub - sParams.VultureGrub.Value.target);
			if (sParams.SmallNeedleWorm != null) 
				weight += sParams.SmallNeedleWorm.Value.weight * Math.Abs(foodPref.SmallNeedleWorm - sParams.SmallNeedleWorm.Value.target);
			if (sParams.Hazer != null)
				weight += sParams.Hazer.Value.weight * Math.Abs(foodPref.Hazer - sParams.Hazer.Value.target);
			if (sParams.NotCounted != null) 
				weight += sParams.NotCounted.Value.weight * Math.Abs(foodPref.NotCounted - sParams.NotCounted.Value.target);
			return weight;
		}
		public static IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore, SearchParams searchParams)
		{
			return new Searcher(searchParams).Search(start, stop, numToStore);
		}
		public IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore, bool logPercents = false)
		{
			SortedList<float, Slugcat> vals = [];   // smallest value at index 0
			float weight;
			bool saturated = false;
			vals.Capacity = numToStore;
			long percentInterval = ((long)stop - (long)start) / 100;	// long cast avoids int32 overflow edge cases that cause a DivideByZero exception.
			int percentTracker = 0;

			bool personality, npcStats, slugcatStats, foodPreferences;
			personality = !(sParams.Sympathy is null && sParams.Energy is null && sParams.Bravery is null && sParams.Nervous is null && sParams.Aggression is null && sParams.Dominance is null);
			npcStats = !(sParams.Met is null && sParams.Bal is null && sParams.Size is null && sParams.Stealth is null && sParams.Dark is null && sParams.EyeColor is null && sParams.H is null && sParams.S is null && sParams.L is null && sParams.Wideness is null);
			slugcatStats = !(sParams.BodyWeightFac is null && sParams.GeneralVisibilityBonus is null && sParams.VisualStealthInSneakMode is null && sParams.LoudnessFac is null && sParams.LungsFac is null && sParams.ThrowingSkill is null && sParams.PoleClimbSpeedFac is null && sParams.CorridorClimbSpeedFac is null && sParams.RunSpeedFac is null);
			foodPreferences = !(sParams.DangleFruit is null && sParams.WaterNut is null && sParams.JellyFish is null && sParams.SlimeMold is null && sParams.EggBugEgg is null && sParams.FireEgg is null && sParams.Popcorn is null && sParams.GooieDuck is null && sParams.LilyPuck is null && sParams.GlowWeed is null && sParams.DandelionPeach is null && sParams.Neuron is null && sParams.Centipede is null && sParams.SmallCentipede is null && sParams.VultureGrub is null && sParams.SmallNeedleWorm is null && sParams.Hazer is null && sParams.NotCounted is null);

			Personality? p = null;
			NPCStats? npc = null;
			SlugcatStats? slugStats = null;
			FoodPreferences? foodPref = null;
			for (int i = start; i < stop; i++)
			{
				if (logPercents && (i - start) % percentInterval == 0)
				{
					percentTracker++;
					Console.WriteLine($"{percentTracker}%");
				}
				weight = 0f;
				if (personality)
				{
					p = new(i);
					weight += PersonalityWeight(p);
				}
				if (npcStats)
				{
					npc = new(i);
					weight += NPCStatsWeight(npc);
				}
				if (slugcatStats) 
				{
					if (!npcStats) npc = new(i);
					slugStats = new(i, npc);
					weight += SlugcatStatsWeight(slugStats);
				}
				if (foodPreferences)
				{
					if (!personality) p = new(i);
					foodPref = new(i, p);
					weight += FoodPreferencesWeight(foodPref);
				}

				if (!saturated && vals.Count < numToStore)
				{
					vals.Add(weight, new(i));
					if (vals.Count == vals.Capacity) saturated = true;
				}
				else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
				{
					vals.RemoveAt(vals.Capacity - 1);
					vals.Add(weight, new(i));
				}
			}
			if (stop == int.MaxValue)	// edge case to prevent overflow and infinite looping when searching up to the largest int32 integer. 
			{
				// Duplicate of the previous for loop's contents. Should that loop be altered, so should here. Consider moving it out into an inlined method.
				int i = stop;
                weight = 0f;
                if (personality)
                {
                    p = new(i);
                    weight += PersonalityWeight(p);
                }
                if (npcStats)
                {
                    npc = new(i);
                    weight += NPCStatsWeight(npc);
                }
                if (slugcatStats)
                {
                    if (!npcStats) npc = new(i);
                    slugStats = new(i, npc);
                    weight += SlugcatStatsWeight(slugStats);
                }
                if (foodPreferences)
                {
                    if (!personality) p = new(i);
                    foodPref = new(i, p);
                    weight += FoodPreferencesWeight(foodPref);
                }

                if (!saturated && vals.Count < numToStore)
                {
                    vals.Add(weight, new(i));
                    if (vals.Count == vals.Capacity) saturated = true;
                }
                else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
                {
                    vals.RemoveAt(vals.Capacity - 1);
					vals.TryAdd(weight, new(i));	// TryAdd to avoid rare occassions where two identical weights are generated. SortedList does not allow duplicate keys and throws an Exception.
				}
            }
			return vals;
		}
		/*public async IEnumerable<KeyValuePair<float, Slugcat>> SearchMultithreaded(int start, int stop, int numToStore, int threads = 1)
		{
			if (threads == 1)
				return Search(start, stop, numToStore);

			SortedList<float, Slugcat> slugs = [];
			IEnumerable<KeyValuePair<float, Slugcat>> results = [];
            int[] markers = new int[threads + 1];
            int count = stop - start;
            int interval = count / threads;

            for (int i = 0; i < threads; i++)
            {
                markers[i] = start + i * interval;
            }
            markers[threads] = stop;

			for (int i = 0; i < markers.Length - 1; i++)
			{
				// I don't know if this will work. Since I'm writing to results, will it wait until this task finishes before starting the next iteration?
				results = results.Concat(await Task.Run(() => Search(markers[i], markers[i + 1], numToStore)));
			}


            foreach (KeyValuePair<float, Slugcat> kvp in results)
				slugs.Add(kvp.Key, kvp.Value);

			return slugs.Take(numToStore);
		}*/
	}
}
