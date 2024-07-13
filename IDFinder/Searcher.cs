using System.Reflection;
using System.Runtime.CompilerServices;

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
		const int chunkSize = 100000;   // do I need this? Could be useful for multithreading, to either dynamically generate it based on thread count or let the user select it
		public Searcher(SearchParams sParams)
		{
			this.sParams = sParams;
		}
		public Searcher() : this(new()) { }
		public void SetParams(SearchParams sParams)
		{
			this.sParams = sParams;
		}
		private IEnumerable<FieldInfo> GetRelevantFields(FieldInfo[] searchFields)
		{
			List<FieldInfo> relevantFields = [];
			FieldInfo? f;
			foreach (FieldInfo fi in searchFields)
			{
				f = typeof(Searcher).GetField(fi.Name);
				if (f != null) relevantFields.Add(f);
				else throw new Exception("Name discrepancy between Searcher and SearchParams fields");
			}
			return relevantFields;
		}

		// Split each class being searched into its own method. Eg Personality, NPCStats, etc and [MethodImpl(MethodImplOptions.AggressiveInlining)] them. This'll cut down on how many if statements I have to use. Maybe add some functionality to the sParams class to allow me to easily determine whether or not an instance contains certain groups, possibly also inline that.

		#region Searches
		public static IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore, SearchParams searchParams)
		{
			return Search(Enumerable.Range(start, stop - start + 1), numToStore, searchParams);
		}
		public static IEnumerable<KeyValuePair<float, Slugcat>> Search(IEnumerable<int> range, int numToStore, SearchParams searchParams)
		{
			return new Searcher(searchParams).Search(range, numToStore);
		}
		public IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore)
		{
			return Search(Enumerable.Range(start, stop - start + 1), numToStore);
		}
		
		public IEnumerable<KeyValuePair<float, Slugcat>> Search(IEnumerable<int> range, int numToStore)
		{
			SortedList<float, Slugcat> vals = [];   // smallest value at index 0

			Slugcat sc;
			float weight;
			bool saturated = false;
			vals.Capacity = numToStore;
			foreach (int i in range)
			{
				sc = new(i);
				weight = 0f;

				// The following is why chunking might be useful. Is it faster to pre-generate groups of slugcats? Possibly threading to generate the slugcats and enqueue them to be searched? Or assigning set subregions between start and stop to multiple threads?
				#region Iterating through everything

				if (sParams.Sympathy != null)
				{
					weight += sParams.Sympathy.Value.weight * Math.Abs(sc.Personality.Sympathy - sParams.Sympathy.Value.target);
				}
				if (sParams.Energy != null)
				{
					weight += sParams.Energy.Value.weight * Math.Abs(sc.Personality.Energy - sParams.Energy.Value.target);
				}
				if (sParams.Bravery != null)
				{
					weight += sParams.Bravery.Value.weight * Math.Abs(sc.Personality.Bravery - sParams.Bravery.Value.target);
				}
				if (sParams.Nervous != null)
				{
					weight += sParams.Nervous.Value.weight * Math.Abs(sc.Personality.Nervous - sParams.Nervous.Value.target);
				}
				if (sParams.Aggression != null)
				{
					weight += sParams.Aggression.Value.weight * Math.Abs(sc.Personality.Aggression - sParams.Aggression.Value.target);
				}
				if (sParams.Dominance != null)
				{
					weight += sParams.Dominance.Value.weight * Math.Abs(sc.Personality.Dominance - sParams.Dominance.Value.target);
				}

				if (sParams.Met != null)
				{
					weight += sParams.Met.Value.weight * Math.Abs(sc.NPCStats.Met - sParams.Met.Value.target);
				}
				if (sParams.Bal != null)
				{
					weight += sParams.Bal.Value.weight * Math.Abs(sc.NPCStats.Bal - sParams.Bal.Value.target);
				}
				if (sParams.Size != null)
				{
					weight += sParams.Size.Value.weight * Math.Abs(sc.NPCStats.Size - sParams.Size.Value.target);
				}
				if (sParams.Stealth != null)
				{
					weight += sParams.Stealth.Value.weight * Math.Abs(sc.NPCStats.Stealth - sParams.Stealth.Value.target);
				}
				if (sParams.Dark != null)
				{
					weight += sParams.Dark.Value.weight * (sc.NPCStats.Dark == sParams.Dark.Value.target ? 1 : 0);
				}
				if (sParams.EyeColor != null)
				{
					weight += sParams.EyeColor.Value.weight * Math.Abs(sc.NPCStats.EyeColor - sParams.EyeColor.Value.target);
				}
				if (sParams.H != null)
				{
					weight += sParams.H.Value.weight * Math.Abs(sc.NPCStats.H - sParams.H.Value.target);
				}
				if (sParams.S != null)
				{
					weight += sParams.S.Value.weight * Math.Abs(sc.NPCStats.S - sParams.S.Value.target);
				}
				if (sParams.L != null)
				{
					weight += sParams.L.Value.weight * Math.Abs(sc.NPCStats.L - sParams.L.Value.target);
				}
				if (sParams.Wideness != null)
				{
					weight += sParams.Wideness.Value.weight * Math.Abs(sc.NPCStats.Wideness - sParams.Wideness.Value.target);
				}

				if (sParams.BodyWeightFac != null)
				{
					weight += sParams.BodyWeightFac.Value.weight * Math.Abs(sc.SlugcatStats.bodyWeightFac - sParams.BodyWeightFac.Value.target);
				}
				if (sParams.GeneralVisibilityBonus != null)
				{
					weight += sParams.GeneralVisibilityBonus.Value.weight * Math.Abs(sc.SlugcatStats.generalVisibilityBonus - sParams.GeneralVisibilityBonus.Value.target);
				}
				if (sParams.VisualStealthInSneakMode != null)
				{
					weight += sParams.VisualStealthInSneakMode.Value.weight * Math.Abs(sc.SlugcatStats.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.Value.target);
				}
				if (sParams.LoudnessFac != null)
				{
					weight += sParams.LoudnessFac.Value.weight * Math.Abs(sc.SlugcatStats.loudnessFac - sParams.LoudnessFac.Value.target);
				}
				if (sParams.LungsFac != null)
				{
					weight += sParams.LungsFac.Value.weight * Math.Abs(sc.SlugcatStats.lungsFac - sParams.LungsFac.Value.target);
				}
				if (sParams.ThrowingSkill != null)
				{
					weight += sParams.ThrowingSkill.Value.weight * Math.Abs(sc.SlugcatStats.throwingSkill - sParams.ThrowingSkill.Value.target);
				}
				if (sParams.PoleClimbSpeedFac != null)
				{
					weight += sParams.PoleClimbSpeedFac.Value.weight * Math.Abs(sc.SlugcatStats.poleClimbSpeedFac - sParams.PoleClimbSpeedFac.Value.target);
				}
				if (sParams.CorridorClimbSpeedFac != null)
				{
					weight += sParams.CorridorClimbSpeedFac.Value.weight * Math.Abs(sc.SlugcatStats.corridorClimbSpeedFac - sParams.CorridorClimbSpeedFac.Value.target);
				}
				if (sParams.RunSpeedFac != null)
				{
					weight += sParams.RunSpeedFac.Value.weight * Math.Abs(sc.SlugcatStats.runSpeedFac - sParams.RunSpeedFac.Value.target);
				}

				if (sParams.DangleFruit != null)
				{
					weight += sParams.DangleFruit.Value.weight * Math.Abs(sc.FoodPreferences.DangleFruit - sParams.DangleFruit.Value.target);
				}
				if (sParams.WaterNut != null)
				{
					weight += sParams.WaterNut.Value.weight * Math.Abs(sc.FoodPreferences.WaterNut - sParams.WaterNut.Value.target);
				}
				if (sParams.JellyFish != null)
				{
					weight += sParams.JellyFish.Value.weight * Math.Abs(sc.FoodPreferences.JellyFish - sParams.JellyFish.Value.target);
				}
				if (sParams.SlimeMold != null)
				{
					weight += sParams.SlimeMold.Value.weight * Math.Abs(sc.FoodPreferences.SlimeMold - sParams.SlimeMold.Value.target);
				}
				if (sParams.EggBugEgg != null)
				{
					weight += sParams.EggBugEgg.Value.weight * Math.Abs(sc.FoodPreferences.EggBugEgg - sParams.EggBugEgg.Value.target);
				}
				if (sParams.FireEgg != null)
				{
					weight += sParams.FireEgg.Value.weight * Math.Abs(sc.FoodPreferences.FireEgg - sParams.FireEgg.Value.target);
				}
				if (sParams.Popcorn != null)
				{
					weight += sParams.Popcorn.Value.weight * Math.Abs(sc.FoodPreferences.Popcorn - sParams.Popcorn.Value.target);
				}
				if (sParams.GooieDuck != null)
				{
					weight += sParams.GooieDuck.Value.weight * Math.Abs(sc.FoodPreferences.GooieDuck - sParams.GooieDuck.Value.target);
				}
				if (sParams.LilyPuck != null)
				{
					weight += sParams.LilyPuck.Value.weight * Math.Abs(sc.FoodPreferences.LilyPuck - sParams.LilyPuck.Value.target);
				}
				if (sParams.GlowWeed != null)
				{
					weight += sParams.GlowWeed.Value.weight * Math.Abs(sc.FoodPreferences.GlowWeed - sParams.GlowWeed.Value.target);
				}
				if (sParams.DandelionPeach != null)
				{
					weight += sParams.DandelionPeach.Value.weight * Math.Abs(sc.FoodPreferences.DandelionPeach - sParams.DandelionPeach.Value.target);
				}
				if (sParams.Neuron != null)
				{
					weight += sParams.Neuron.Value.weight * Math.Abs(sc.FoodPreferences.Neuron - sParams.Neuron.Value.target);
				}
				if (sParams.Centipede != null)
				{
					weight += sParams.Centipede.Value.weight * Math.Abs(sc.FoodPreferences.Centipede - sParams.Centipede.Value.target);
				}
				if (sParams.SmallCentipede != null)
				{
					weight += sParams.SmallCentipede.Value.weight * Math.Abs(sc.FoodPreferences.SmallCentipede - sParams.SmallCentipede.Value.target);
				}
				if (sParams.VultureGrub != null)
				{
					weight += sParams.VultureGrub.Value.weight * Math.Abs(sc.FoodPreferences.VultureGrub - sParams.VultureGrub.Value.target);
				}
				if (sParams.SmallNeedleWorm != null)
				{
					weight += sParams.SmallNeedleWorm.Value.weight * Math.Abs(sc.FoodPreferences.SmallNeedleWorm - sParams.SmallNeedleWorm.Value.target);
				}
				if (sParams.Hazer != null)
				{
					weight += sParams.Hazer.Value.weight * Math.Abs(sc.FoodPreferences.Hazer - sParams.Hazer.Value.target);
				}
				if (sParams.NotCounted != null)
				{
					weight += sParams.NotCounted.Value.weight * Math.Abs(sc.FoodPreferences.NotCounted - sParams.NotCounted.Value.target);
				}

				#endregion

				if (!saturated && vals.Count < numToStore)
				{
					vals.Add(weight, sc);
					if (vals.Count == vals.Capacity) saturated = true;
				}
				else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
				{
					vals.RemoveAt(vals.Capacity - 1);
					vals.Add(weight, sc);
				}
			}

			return vals;
		}

		// https://timdeschryver.dev/blog/process-your-list-in-parallel-to-make-it-faster-in-dotnet
		public IEnumerable<KeyValuePair<float, Slugcat>> BatchSearch(IEnumerable<int> range, int batchSize, int numToStore)
		{
			throw new NotImplementedException("Batch searching has not yet been implemented.");
			IEnumerable<int[]> chunks = range.Chunk(batchSize);
			SortedList<float, Slugcat> vals = [];   // smallest value at index 0

			Dictionary<Slugcat, float> scugs = [];
			float weight;
			bool saturated = false;
			vals.Capacity = numToStore;

			foreach (int[] i in chunks)
			{
				scugs = [];
				// Run a for loop 
				foreach (int j in i)
				{
					scugs.Add(new(j), 0f);
				}

				#region Iterating through everything

				if (sParams.Sympathy != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Sympathy.Value.weight * Math.Abs(sc.Personality.Sympathy - sParams.Sympathy.Value.target);
					}
				}
				if (sParams.Energy != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Energy.Value.weight * Math.Abs(sc.Personality.Energy - sParams.Energy.Value.target);
					}
				}
				if (sParams.Bravery != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Bravery.Value.weight * Math.Abs(sc.Personality.Bravery - sParams.Bravery.Value.target);
					}
				}
				if (sParams.Nervous != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Nervous.Value.weight * Math.Abs(sc.Personality.Nervous - sParams.Nervous.Value.target);
					}
				}
				if (sParams.Aggression != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Aggression.Value.weight * Math.Abs(sc.Personality.Aggression - sParams.Aggression.Value.target);
					}
				}
				if (sParams.Dominance != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Dominance.Value.weight * Math.Abs(sc.Personality.Dominance - sParams.Dominance.Value.target);
					}
				}

				if (sParams.Met != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Met.Value.weight * Math.Abs(sc.NPCStats.Met - sParams.Met.Value.target);
					}
				}
				if (sParams.Bal != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Bal.Value.weight * Math.Abs(sc.NPCStats.Bal - sParams.Bal.Value.target);
					}
				}
				if (sParams.Size != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Size.Value.weight * Math.Abs(sc.NPCStats.Size - sParams.Size.Value.target);
					}
				}
				if (sParams.Stealth != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Stealth.Value.weight * Math.Abs(sc.NPCStats.Stealth - sParams.Stealth.Value.target);
					}
				}
				if (sParams.Dark != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Dark.Value.weight * (sc.NPCStats.Dark == sParams.Dark.Value.target ? 1 : 0);
					}
				}
				if (sParams.EyeColor != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.EyeColor.Value.weight * Math.Abs(sc.NPCStats.EyeColor - sParams.EyeColor.Value.target);
					}
				}
				if (sParams.H != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.H.Value.weight * Math.Abs(sc.NPCStats.H - sParams.H.Value.target);
					}
				}
				if (sParams.S != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.S.Value.weight * Math.Abs(sc.NPCStats.S - sParams.S.Value.target);
					}
				}
				if (sParams.L != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.L.Value.weight * Math.Abs(sc.NPCStats.L - sParams.L.Value.target);
					}
				}
				if (sParams.Wideness != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Wideness.Value.weight * Math.Abs(sc.NPCStats.Wideness - sParams.Wideness.Value.target);
					}
				}

				if (sParams.BodyWeightFac != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.BodyWeightFac.Value.weight * Math.Abs(sc.SlugcatStats.bodyWeightFac - sParams.BodyWeightFac.Value.target);
					}
				}
				if (sParams.GeneralVisibilityBonus != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.GeneralVisibilityBonus.Value.weight * Math.Abs(sc.SlugcatStats.generalVisibilityBonus - sParams.GeneralVisibilityBonus.Value.target);
					}
				}
				if (sParams.VisualStealthInSneakMode != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.VisualStealthInSneakMode.Value.weight * Math.Abs(sc.SlugcatStats.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.Value.target);
					}
				}
				if (sParams.LoudnessFac != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.LoudnessFac.Value.weight * Math.Abs(sc.SlugcatStats.loudnessFac - sParams.LoudnessFac.Value.target);
					}
				}
				if (sParams.LungsFac != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.LungsFac.Value.weight * Math.Abs(sc.SlugcatStats.lungsFac - sParams.LungsFac.Value.target);
					}
				}
				if (sParams.ThrowingSkill != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.ThrowingSkill.Value.weight * Math.Abs(sc.SlugcatStats.throwingSkill - sParams.ThrowingSkill.Value.target);
					}
				}
				if (sParams.PoleClimbSpeedFac != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.PoleClimbSpeedFac.Value.weight * Math.Abs(sc.SlugcatStats.poleClimbSpeedFac - sParams.PoleClimbSpeedFac.Value.target);
					}
				}
				if (sParams.CorridorClimbSpeedFac != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.CorridorClimbSpeedFac.Value.weight * Math.Abs(sc.SlugcatStats.corridorClimbSpeedFac - sParams.CorridorClimbSpeedFac.Value.target);
					}
				}
				if (sParams.RunSpeedFac != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.RunSpeedFac.Value.weight * Math.Abs(sc.SlugcatStats.runSpeedFac - sParams.RunSpeedFac.Value.target);
					}
				}

				if (sParams.DangleFruit != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.DangleFruit.Value.weight * Math.Abs(sc.FoodPreferences.DangleFruit - sParams.DangleFruit.Value.target);
					}
				}
				if (sParams.WaterNut != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.WaterNut.Value.weight * Math.Abs(sc.FoodPreferences.WaterNut - sParams.WaterNut.Value.target);
					}
				}
				if (sParams.JellyFish != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.JellyFish.Value.weight * Math.Abs(sc.FoodPreferences.JellyFish - sParams.JellyFish.Value.target);
					}
				}
				if (sParams.SlimeMold != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.SlimeMold.Value.weight * Math.Abs(sc.FoodPreferences.SlimeMold - sParams.SlimeMold.Value.target);
					}
				}
				if (sParams.EggBugEgg != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.EggBugEgg.Value.weight * Math.Abs(sc.FoodPreferences.EggBugEgg - sParams.EggBugEgg.Value.target);
					}
				}
				if (sParams.FireEgg != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.FireEgg.Value.weight * Math.Abs(sc.FoodPreferences.FireEgg - sParams.FireEgg.Value.target);
					}
				}
				if (sParams.Popcorn != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Popcorn.Value.weight * Math.Abs(sc.FoodPreferences.Popcorn - sParams.Popcorn.Value.target);
					}
				}
				if (sParams.GooieDuck != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.GooieDuck.Value.weight * Math.Abs(sc.FoodPreferences.GooieDuck - sParams.GooieDuck.Value.target);
					}
				}
				if (sParams.LilyPuck != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.LilyPuck.Value.weight * Math.Abs(sc.FoodPreferences.LilyPuck - sParams.LilyPuck.Value.target);
					}
				}
				if (sParams.GlowWeed != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.GlowWeed.Value.weight * Math.Abs(sc.FoodPreferences.GlowWeed - sParams.GlowWeed.Value.target);
					}
				}
				if (sParams.DandelionPeach != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.DandelionPeach.Value.weight * Math.Abs(sc.FoodPreferences.DandelionPeach - sParams.DandelionPeach.Value.target);
					}
				}
				if (sParams.Neuron != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Neuron.Value.weight * Math.Abs(sc.FoodPreferences.Neuron - sParams.Neuron.Value.target);
					}
				}
				if (sParams.Centipede != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Centipede.Value.weight * Math.Abs(sc.FoodPreferences.Centipede - sParams.Centipede.Value.target);
					}
				}
				if (sParams.SmallCentipede != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.SmallCentipede.Value.weight * Math.Abs(sc.FoodPreferences.SmallCentipede - sParams.SmallCentipede.Value.target);
					}
				}
				if (sParams.VultureGrub != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.VultureGrub.Value.weight * Math.Abs(sc.FoodPreferences.VultureGrub - sParams.VultureGrub.Value.target);
					}
				}
				if (sParams.SmallNeedleWorm != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.SmallNeedleWorm.Value.weight * Math.Abs(sc.FoodPreferences.SmallNeedleWorm - sParams.SmallNeedleWorm.Value.target);
					}
				}
				if (sParams.Hazer != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.Hazer.Value.weight * Math.Abs(sc.FoodPreferences.Hazer - sParams.Hazer.Value.target);
					}
				}
				if (sParams.NotCounted != null)
				{
					foreach (Slugcat sc in scugs.Keys)
					{
						scugs[sc] += sParams.NotCounted.Value.weight * Math.Abs(sc.FoodPreferences.NotCounted - sParams.NotCounted.Value.target);
					}
				}

				#endregion




			}

			if (!saturated && vals.Count < numToStore)
			{
				//vals.Add(weight, sc);
				if (vals.Count == vals.Capacity) saturated = true;
			}
			else if (vals.GetKeyAtIndex(vals.Capacity - 1) > weight)
			{
				vals.RemoveAt(vals.Capacity - 1);
				//vals.Add(weight, sc);
			}



			
			return vals;
		}
		#endregion
	}
}
