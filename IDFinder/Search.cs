using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IDFinder
{
	public static class Searcher
	{
		private static readonly Comparer<KeyValuePair<float, int>> comparer = Comparer<KeyValuePair<float, int>>.Create((x, y) => x.Key.CompareTo(y.Key));

		#region Weights
		private static float PersonalityWeight(in Personality p, SearchParams sParams)
		{
			float weight = 0f;
			weight += sParams.Sympathy.weight * float.Abs(p.Sympathy - sParams.Sympathy.target);
			weight += sParams.Energy.weight * float.Abs(p.Energy - sParams.Energy.target);
			weight += sParams.Bravery.weight * float.Abs(p.Bravery - sParams.Bravery.target);
			weight += sParams.Nervous.weight * float.Abs(p.Nervous - sParams.Nervous.target);
			weight += sParams.Aggression.weight * float.Abs(p.Aggression - sParams.Aggression.target);
			weight += sParams.Dominance.weight * float.Abs(p.Dominance - sParams.Dominance.target);
			return weight;
		}
		private static float NPCStatsWeight(in NPCStats npc, SearchParams sParams)
		{
			float weight = 0f;
			weight += sParams.Met.weight * float.Abs(npc.Met - sParams.Met.target);
			weight += sParams.Bal.weight * float.Abs(npc.Bal - sParams.Bal.target);
			weight += sParams.Size.weight * float.Abs(npc.Size - sParams.Size.target);
			weight += sParams.Stealth.weight * float.Abs(npc.Stealth - sParams.Stealth.target);
			weight += sParams.Dark.weight * (npc.Dark == sParams.Dark.target ? 0 : 1);
			weight += sParams.EyeColor.weight * float.Abs(npc.EyeColor - sParams.EyeColor.target);
			weight += sParams.H.weight * Custom.HueDiff(npc.H, sParams.H.target);
			weight += sParams.S.weight * float.Abs(npc.S - sParams.S.target);
			weight += sParams.L.weight * float.Abs(npc.L - sParams.L.target);
			weight += sParams.Wideness.weight * float.Abs(npc.Wideness - sParams.Wideness.target);
			return weight;
		}
		private static float SlugcatStatsWeight(in SlugcatStats slug, SearchParams sParams)
		{
			float weight = 0f;
			weight += sParams.BodyWeightFac.weight * float.Abs(slug.bodyWeightFac - sParams.BodyWeightFac.target);
			weight += sParams.GeneralVisibilityBonus.weight * float.Abs(slug.generalVisibilityBonus - sParams.GeneralVisibilityBonus.target);
			weight += sParams.VisualStealthInSneakMode.weight * float.Abs(slug.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.target);
			weight += sParams.LoudnessFac.weight * float.Abs(slug.loudnessFac - sParams.LoudnessFac.target);
			weight += sParams.LungsFac.weight * float.Abs(slug.lungsFac - sParams.LungsFac.target);
			weight += sParams.ThrowingSkill.weight * float.Abs(slug.throwingSkill - sParams.ThrowingSkill.target);
			weight += sParams.PoleClimbSpeedFac.weight * float.Abs(slug.poleClimbSpeedFac - sParams.PoleClimbSpeedFac.target);
			weight += sParams.CorridorClimbSpeedFac.weight * float.Abs(slug.corridorClimbSpeedFac - sParams.CorridorClimbSpeedFac.target);
			weight += sParams.RunSpeedFac.weight * float.Abs(slug.runSpeedFac - sParams.RunSpeedFac.target);
			return weight;
		}
		private static float FoodPreferencesWeight(in FoodPreferences foodPref, SearchParams sParams)
		{
			float weight = 0f;
			weight += sParams.DangleFruit.weight * float.Abs(foodPref.DangleFruit - sParams.DangleFruit.target);
			weight += sParams.WaterNut.weight * float.Abs(foodPref.WaterNut - sParams.WaterNut.target);
			weight += sParams.JellyFish.weight * float.Abs(foodPref.JellyFish - sParams.JellyFish.target);
			weight += sParams.SlimeMold.weight * float.Abs(foodPref.SlimeMold - sParams.SlimeMold.target);
			weight += sParams.EggBugEgg.weight * float.Abs(foodPref.EggBugEgg - sParams.EggBugEgg.target);
			weight += sParams.FireEgg.weight * float.Abs(foodPref.FireEgg - sParams.FireEgg.target);
			weight += sParams.Popcorn.weight * float.Abs(foodPref.Popcorn - sParams.Popcorn.target);
			weight += sParams.GooieDuck.weight * float.Abs(foodPref.GooieDuck - sParams.GooieDuck.target);
			weight += sParams.LilyPuck.weight * float.Abs(foodPref.LilyPuck - sParams.LilyPuck.target);
			weight += sParams.GlowWeed.weight * float.Abs(foodPref.GlowWeed - sParams.GlowWeed.target);
			weight += sParams.DandelionPeach.weight * float.Abs(foodPref.DandelionPeach - sParams.DandelionPeach.target);
			weight += sParams.Neuron.weight * float.Abs(foodPref.Neuron - sParams.Neuron.target);
			weight += sParams.Centipede.weight * float.Abs(foodPref.Centipede - sParams.Centipede.target);
			weight += sParams.SmallCentipede.weight * float.Abs(foodPref.SmallCentipede - sParams.SmallCentipede.target);
			weight += sParams.VultureGrub.weight * float.Abs(foodPref.VultureGrub - sParams.VultureGrub.target);
			weight += sParams.SmallNeedleWorm.weight * float.Abs(foodPref.SmallNeedleWorm - sParams.SmallNeedleWorm.target);
			weight += sParams.Hazer.weight * float.Abs(foodPref.Hazer - sParams.Hazer.target);
			weight += sParams.NotCounted.weight * float.Abs(foodPref.NotCounted - sParams.NotCounted.target);
			return weight;
		}
		private static float IndividualVariationsWeight(in IndividualVariations iVars, SearchParams sParams)
		{
			float weight = 0f;
			weight += sParams.WaistWidth.weight * float.Abs(sParams.WaistWidth.target - iVars.WaistWidth);
			weight += sParams.HeadSize.weight * float.Abs(sParams.HeadSize.target - iVars.HeadSize);
			weight += sParams.EartlerWidth.weight * float.Abs(sParams.EartlerWidth.target - iVars.EartlerWidth);
			weight += sParams.NeckThickness.weight * float.Abs(sParams.NeckThickness.target - iVars.NeckThickness);
			weight += sParams.HandsHeadColor.weight * float.Abs(sParams.HandsHeadColor.target - iVars.HandsHeadColor);
			weight += sParams.EyeSize.weight * float.Abs(sParams.EyeSize.target - iVars.EyeSize);
			weight += sParams.NarrowEyes.weight * float.Abs(sParams.NarrowEyes.target - iVars.NarrowEyes);
			weight += sParams.EyesAngle.weight * float.Abs(sParams.EyesAngle.target - iVars.EyesAngle);
			weight += sParams.Fatness.weight * float.Abs(sParams.Fatness.target - iVars.Fatness);
			weight += sParams.NarrowWaist.weight * float.Abs(sParams.NarrowWaist.target - iVars.NarrowWaist);
			weight += sParams.LegsSize.weight * float.Abs(sParams.LegsSize.target - iVars.LegsSize);
			weight += sParams.ArmThickness.weight * float.Abs(sParams.ArmThickness.target - iVars.ArmThickness);
			weight += sParams.WideTeeth.weight * float.Abs(sParams.WideTeeth.target - iVars.WideTeeth);
			weight += sParams.PupilSize.weight * float.Abs(sParams.PupilSize.target - iVars.PupilSize);
			weight += sParams.Scruffy.weight * float.Abs(sParams.Scruffy.target - iVars.Scruffy);
			weight += sParams.ColoredEartlerTips.weight * (sParams.ColoredEartlerTips.target == iVars.ColoredEartlerTips ? 0f : 1f);
			weight += sParams.DeepPupils.weight * (sParams.DeepPupils.target == iVars.DeepPupils ? 0f : 1f);
			weight += sParams.ColoredPupils.weight * float.Abs(sParams.ColoredPupils.target - iVars.ColoredPupils);
			weight += sParams.TailSegs.weight * float.Abs(sParams.TailSegs.target - iVars.TailSegs);
			weight += sParams.GeneralMelanin.weight * float.Abs(sParams.GeneralMelanin.target - iVars.GeneralMelanin);
			return weight;
		}
		private static float EartlersWeight() => throw new NotImplementedException();
		private static float ScavColorsWeight(in ScavColors colors, SearchParams sParams)
		{
			// Logic error: hues don't wrap around.
			float weight = 0f;
			weight += sParams.BellyColorH.weight * Custom.HueDiff(sParams.BellyColorH.target, colors.BellyColor.H);
			weight += sParams.BellyColorS.weight * float.Abs(sParams.BellyColorS.target - colors.BellyColor.S);
			weight += sParams.BellyColorL.weight * float.Abs(sParams.BellyColorL.target - colors.BellyColor.L);
			weight += sParams.BodyColorH.weight * Custom.HueDiff(sParams.BodyColorH.target, colors.BodyColor.H);
			weight += sParams.BodyColorS.weight * float.Abs(sParams.BodyColorS.target - colors.BodyColor.S);
			weight += sParams.BodyColorL.weight * float.Abs(sParams.BodyColorL.target - colors.BodyColor.L);
			weight += sParams.DecorationColorH.weight * Custom.HueDiff(sParams.DecorationColorH.target, colors.DecorationColor.H);
			weight += sParams.DecorationColorS.weight * float.Abs(sParams.DecorationColorS.target - colors.DecorationColor.S);
			weight += sParams.DecorationColorL.weight * float.Abs(sParams.DecorationColorL.target - colors.DecorationColor.L);
			weight += sParams.EyeColorH.weight * Custom.HueDiff(sParams.EyeColorH.target, colors.EyeColor.H);
			weight += sParams.EyeColorL.weight * float.Abs(sParams.EyeColorL.target - colors.EyeColor.L);
			weight += sParams.HeadColorH.weight * float.Abs(sParams.HeadColorH.target - colors.HeadColor.H);
			weight += sParams.HeadColorS.weight * float.Abs(sParams.HeadColorS.target - colors.HeadColor.S);
			weight += sParams.HeadColorL.weight * float.Abs(sParams.HeadColorL.target - colors.HeadColor.L);
			weight += sParams.BellyColorBlack.weight * float.Abs(sParams.BellyColorBlack.target - colors.BellyColorBlack);
			weight += sParams.BodyColorBlack.weight * float.Abs(sParams.BodyColorBlack.target - colors.BodyColorBlack);
			weight += sParams.HeadColorBlack.weight * float.Abs(sParams.HeadColorBlack.target - colors.HeadColorBlack);
			return weight;
		}
		private static float ScavSkillsWeight(in ScavSkills skills, SearchParams sParams)
		{
			float weight = 0f;
			weight += sParams.BlockingSkill.weight * float.Abs(sParams.BlockingSkill.target - skills.BlockingSkill);
			weight += sParams.DodgeSkill.weight * float.Abs(sParams.DodgeSkill.target - skills.DodgeSkill);
			weight += sParams.MeleeSkill.weight * float.Abs(sParams.MeleeSkill.target - skills.MeleeSkill);
			weight += sParams.MidRangeSkill.weight * float.Abs(sParams.MidRangeSkill.target - skills.MidRangeSkill);
			weight += sParams.ReactionSkill.weight * float.Abs(sParams.ReactionSkill.target - skills.ReactionSkill);
			return weight;
		}
		private static float ScavBackPatternsWeight(BackTuftsAndRidges back, IScavBackPatternsParams sParams)
		{
			float weight = 0f;
			weight += sParams.Top.weight * float.Abs(sParams.Top.target - back.Top);
			weight += sParams.Bottom.weight * float.Abs(sParams.Bottom.target - back.Bottom);
			weight += sParams.Pattern.weight * float.Abs(sParams.Pattern.target - back.Pattern);
			weight += sParams.Type.weight * (sParams.Type.target == back.Type ? 0f : 1f);
			weight += sParams.ColorType.weight * float.Abs(sParams.ColorType.target - back.ColorType);
			weight += sParams.IsColored.weight * (sParams.IsColored.target == back.IsColored ? 0f : 1f);
			weight += sParams.ScaleGraf.weight * float.Abs(sParams.ScaleGraf.target - back.ScaleGraf);
			weight += sParams.GeneralSize.weight * float.Abs(sParams.GeneralSize.target - back.GeneralSize);
			weight += sParams.Colored.weight * float.Abs(sParams.Colored.target - back.Colored);
			weight += sParams.NumberOfSpines.weight * float.Abs(sParams.NumberOfSpines.target - back.NumberOfSpines);
			return weight;
		}
		#endregion

		public static List<KeyValuePair<float, int>> SearchThreaded(int start, int stop, int numToStore, int threads, SearchParams SearchParams, bool trimToNumToStore = true, EventHandler<SearchProgressEventArgs>? progressEventHandler = null, EventHandler<SearchWorkingValsEventArgs>? workingValsEventHandler = null, int progressInterval = 1000, int workingValsInterval = 10000)
		{
			if (threads < 1)
				throw new ArgumentException("Cannot use less than one thread!");
			if (threads == 1)
				return Search(start, stop, numToStore, SearchParams);

			int[][] chunks = Chunker(start, stop, threads);
			ConcurrentBag<List<KeyValuePair<float, int>>> resultBag = new();

			// Shallow copying SearchParams in the expectation that a bottleneck could arise when multiple threads access the same instance. I don't think this is true given it'll only be reading, but I don't want to touch and break anything

			// Could do with adding private event handlers to get the event data for each search, then average it and emit it as a single invocation of the event handler args given to SearchThreaded

			if (progressEventHandler is not null)
			{

			}

			if (workingValsEventHandler is not null)
			{

			}

			Parallel.For(0, threads, i =>
			{
				var res = Search(chunks[i][0], chunks[i][1], numToStore, SearchParams.Clone(), progressEventHandler, workingValsEventHandler, progressInterval, workingValsInterval);
				resultBag.Add(res);
			});

			List<KeyValuePair<float, int>> resultsSorted = new(threads * numToStore);
			foreach (var result in resultBag)
				resultsSorted.AddRange(result);

			resultsSorted = resultsSorted.OrderBy(kvp => kvp.Key).ThenBy(kvp => kvp.Value).ToList();

			if (trimToNumToStore)
				return resultsSorted.GetRange(0, numToStore);

			return resultsSorted;
		}

		/// <summary>Complete an ID search in the given interval with the given search parameters.</summary>
		/// <param name="start">Start ID</param>
		/// <param name="stop">End ID (inclusive)</param>
		/// <param name="numToStore">Number of IDs to find and return</param>
		/// <param name="SearchParams">Search parameters used to weigh each ID</param>
		/// <param name="progressEventHandler">Event handler to update subscribers about the percentage completion of the search</param>
		/// <param name="workingValsEventHandler">Event handler to update subscribers with periodic working values</param>
		/// <param name="progressInterval">Interval to invoke the progress event handler, in milliseconds</param>
		/// <param name="workingValsInterval">Interval to invoke the working values event handler, in milliseconds</param>
		/// <returns>A sorted list of weight-ID key value pairs, from smallest to larger weights</returns>
		/// <remarks>A small workingValsInterval will cause slowdown due to excessive list copying with high numToStore. For this reason it is recommended only to be used with long-running searches or small numToStore.</remarks>
		public static List<KeyValuePair<float, int>> Search(int start, int stop, int numToStore, SearchParams SearchParams, EventHandler<SearchProgressEventArgs>? progressEventHandler = null, EventHandler<SearchWorkingValsEventArgs>? workingValsEventHandler = null, int progressInterval = 1000, int workingValsInterval = 10000)
		{
			XORShift128 rng = new();

			bool boolPersonality = !((IPersonalityParams)SearchParams).AllWeightless();
			bool boolNpcStats = !((INPCStatsParams)SearchParams).AllWeightless();
			bool boolSlugcatStats = !((ISlugcatStatsParams)SearchParams).AllWeightless();
			bool boolFoodPreferences = !((IFoodPreferencesParams)SearchParams).AllWeightless();
			bool boolScavVariations = !((IIndividualVariationsParams)SearchParams).AllWeightless();
			bool boolScavColors = !((IScavColorsParams)SearchParams).AllWeightless();
			bool boolScavSkills = !((IScavSkillsParams)SearchParams).AllWeightless();
			bool boolScavBack = !((IScavBackPatternsParams)SearchParams).AllWeightless();
			bool isElite = SearchParams.Elite;

			List<KeyValuePair<float, int>> vals = new(numToStore + 1);
			float weight;
			float prevLargestWeight = float.MaxValue;

			Personality personality = default;
			NPCStats npcStats = default;
			SlugcatStats slugStats = default;
			FoodPreferences foodPref = default;
			ScavSkills scavSkills = default;

			int i = start;  // Assigned by the for loop, but also used in the progress callback
			TimerCallback progressCallback = new(_obj =>
			{
				float percent = Custom.InverseLerp(start, stop, i) * 100f;
				progressEventHandler?.Invoke(null, new(percent));
			});
			Timer? progressTimer = progressEventHandler is null ? null : new(progressCallback, null, int.Max(progressInterval, 100), progressInterval); // Once initialised it starts firing. Minimum of 100ms to ensure the main loop has since began and given meaningful change

			TimerCallback workingValsCallback = new(_listVals =>
			{
				lock (_listVals!)
				{
					workingValsEventHandler?.Invoke(null, new((_listVals as List<KeyValuePair<float, int>>)!)); // If this is null, you have bigger problems to deal with
				}
			});
			Timer? workingValsTimer = workingValsEventHandler is null ? null : new(workingValsCallback, vals, int.Max(workingValsInterval, 100), workingValsInterval);

			int largerThanIndex;
			KeyValuePair<float, int> kvp;
			for (long l = start; l <= stop; l++)
			{
				i = (int)l;	// Protect against overflows

				#region scugs
				weight = 0f;
				if (boolPersonality)
				{
					personality = new(i, rng);
					// Weight checks after each generation lets us exit an iteration early if it's out of range. Will incur performance loss for single trait object searches, but should improve performance otherwise
					if ((weight += PersonalityWeight(personality, SearchParams)) > prevLargestWeight)
						goto ExitWeight;
				}
				if (boolNpcStats)
				{
					npcStats = new(i, rng);
					if ((weight += NPCStatsWeight(npcStats, SearchParams)) > prevLargestWeight)
						goto ExitWeight;
				}
				if (boolSlugcatStats)
				{
					if (!boolNpcStats)
						npcStats = new(i, rng);

					slugStats = new(npcStats);	// SlugcatStats makes no use of rng in its constructor
					if ((weight += SlugcatStatsWeight(slugStats, SearchParams)) > prevLargestWeight)
						goto ExitWeight;
				}
				if (boolFoodPreferences)
				{
					if (!boolPersonality)
						personality = new(i, rng);

					foodPref = new(i, personality, rng);
					if ((weight += FoodPreferencesWeight(foodPref, SearchParams)) > prevLargestWeight)
						goto ExitWeight;
				}
				#endregion
				#region scavs
				if (boolScavSkills) 
				{
					if (!boolPersonality) 
						personality = new(i, rng);

					scavSkills = new(i, personality, rng, isElite);
					if ((weight += ScavSkillsWeight(scavSkills, SearchParams)) > prevLargestWeight)
						goto ExitWeight;
				}
				if (boolScavVariations || boolScavBack || boolScavColors)
				{
					if (!(boolPersonality || boolScavSkills))
						personality = new(i, rng);

					if (!(boolScavBack || boolScavColors))
					{
						if ((weight += IndividualVariationsWeight(new IndividualVariations(personality, rng, isElite), SearchParams)) > prevLargestWeight)
							goto ExitWeight;
					}
					else
					{
						(IndividualVariations? variations, ScavColors? color, BackTuftsAndRidges? back) = Scavenger.GetGraphicsRNGParam(i, rng, isElite, boolScavVariations, boolScavColors, boolScavBack, (boolPersonality || boolScavSkills || boolScavVariations) ? personality : null);

						if (boolScavVariations)
						{
							if ((weight += IndividualVariationsWeight((IndividualVariations)variations!, SearchParams)) > prevLargestWeight)
								goto ExitWeight;
						}
						if (boolScavColors)
						{
							if ((weight += ScavColorsWeight((ScavColors)color!, SearchParams)) > prevLargestWeight)
								goto ExitWeight;
						}
						if (boolScavBack)
						{
							if ((weight += ScavBackPatternsWeight(back!, SearchParams)) > prevLargestWeight)
								goto ExitWeight;
						}
					}
				}
				#endregion

				ExitWeight:
				if (vals.Count < numToStore)
				{
					vals.Add(new(weight, i));
					if (vals.Count == numToStore)
					{
						vals.Sort(comparer);
					}
					// Ignore prevLargestWeight for now, since this is just gonna be filling it up regardless of how it ranks
				}
				else if (vals[^1].Key > weight)
				{
					// vals is certainly ordered at this point, as vals is sorted when vals.Count == numToStore. Therefore we can do a binary search ( log(n) ) to find the index of the next largest or equal weight, insert there ( O(n) ), and remove the final element ( O(1) )
					kvp = new(weight, i);
					largerThanIndex = vals.BinarySearch(kvp, comparer);
					if (largerThanIndex < 0)
						largerThanIndex = ~largerThanIndex; // bitwise complement of a BinarySearch that does not find the value returns the index of the next largest value in the list.

					vals.Insert(largerThanIndex, kvp);
					vals.RemoveAt(numToStore);
					prevLargestWeight = vals[^1].Key;
				}
			}

			progressTimer?.Dispose();
			workingValsTimer?.Dispose();

			return vals;
		}

		/// <summary>Helper method to split a range into a set of ranges for multithreaded workloads</summary>
		/// <param name="start"></param>
		/// <param name="stop"></param>
		/// <param name="threads"></param>
		/// <returns></returns>
		private static int[][] Chunker(int start, int stop, int threads)
		{
			int[][] chunks = new int[threads][];
			long chunkSize = ((long)stop - start) / threads;
			for (int i = 0; i < threads; i++)
			{
				chunks[i] = new int[2];
				chunks[i][0] = start + i * (int)chunkSize + 1;
				chunks[i][1] = start + (i + 1) * (int)chunkSize;
			}
			chunks[0][0] = start;
			chunks[threads - 1][1] = stop;

			return chunks;
		}
	}
}
