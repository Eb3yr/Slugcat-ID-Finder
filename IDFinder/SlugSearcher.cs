﻿namespace IDFinder
{
	// Have an ISearcher interface, each creature has its own class that implements it. This'll avoid having a disgusting number of parameters, and SearchParams can be a nested class
	public abstract class Searcher<T>
	{ 
		public abstract ISearchParams SearchParams { get; protected set; }
		public abstract void SetParams(ISearchParams inParams);
		public abstract IEnumerable<KeyValuePair<float, T>> Search();
	}
	public class SlugSearcher
	{
		public SlugParams SearchParams { get; protected set; }
		public SlugSearcher(SlugParams sParams)
		{
			this.SearchParams = sParams;
		}
		public SlugSearcher() : this(new()) { }
		public void SetParams(SlugParams inParams)
		{
			this.SearchParams = inParams;
		}

		private float PersonalityWeight(Personality p)
		{
			float weight = 0f;
			if (SearchParams.Sympathy != null)
				weight += SearchParams.Sympathy.Value.weight * Math.Abs(p.Sympathy - SearchParams.Sympathy.Value.target);
			if (SearchParams.Energy != null)
				weight += SearchParams.Energy.Value.weight * Math.Abs(p.Energy - SearchParams.Energy.Value.target);
			if (SearchParams.Bravery != null)
				weight += SearchParams.Bravery.Value.weight * Math.Abs(p.Bravery - SearchParams.Bravery.Value.target);
			if (SearchParams.Nervous != null)
				weight += SearchParams.Nervous.Value.weight * Math.Abs(p.Nervous - SearchParams.Nervous.Value.target);
			if (SearchParams.Aggression != null)
				weight += SearchParams.Aggression.Value.weight * Math.Abs(p.Aggression - SearchParams.Aggression.Value.target);
			if (SearchParams.Dominance != null)
				weight += SearchParams.Dominance.Value.weight * Math.Abs(p.Dominance - SearchParams.Dominance.Value.target);
			return weight;
		}
		private float NPCStatsWeight(NPCStats npc)
		{
			float weight = 0f;
			if (SearchParams.Met != null)
				weight += SearchParams.Met.Value.weight * Math.Abs(npc.Met - SearchParams.Met.Value.target);
			if (SearchParams.Bal != null)
				weight += SearchParams.Bal.Value.weight * Math.Abs(npc.Bal - SearchParams.Bal.Value.target);
			if (SearchParams.Size != null)
				weight += SearchParams.Size.Value.weight * Math.Abs(npc.Size - SearchParams.Size.Value.target);
			if (SearchParams.Stealth != null)
				weight += SearchParams.Stealth.Value.weight * Math.Abs(npc.Stealth - SearchParams.Stealth.Value.target);
			if (SearchParams.Dark != null)
				weight += SearchParams.Dark.Value.weight * (npc.Dark == SearchParams.Dark.Value.target ? 1 : 0);
			if (SearchParams.EyeColor != null)
				weight += SearchParams.EyeColor.Value.weight * Math.Abs(npc.EyeColor - SearchParams.EyeColor.Value.target);
			if (SearchParams.H != null)
				weight += SearchParams.H.Value.weight * Math.Abs(npc.H - SearchParams.H.Value.target);
			if (SearchParams.S != null)
				weight += SearchParams.S.Value.weight * Math.Abs(npc.S - SearchParams.S.Value.target);
			if (SearchParams.L != null)
				weight += SearchParams.L.Value.weight * Math.Abs(npc.L - SearchParams.L.Value.target);
			if (SearchParams.Wideness != null)
				weight += SearchParams.Wideness.Value.weight * Math.Abs(npc.Wideness - SearchParams.Wideness.Value.target);
			return weight;
		}
		private float SlugcatStatsWeight(SlugcatStats slug)
		{
			float weight = 0f;
			if (SearchParams.BodyWeightFac != null)
				weight += SearchParams.BodyWeightFac.Value.weight * Math.Abs(slug.bodyWeightFac - SearchParams.BodyWeightFac.Value.target);
			if (SearchParams.GeneralVisibilityBonus != null)
				weight += SearchParams.GeneralVisibilityBonus.Value.weight * Math.Abs(slug.generalVisibilityBonus - SearchParams.GeneralVisibilityBonus.Value.target);
			if (SearchParams.VisualStealthInSneakMode != null)
				weight += SearchParams.VisualStealthInSneakMode.Value.weight * Math.Abs(slug.visualStealthInSneakMode - SearchParams.VisualStealthInSneakMode.Value.target);
			if (SearchParams.LoudnessFac != null )
				weight += SearchParams.LoudnessFac.Value.weight * Math.Abs(slug.loudnessFac - SearchParams.LoudnessFac.Value.target);
			if (SearchParams.LungsFac != null)
				weight += SearchParams.LungsFac.Value.weight * Math.Abs(slug.lungsFac - SearchParams.LungsFac.Value.target);
			if (SearchParams.ThrowingSkill != null)
				weight += SearchParams.ThrowingSkill.Value.weight * Math.Abs(slug.throwingSkill - SearchParams.ThrowingSkill.Value.target);
			if (SearchParams.PoleClimbSpeedFac != null)
				weight += SearchParams.PoleClimbSpeedFac.Value.weight * Math.Abs(slug.poleClimbSpeedFac - SearchParams.PoleClimbSpeedFac.Value.target);
			if (SearchParams.CorridorClimbSpeedFac != null)
				weight += SearchParams.CorridorClimbSpeedFac.Value.weight * Math.Abs(slug.corridorClimbSpeedFac - SearchParams.CorridorClimbSpeedFac.Value.target);
			if (SearchParams.RunSpeedFac != null)
				weight += SearchParams.RunSpeedFac.Value.weight * Math.Abs(slug.runSpeedFac - SearchParams.RunSpeedFac.Value.target);
			return weight;
		}

		private float FoodPreferencesWeight(FoodPreferences foodPref)
		{
			float weight = 0f;
			if (SearchParams.DangleFruit != null)
				weight += SearchParams.DangleFruit.Value.weight * Math.Abs(foodPref.DangleFruit - SearchParams.DangleFruit.Value.target);
			if (SearchParams.WaterNut != null)
				weight += SearchParams.WaterNut.Value.weight * Math.Abs(foodPref.WaterNut - SearchParams.WaterNut.Value.target);
			if (SearchParams.JellyFish != null)
				weight += SearchParams.JellyFish.Value.weight * Math.Abs(foodPref.JellyFish - SearchParams.JellyFish.Value.target);
			if (SearchParams.SlimeMold != null)
				weight += SearchParams.SlimeMold.Value.weight * Math.Abs(foodPref.SlimeMold - SearchParams.SlimeMold.Value.target);
			if (SearchParams.EggBugEgg != null)
				weight += SearchParams.EggBugEgg.Value.weight * Math.Abs(foodPref.EggBugEgg - SearchParams.EggBugEgg.Value.target);
			if (SearchParams.FireEgg != null)
				weight += SearchParams.FireEgg.Value.weight * Math.Abs(foodPref.FireEgg - SearchParams.FireEgg.Value.target);
			if (SearchParams.Popcorn != null)
				weight += SearchParams.Popcorn.Value.weight * Math.Abs(foodPref.Popcorn - SearchParams.Popcorn.Value.target);
			if (SearchParams.GooieDuck != null)
				weight += SearchParams.GooieDuck.Value.weight * Math.Abs(foodPref.GooieDuck - SearchParams.GooieDuck.Value.target);
			if (SearchParams.LilyPuck != null)
				weight += SearchParams.LilyPuck.Value.weight * Math.Abs(foodPref.LilyPuck - SearchParams.LilyPuck.Value.target);
			if (SearchParams.GlowWeed != null)
				weight += SearchParams.GlowWeed.Value.weight * Math.Abs(foodPref.GlowWeed - SearchParams.GlowWeed.Value.target);
			if (SearchParams.DandelionPeach != null)
				weight += SearchParams.DandelionPeach.Value.weight * Math.Abs(foodPref.DandelionPeach - SearchParams.DandelionPeach.Value.target);
			if (SearchParams.Neuron != null)
				weight += SearchParams.Neuron.Value.weight * Math.Abs(foodPref.Neuron - SearchParams.Neuron.Value.target);
			if (SearchParams.Centipede != null)
				weight += SearchParams.Centipede.Value.weight * Math.Abs(foodPref.Centipede - SearchParams.Centipede.Value.target);
			if (SearchParams.SmallCentipede != null)
				weight += SearchParams.SmallCentipede.Value.weight * Math.Abs(foodPref.SmallCentipede - SearchParams.SmallCentipede.Value.target);
			if (SearchParams.VultureGrub != null)
				weight += SearchParams.VultureGrub.Value.weight * Math.Abs(foodPref.VultureGrub - SearchParams.VultureGrub.Value.target);
			if (SearchParams.SmallNeedleWorm != null) 
				weight += SearchParams.SmallNeedleWorm.Value.weight * Math.Abs(foodPref.SmallNeedleWorm - SearchParams.SmallNeedleWorm.Value.target);
			if (SearchParams.Hazer != null)
				weight += SearchParams.Hazer.Value.weight * Math.Abs(foodPref.Hazer - SearchParams.Hazer.Value.target);
			if (SearchParams.NotCounted != null) 
				weight += SearchParams.NotCounted.Value.weight * Math.Abs(foodPref.NotCounted - SearchParams.NotCounted.Value.target);
			return weight;
		}
		public static IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore, SlugParams searchParams)
		{
			return new SlugSearcher(searchParams).Search(start, stop, numToStore);
		}
		public IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore, bool logPercents = false)	// logPercents is primitive and doesn't fit anything other than console.
		{
			// Why am I storing an object? Store the ID. All the structs in slugcat need to be instantiated otherwise. ID is also more expandable for other creatures. 
			// I could use stackalloc spans of float key, int ID value, and use another variable to keep track of the position of the largest float instead of searching each time, which would be expensive with high numToStore.
			// --> I could use a queue! Item gets added to the collection to return, the element its added at is added to the queue. This means that when I add the smallest float's position, everything before it has to be dequeued first before it's on the chopping block. This would be perfect. Just need to know if it's efficient. 
			SortedList<float, Slugcat> vals = [];   // smallest value at index 0
			float weight;
			bool saturated = false;
			vals.Capacity = numToStore;
			long percentInterval = ((long)stop - (long)start) / 100;	// long cast avoids int32 overflow edge cases that cause a DivideByZero exception.
			int percentTracker = 0;

			bool personality, npcStats, slugcatStats, foodPreferences;
			personality = !(SearchParams.Sympathy is null && SearchParams.Energy is null && SearchParams.Bravery is null && SearchParams.Nervous is null && SearchParams.Aggression is null && SearchParams.Dominance is null);
			npcStats = !(SearchParams.Met is null && SearchParams.Bal is null && SearchParams.Size is null && SearchParams.Stealth is null && SearchParams.Dark is null && SearchParams.EyeColor is null && SearchParams.H is null && SearchParams.S is null && SearchParams.L is null && SearchParams.Wideness is null);
			slugcatStats = !(SearchParams.BodyWeightFac is null && SearchParams.GeneralVisibilityBonus is null && SearchParams.VisualStealthInSneakMode is null && SearchParams.LoudnessFac is null && SearchParams.LungsFac is null && SearchParams.ThrowingSkill is null && SearchParams.PoleClimbSpeedFac is null && SearchParams.CorridorClimbSpeedFac is null && SearchParams.RunSpeedFac is null);
			foodPreferences = !(SearchParams.DangleFruit is null && SearchParams.WaterNut is null && SearchParams.JellyFish is null && SearchParams.SlimeMold is null && SearchParams.EggBugEgg is null && SearchParams.FireEgg is null && SearchParams.Popcorn is null && SearchParams.GooieDuck is null && SearchParams.LilyPuck is null && SearchParams.GlowWeed is null && SearchParams.DandelionPeach is null && SearchParams.Neuron is null && SearchParams.Centipede is null && SearchParams.SmallCentipede is null && SearchParams.VultureGrub is null && SearchParams.SmallNeedleWorm is null && SearchParams.Hazer is null && SearchParams.NotCounted is null);

			Personality p = new(0);
			NPCStats npc = new(0);
			SlugcatStats slugStats = new(0);
			FoodPreferences foodPref = new(0);
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
					vals.TryAdd(weight, new(i));
					if (vals.Count == vals.Capacity) saturated = true;
				}
				else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
				{
					if (!vals.ContainsKey(weight))
					{
                        vals.RemoveAt(vals.Capacity - 1);
						vals.Add(weight, new(i));
                    }
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
                    vals.TryAdd(weight, new(i));
                    if (vals.Count == vals.Capacity) saturated = true;
                }
                else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
                {
                    if (!vals.ContainsKey(weight))
                    {
                        vals.RemoveAt(vals.Capacity - 1);
                        vals.Add(weight, new(i));
                    }
                }
            }
			return vals;
		}

		#region search bools
		bool personality = false;
		bool npcStats = false;
		bool slugcatStats = false;
		bool foodPreferences = false;

        #endregion
        public async Task<IEnumerable<KeyValuePair<float, int>>> MULTISearch(int start, int stop, int numToStore, bool logPercents = false, int threads = 6)  // logPercents is primitive and doesn't fit anything other than console. Consider async task implementation and progress tracking that way. Does any responsibility for this fall onto the library or only whatever program utilises it?
        {
            SortedList<float, int> vals = [];

			// Do bools here, pass them to SearchIterationsAsync
			// Maybe they should be fields? Searcher gets instantiated anyway

			
			


			// Address edge case int.MaxValue here
            return vals;
        }
		private void SearchIterations(SortedList<float, int> vals, int start, int stop, int numToStore, bool logPercents = false)
		{
            float weight;
            bool saturated = false;
            vals.Capacity = numToStore;
            long percentInterval = ((long)stop - (long)start) / 100;    // long cast avoids int32 overflow edge cases that cause a DivideByZero exception.
            int percentTracker = 0;

            personality = !(SearchParams.Sympathy is null && SearchParams.Energy is null && SearchParams.Bravery is null && SearchParams.Nervous is null && SearchParams.Aggression is null && SearchParams.Dominance is null);
            npcStats = !(SearchParams.Met is null && SearchParams.Bal is null && SearchParams.Size is null && SearchParams.Stealth is null && SearchParams.Dark is null && SearchParams.EyeColor is null && SearchParams.H is null && SearchParams.S is null && SearchParams.L is null && SearchParams.Wideness is null);
            slugcatStats = !(SearchParams.BodyWeightFac is null && SearchParams.GeneralVisibilityBonus is null && SearchParams.VisualStealthInSneakMode is null && SearchParams.LoudnessFac is null && SearchParams.LungsFac is null && SearchParams.ThrowingSkill is null && SearchParams.PoleClimbSpeedFac is null && SearchParams.CorridorClimbSpeedFac is null && SearchParams.RunSpeedFac is null);
            foodPreferences = !(SearchParams.DangleFruit is null && SearchParams.WaterNut is null && SearchParams.JellyFish is null && SearchParams.SlimeMold is null && SearchParams.EggBugEgg is null && SearchParams.FireEgg is null && SearchParams.Popcorn is null && SearchParams.GooieDuck is null && SearchParams.LilyPuck is null && SearchParams.GlowWeed is null && SearchParams.DandelionPeach is null && SearchParams.Neuron is null && SearchParams.Centipede is null && SearchParams.SmallCentipede is null && SearchParams.VultureGrub is null && SearchParams.SmallNeedleWorm is null && SearchParams.Hazer is null && SearchParams.NotCounted is null);

            Personality p = new(0);
            NPCStats npc = new(0);
            SlugcatStats slugStats = new(0);
            FoodPreferences foodPref = new(0);
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
					lock (vals)
					{
						vals.TryAdd(weight, i);
						if (vals.Count == vals.Capacity) saturated = true;
					}
                }
                else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
                {
                    if (!vals.ContainsKey(weight))
                    {
						lock (vals)
						{
							vals.RemoveAt(vals.Capacity - 1);
							vals.Add(weight, i);
						}
                    }
                }
            }
        }
		private static int[][] Chunking(int start, int stop, int threads)
        {
            int[][] chunks = new int[threads][];
            int chunkSize = (stop - start) / threads;
            for (int i = 0; i < threads; i++)
            {
                chunks[i] = [start + i * chunkSize + 1, start + (i + 1) * chunkSize];
            }
            chunks[0][0] = start;
            chunks[threads - 1][1] = stop;

            return chunks;
        }
    }
}
