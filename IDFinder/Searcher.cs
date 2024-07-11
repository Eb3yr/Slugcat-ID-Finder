namespace IDFinder
{
	// Could turn Searcher static. Consider it.
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

			public (float target, float weight)? bodyWeightFac = null;
			public (float target, float weight)? generalVisibilityBonus = null;
			public (float target, float weight)? visualStealthInSneakMode = null;
			public (float target, float weight)? loudnessFac = null;
			public (float target, float weight)? lungsFac = null;
			public (int target, float weight)? throwingSkill = null;
			public (float target, float weight)? poleClimbSpeedFac = null;
			public (float target, float weight)? corridorClimbSpeedFac = null;
			public (float target, float weight)? runSpeedFac = null;

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
		const int chunkSize = 100000;	// do I need this? Could be useful for multithreading, to either dynamically generate it based on thread count or let the user select it
		public Searcher(SearchParams sParams)
		{
			this.sParams = sParams;
		}
		public Searcher() : this(new()) { }
		public void SetParams(SearchParams sParams)
		{
			this.sParams = sParams;
		}
		public static (int startIndex, int endIndex)[] Chunk(int start, int stop)
		{
			int s = (int)Math.Ceiling((double)(stop - start) / chunkSize);
			if (s == 0 || s == 1) return [(start, stop)];
			(int startIndex, int endIndex)[] chunks = new (int startIndex, int endIndex)[s];
			chunks[0].startIndex = start;
			chunks[chunks.Length - 1].endIndex = stop;
			for (int i = 1; i < chunks.Length; i++)
			{
				chunks[i - 1].endIndex = chunks[i - 1].startIndex + chunkSize - 1;
				chunks[i].startIndex = chunks[i - 1].endIndex + 1;
			}

			return chunks;
		}
		public void Search(int start, int stop)
		{
			#region Iterating through fields
			bool personality = sParams.Sympathy == null && sParams.Energy == null && sParams.Bravery == null && sParams.Nervous == null && sParams.Aggression == null && sParams.Dominance == null;
			bool stats = sParams.Met == null && sParams.Bal == null && sParams.Size == null && sParams.Stealth == null && sParams.Dark == null && sParams.EyeColor == null && sParams.H == null && sParams.S == null && sParams.L == null && sParams.Wideness == null;
			bool foodPrefs = sParams.DangleFruit == null && sParams.WaterNut == null && sParams.JellyFish == null && sParams.SlimeMold == null && sParams.EggBugEgg == null && sParams.FireEgg == null && sParams.Popcorn == null && sParams.GooieDuck == null && sParams.LilyPuck == null && sParams.GlowWeed == null && sParams.DandelionPeach == null && sParams.Neuron == null && sParams.Centipede == null && sParams.SmallCentipede == null && sParams.VultureGrub == null && sParams.SmallNeedleWorm == null && sParams.Hazer == null && sParams.NotCounted == null;
			Slugcat sc;
			for (int i = start; i < stop; i++)
			{
				sc = new(i, personality, stats, foodPrefs);

				if (personality)
				{

				}
				if (stats)
				{

				}
				if (foodPrefs)
				{

				}
				// I don't like how annoyingly exclusive this is, and how it could end up with loads of if statements for different permutations of personality, stats, and foodPrefs.
			}

			#endregion
		}
		// Make this static
		public IEnumerable<KeyValuePair<float, Slugcat>> NewSearch(int start, int stop, int numToStore)
		{
			SortedList<float, Slugcat> vals = [];	// smallest value at index 0

			Slugcat sc;
			float weight;
			for (int i = start; i < stop; i++)
			{
				sc = new(i);
				weight = 0f;

				#region Iterating through everything
				




				#endregion

				if (vals.Count < numToStore) vals.Add(weight, sc);
				else if (vals.GetKeyAtIndex(0) < weight)
				{
					vals.RemoveAt(0);
					vals.Add(weight, sc);
				}
			}

			return vals.Reverse();
		}
	}
}
