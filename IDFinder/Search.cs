using System.Collections.Concurrent;

namespace IDFinder
{
	// Have an ISearcher interface, each creature has its own class that implements it. This'll avoid having a disgusting number of parameters, and SearchParams can be a nested class
	public static class Searcher
	{
		private static readonly IComparer<KeyValuePair<float, int>> comparer = Comparer<KeyValuePair<float, int>>.Create((x, y) => x.Key.CompareTo(y.Key));
		private static bool abortSearch = false;   // I'd rather this be a private field and have an Abort() method invoked that sets it to true, awaits a Task.Delay, and sets it back to false afterwards. I feel there's too much risk to forget to reset it, or for race condition shenanigans. 
		private static float[] _completions = new float[0];
		public static float CompletionPercent
		{
			get => (float)Math.Round(_completions.Average(), 2, MidpointRounding.AwayFromZero);
		}
		private static void Init(int threads)
		{
			abortSearch = false;
			_completions = new float[threads];
		}
		public async static void Abort()
		{
			abortSearch = true;
			await Task.Delay(200); 
			// Should be long enough to ensure that all searches terminate by the time this method returns
			// All searches call Init so no need to set abortSearch to false. 
		}
		#region Weights
		private static float PersonalityWeight(Personality p, IPersonalityParams sParams)
		{
			float weight = 0f;
			if (sParams.Sympathy.weight != 0f)
				weight += sParams.Sympathy.weight * Math.Abs(p.Sympathy - sParams.Sympathy.target);
			if (sParams.Energy.weight != 0f)
				weight += sParams.Energy.weight * Math.Abs(p.Energy - sParams.Energy.target);
			if (sParams.Bravery.weight != 0f)
				weight += sParams.Bravery.weight * Math.Abs(p.Bravery - sParams.Bravery.target);
			if (sParams.Nervous.weight != 0f)
				weight += sParams.Nervous.weight * Math.Abs(p.Nervous - sParams.Nervous.target);
			if (sParams.Aggression.weight != 0f)
				weight += sParams.Aggression.weight * Math.Abs(p.Aggression - sParams.Aggression.target);
			if (sParams.Dominance.weight != 0f)
				weight += sParams.Dominance.weight * Math.Abs(p.Dominance - sParams.Dominance.target);
			return weight;
		}
		private static float NPCStatsWeight(NPCStats npc, INPCStatsParams sParams)
		{
			float weight = 0f;
			if (sParams.Met.weight != 0f)
				weight += sParams.Met.weight * Math.Abs(npc.Met - sParams.Met.target);
			if (sParams.Bal.weight != 0f)
				weight += sParams.Bal.weight * Math.Abs(npc.Bal - sParams.Bal.target);
			if (sParams.Size.weight != 0f)
				weight += sParams.Size.weight * Math.Abs(npc.Size - sParams.Size.target);
			if (sParams.Stealth.weight != 0f)
				weight += sParams.Stealth.weight * Math.Abs(npc.Stealth - sParams.Stealth.target);
			if (sParams.Dark.weight != 0f)
				weight += sParams.Dark.weight * (npc.Dark == sParams.Dark.target ? 0 : 1);
			if (sParams.EyeColor.weight != 0f)
				weight += sParams.EyeColor.weight * Math.Abs(npc.EyeColor - sParams.EyeColor.target);
			if (sParams.H.weight != 0f)
				weight += sParams.H.weight * Math.Abs(npc.H - sParams.H.target);
			if (sParams.S.weight != 0f)
				weight += sParams.S.weight * Math.Abs(npc.S - sParams.S.target);
			if (sParams.L.weight != 0f)
				weight += sParams.L.weight * Math.Abs(npc.L - sParams.L.target);
			if (sParams.Wideness.weight != 0f)
				weight += sParams.Wideness.weight * Math.Abs(npc.Wideness - sParams.Wideness.target);
			return weight;
		}
		private static float SlugcatStatsWeight(SlugcatStats slug, ISlugcatStatsParams sParams)
		{
			float weight = 0f;
			if (sParams.BodyWeightFac.weight != 0f)
				weight += sParams.BodyWeightFac.weight * Math.Abs(slug.bodyWeightFac - sParams.BodyWeightFac.target);
			if (sParams.GeneralVisibilityBonus.weight != 0f)
				weight += sParams.GeneralVisibilityBonus.weight * Math.Abs(slug.generalVisibilityBonus - sParams.GeneralVisibilityBonus.target);
			if (sParams.VisualStealthInSneakMode.weight != 0f)
				weight += sParams.VisualStealthInSneakMode.weight * Math.Abs(slug.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.target);
			if (sParams.LoudnessFac.weight != 0f)
				weight += sParams.LoudnessFac.weight * Math.Abs(slug.loudnessFac - sParams.LoudnessFac.target);
			if (sParams.LungsFac.weight != 0f)
				weight += sParams.LungsFac.weight * Math.Abs(slug.lungsFac - sParams.LungsFac.target);
			if (sParams.ThrowingSkill.weight != 0f)
				weight += sParams.ThrowingSkill.weight * Math.Abs(slug.throwingSkill - sParams.ThrowingSkill.target);
			if (sParams.PoleClimbSpeedFac.weight != 0f)
				weight += sParams.PoleClimbSpeedFac.weight * Math.Abs(slug.poleClimbSpeedFac - sParams.PoleClimbSpeedFac.target);
			if (sParams.CorridorClimbSpeedFac.weight != 0f)
				weight += sParams.CorridorClimbSpeedFac.weight * Math.Abs(slug.corridorClimbSpeedFac - sParams.CorridorClimbSpeedFac.target);
			if (sParams.RunSpeedFac.weight != 0f)
				weight += sParams.RunSpeedFac.weight * Math.Abs(slug.runSpeedFac - sParams.RunSpeedFac.target);
			return weight;
		}
		private static float FoodPreferencesWeight(FoodPreferences foodPref, IFoodPreferencesParams sParams)
		{
			float weight = 0f;
			if (sParams.DangleFruit.weight != 0f)
				weight += sParams.DangleFruit.weight * Math.Abs(foodPref.DangleFruit - sParams.DangleFruit.target);
			if (sParams.WaterNut.weight != 0f)
				weight += sParams.WaterNut.weight * Math.Abs(foodPref.WaterNut - sParams.WaterNut.target);
			if (sParams.JellyFish.weight != 0f)
				weight += sParams.JellyFish.weight * Math.Abs(foodPref.JellyFish - sParams.JellyFish.target);
			if (sParams.SlimeMold.weight != 0f)
				weight += sParams.SlimeMold.weight * Math.Abs(foodPref.SlimeMold - sParams.SlimeMold.target);
			if (sParams.EggBugEgg.weight != 0f)
				weight += sParams.EggBugEgg.weight * Math.Abs(foodPref.EggBugEgg - sParams.EggBugEgg.target);
			if (sParams.FireEgg.weight != 0f)
				weight += sParams.FireEgg.weight * Math.Abs(foodPref.FireEgg - sParams.FireEgg.target);
			if (sParams.Popcorn.weight != 0f)
				weight += sParams.Popcorn.weight * Math.Abs(foodPref.Popcorn - sParams.Popcorn.target);
			if (sParams.GooieDuck.weight != 0f)
				weight += sParams.GooieDuck.weight * Math.Abs(foodPref.GooieDuck - sParams.GooieDuck.target);
			if (sParams.LilyPuck.weight != 0f)
				weight += sParams.LilyPuck.weight * Math.Abs(foodPref.LilyPuck - sParams.LilyPuck.target);
			if (sParams.GlowWeed.weight != 0f)
				weight += sParams.GlowWeed.weight * Math.Abs(foodPref.GlowWeed - sParams.GlowWeed.target);
			if (sParams.DandelionPeach.weight != 0f)
				weight += sParams.DandelionPeach.weight * Math.Abs(foodPref.DandelionPeach - sParams.DandelionPeach.target);
			if (sParams.Neuron.weight != 0f)
				weight += sParams.Neuron.weight * Math.Abs(foodPref.Neuron - sParams.Neuron.target);
			if (sParams.Centipede.weight != 0f)
				weight += sParams.Centipede.weight * Math.Abs(foodPref.Centipede - sParams.Centipede.target);
			if (sParams.SmallCentipede.weight != 0f)
				weight += sParams.SmallCentipede.weight * Math.Abs(foodPref.SmallCentipede - sParams.SmallCentipede.target);
			if (sParams.VultureGrub.weight != 0f)
				weight += sParams.VultureGrub.weight * Math.Abs(foodPref.VultureGrub - sParams.VultureGrub.target);
			if (sParams.SmallNeedleWorm.weight != 0f)
				weight += sParams.SmallNeedleWorm.weight * Math.Abs(foodPref.SmallNeedleWorm - sParams.SmallNeedleWorm.target);
			if (sParams.Hazer.weight != 0f)
				weight += sParams.Hazer.weight * Math.Abs(foodPref.Hazer - sParams.Hazer.target);
			if (sParams.NotCounted.weight != 0f)
				weight += sParams.NotCounted.weight * Math.Abs(foodPref.NotCounted - sParams.NotCounted.target);
			return weight;
		}
		private static float IndividualVariationsWeight(IndividualVariations iVars, IIndividualVariationsParams sParams)
		{
			float weight = 0f;
			if (sParams.WaistWidth.weight != 0f)
				weight += sParams.WaistWidth.weight * Math.Abs(sParams.WaistWidth.target - iVars.WaistWidth);
			if (sParams.HeadSize.weight != 0f)
				weight += sParams.HeadSize.weight * Math.Abs(sParams.HeadSize.target - iVars.HeadSize);
			if (sParams.EartlerWidth.weight != 0f)
				weight += sParams.EartlerWidth.weight * Math.Abs(sParams.EartlerWidth.target - iVars.EartlerWidth);
			if (sParams.NeckThickness.weight != 0f)
				weight += sParams.NeckThickness.weight * Math.Abs(sParams.NeckThickness.target - iVars.NeckThickness);
			if (sParams.HandsHeadColor.weight != 0f)
				weight += sParams.HandsHeadColor.weight * Math.Abs(sParams.HandsHeadColor.target - iVars.HandsHeadColor);
			if (sParams.EyeSize.weight != 0f)
				weight += sParams.EyeSize.weight * Math.Abs(sParams.EyeSize.target - iVars.EyeSize);
			if (sParams.NarrowEyes.weight != 0f)
				weight += sParams.NarrowEyes.weight * Math.Abs(sParams.NarrowEyes.target - iVars.NarrowEyes);
			if (sParams.EyesAngle.weight != 0f)
				weight += sParams.EyesAngle.weight * Math.Abs(sParams.EyesAngle.target - iVars.EyesAngle);
			if (sParams.Fatness.weight != 0f)
				weight += sParams.Fatness.weight * Math.Abs(sParams.Fatness.target - iVars.Fatness);
			if (sParams.NarrowWaist.weight != 0f)
				weight += sParams.NarrowWaist.weight * Math.Abs(sParams.NarrowWaist.target - iVars.NarrowWaist);
			if (sParams.LegsSize.weight != 0f)
				weight += sParams.LegsSize.weight * Math.Abs(sParams.LegsSize.target - iVars.LegsSize);
			if (sParams.ArmThickness.weight != 0f)
				weight += sParams.ArmThickness.weight * Math.Abs(sParams.ArmThickness.target - iVars.ArmThickness);
			if (sParams.WideTeeth.weight != 0f)
				weight += sParams.WideTeeth.weight * Math.Abs(sParams.WideTeeth.target - iVars.WideTeeth);
			if (sParams.PupilSize.weight != 0f)
				weight += sParams.PupilSize.weight * Math.Abs(sParams.PupilSize.target - iVars.PupilSize);
			if (sParams.Scruffy.weight != 0f)
				weight += sParams.Scruffy.weight * Math.Abs(sParams.Scruffy.target - iVars.Scruffy);
			if (sParams.ColoredEartlerTips.weight != 0f)
				weight += sParams.ColoredEartlerTips.weight * (sParams.ColoredEartlerTips.target == iVars.ColoredEartlerTips ? 0f : 1f);
			if (sParams.DeepPupils.weight != 0f)
				weight += sParams.DeepPupils.weight * (sParams.DeepPupils.target == iVars.DeepPupils ? 0f : 1f);
			if (sParams.ColoredPupils.weight != 0f)
				weight += sParams.ColoredPupils.weight * Math.Abs(sParams.ColoredPupils.target - iVars.ColoredPupils);
			if (sParams.TailSegs.weight != 0f)
				weight += sParams.TailSegs.weight * Math.Abs(sParams.TailSegs.target - iVars.TailSegs);
			if (sParams.GeneralMelanin.weight != 0f)
				weight += sParams.GeneralMelanin.weight * Math.Abs(sParams.GeneralMelanin.target - iVars.GeneralMelanin);
			return weight;
		}
		private static float EartlersWeight() => throw new NotImplementedException();
		private static float ScavColorsWeight(ScavColors colors, IScavColorsParams sParams)
		{
			float weight = 0f;
			if (sParams.BellyColorH.weight != 0f)
				weight += sParams.BellyColorH.weight * Math.Abs(sParams.BellyColorH.target - colors.BellyColor.H);
			if (sParams.BellyColorS.weight != 0f)
					weight += sParams.BellyColorS.weight * Math.Abs(sParams.BellyColorS.target - colors.BellyColor.S);
			if (sParams.BellyColorL.weight != 0f)
			weight += sParams.BellyColorL.weight * Math.Abs(sParams.BellyColorL.target - colors.BellyColor.L);
			if (sParams.BodyColorH.weight != 0f)
				weight += sParams.BodyColorH.weight * Math.Abs(sParams.BodyColorH.target - colors.BodyColor.H);
			if (sParams.BodyColorS.weight != 0f)
					weight += sParams.BodyColorS.weight * Math.Abs(sParams.BodyColorS.target - colors.BodyColor.S);
			if (sParams.BodyColorL.weight != 0f)
			weight += sParams.BodyColorL.weight * Math.Abs(sParams.BodyColorL.target - colors.BodyColor.L);
			if (sParams.DecorationColorH.weight != 0f)
				weight += sParams.DecorationColorH.weight * Math.Abs(sParams.DecorationColorH.target - colors.DecorationColor.H);
			if (sParams.DecorationColorS.weight != 0f)
					weight += sParams.DecorationColorS.weight * Math.Abs(sParams.DecorationColorS.target - colors.DecorationColor.S);
			if (sParams.DecorationColorL.weight != 0f)
			weight += sParams.DecorationColorL.weight * Math.Abs(sParams.DecorationColorL.target - colors.DecorationColor.L);
			if (sParams.EyeColorH.weight != 0f)
				weight += sParams.EyeColorH.weight * Math.Abs(sParams.EyeColorH.target - colors.EyeColor.H);
			if (sParams.EyeColorL.weight != 0f)
					weight += sParams.EyeColorL.weight * Math.Abs(sParams.EyeColorL.target - colors.EyeColor.L);
			if (sParams.HeadColorH.weight != 0f)
			weight += sParams.HeadColorH.weight * Math.Abs(sParams.HeadColorH.target - colors.HeadColor.H);
			if (sParams.HeadColorS.weight != 0f)
				weight += sParams.HeadColorS.weight * Math.Abs(sParams.HeadColorS.target - colors.HeadColor.S);
			if (sParams.HeadColorL.weight != 0f)
					weight += sParams.HeadColorL.weight * Math.Abs(sParams.HeadColorL.target - colors.HeadColor.L);
			if (sParams.BellyColorBlack.weight != 0f)
			weight += sParams.BellyColorBlack.weight * Math.Abs(sParams.BellyColorBlack.target - colors.BellyColorBlack);
			if (sParams.BodyColorBlack.weight != 0f)
				weight += sParams.BodyColorBlack.weight * Math.Abs(sParams.BodyColorBlack.target - colors.BodyColorBlack);
			if (sParams.HeadColorBlack.weight != 0f)
					weight += sParams.HeadColorBlack.weight * Math.Abs(sParams.HeadColorBlack.target - colors.HeadColorBlack);
			return weight;
		}
		private static float ScavSkillsWeight(ScavSkills skills, IScavSkillsParams sParams)
		{
			float weight = 0f;
			if (sParams.BlockingSkill.weight != 0f)
				weight += sParams.BlockingSkill.weight * Math.Abs(sParams.BlockingSkill.target - skills.BlockingSkill);
			if (sParams.DodgeSkill.weight != 0f)
				weight += sParams.DodgeSkill.weight * Math.Abs(sParams.DodgeSkill.target - skills.DodgeSkill);
			if (sParams.MeleeSkill.weight != 0f)
				weight += sParams.MeleeSkill.weight * Math.Abs(sParams.MeleeSkill.target - skills.MeleeSkill);
			if (sParams.MidRangeSkill.weight != 0f)
				weight += sParams.MidRangeSkill.weight * Math.Abs(sParams.MidRangeSkill.target - skills.MidRangeSkill);
			if (sParams.ReactionSkill.weight != 0f)
				weight += sParams.ReactionSkill.weight * Math.Abs(sParams.ReactionSkill.target - skills.ReactionSkill);
			return weight;
		}
		private static float ScavBackPatternsWeight(BackTuftsAndRidges back, IScavBackPatternsParams sParams)
		{
			float weight = 0f;
			if (sParams.Top.weight != 0f)
				weight += sParams.Top.weight * Math.Abs(sParams.Top.target - back.Top);
			if (sParams.Bottom.weight != 0f)
				weight += sParams.Bottom.weight * Math.Abs(sParams.Bottom.target - back.Bottom);
			if (sParams.Pattern.weight != 0f)
				weight += sParams.Pattern.weight * Math.Abs(sParams.Pattern.target - back.Pattern);
			if (sParams.Type.weight != 0f)
				weight += sParams.Type.weight * (sParams.Type.target == back.Type ? 0f : 1f);
			if (sParams.ColorType.weight != 0f)
				weight += sParams.ColorType.weight * Math.Abs(sParams.ColorType.target - back.ColorType);
			if (sParams.IsColored.weight != 0f)
				weight += sParams.IsColored.weight * (sParams.IsColored.target == back.IsColored ? 0f : 1f);
			if (sParams.ScaleGraf.weight != 0f)
				weight += sParams.ScaleGraf.weight * Math.Abs(sParams.ScaleGraf.target - back.ScaleGraf);
			if (sParams.GeneralSize.weight != 0f)
				weight += sParams.GeneralSize.weight * Math.Abs(sParams.GeneralSize.target - back.GeneralSize);
			if (sParams.Colored.weight != 0f)
				weight += sParams.Colored.weight * Math.Abs(sParams.Colored.target - back.Colored);
			if (sParams.NumberOfSpines.weight != 0f)
				weight += sParams.NumberOfSpines.weight * Math.Abs(sParams.NumberOfSpines.target - back.NumberOfSpines);
			return weight;
		}
		#endregion
		public static IEnumerable<KeyValuePair<float, int>> Search(int start, int stop, int numToStore, SearchParams SearchParams, bool logPercents = false)  // logPercents doesn't fit anything other than console.
		{
			// Must remember to update XORShiftSearch when updating this method. Not merged into one as different object constructors are used for each struct/class being searched.
			Init(1);
			bool boolPersonality = !((IPersonalityParams)SearchParams).AllWeightless();
			bool boolNpcStats = !((INPCStatsParams)SearchParams).AllWeightless();
			bool boolSlugcatStats = !((ISlugcatStatsParams)SearchParams).AllWeightless();
			bool boolFoodPreferences = !((IFoodPreferencesParams)SearchParams).AllWeightless();
			bool boolScavVariations = !((IIndividualVariationsParams)SearchParams).AllWeightless();
			bool boolScavColors = !((IScavColorsParams)SearchParams).AllWeightless();
			bool boolScavSkills = !((IScavSkillsParams)SearchParams).AllWeightless();
			bool boolScavBack = !((IScavBackPatternsParams)SearchParams).AllWeightless();
			bool isElite = SearchParams.Elite;

			List<KeyValuePair<float, int>> vals = new(numToStore + 1);   // uses comparison property of Searcher class to ensure smallest weights at index 0. Unfortunately this handles large numbers of elements far worse than SortedList, so it's recommended to use SearchMulti with an increased number of threads and trimNumToStore disabled rather than a numToStore in the tens of thousands.

			float weight;
			bool saturated = false;
			float percentRange = 1f + ((long)stop - (long)start);	// +1f makes it inclusive of stop. long casts avoid overflow errors.
			long startLong = start;	// Avoid having to cast repeatedly, makes the expression i - startLong into a long rather than an int that'd overflow. 

			Personality personality = default;
			NPCStats npcStats = default;
			SlugcatStats slugStats = default;
			FoodPreferences foodPref = default;
			ScavSkills scavSkills = default;

			int largerThanIndex;
			float lastPercent = 0f;	// For pasting to console only. 
			KeyValuePair<float, int> kvp;
			for (int i = start; i <= stop; i++)
			{
				if ((i & 127) == 0)	// More performant than i % 128
				{
					if (abortSearch)
						return vals;

					if (logPercents)
					{
						_completions[0] = 100f * (i - startLong) / percentRange;
						if (CompletionPercent - lastPercent > 0.99f)
						{
							Console.WriteLine(CompletionPercent + "%");
							lastPercent = CompletionPercent;
						}
					}
				}
				#region scugs
				weight = 0f;
				if (boolPersonality)
				{
					personality = new(i);
					weight += PersonalityWeight(personality, SearchParams);
				}
				if (boolNpcStats)
				{
					npcStats = new(i);
					weight += NPCStatsWeight(npcStats, SearchParams);
				}
				if (boolSlugcatStats)
				{
					if (!boolNpcStats) npcStats = new(i);
					slugStats = new(npcStats);
					weight += SlugcatStatsWeight(slugStats, SearchParams);
				}
				if (boolFoodPreferences)
				{
					if (!boolPersonality) personality = new(i);
					foodPref = new(i, personality);
					weight += FoodPreferencesWeight(foodPref, SearchParams);
				}
				#endregion
				#region scavs
				if (boolScavSkills) // Needs personality
				{
					if (!boolPersonality) personality = new(i);
					scavSkills = new(i, personality, isElite);
					weight += ScavSkillsWeight(scavSkills, SearchParams);
				}
				if (boolScavVariations || boolScavBack || boolScavColors)   // Needs personality
				{
					if (!boolPersonality && !boolScavSkills) personality = new(i);

					if (!(boolScavBack || boolScavColors))   // Avoid having to call GetGraphics if only variations are needed
					{
						XORShift128.InitSeed(i);
						weight += IndividualVariationsWeight(new IndividualVariations(personality, isElite), SearchParams);
					}
					else
					{
						(IndividualVariations? variations, ScavColors? color, BackTuftsAndRidges? back) graphics = Scavenger.GetGraphics(i, isElite, boolScavVariations, boolScavColors, boolScavBack, (boolPersonality || boolScavSkills || boolScavVariations) ? personality : null);

						if (boolScavVariations)
							weight += IndividualVariationsWeight((IndividualVariations)graphics.variations!, SearchParams);
						if (boolScavColors)
							weight += ScavColorsWeight((ScavColors)graphics.color!, SearchParams);
						if (boolScavBack)
							weight += ScavBackPatternsWeight(graphics.back!, SearchParams);
					}
				}
				#endregion
				if (!saturated && vals.Count < numToStore)
				{
					vals.Add(new(weight, i));
					if (vals.Count == numToStore)
					{
						vals = vals.OrderBy(kvp => kvp.Key).ToList();
						saturated = true;
					}
				}
				else if (vals.Last().Key > weight)
				{
					// vals is certainly ordered at this point, as vals is sorted when vals.Count == numToStore. Therefore I can do a binary search ( log(n) ) to find the index of the next largest or equal weight, insert there ( O(n) where n is list.count - index ), and remove the final element ( O(1) ). Much faster than appending a value and sorting the whole list each time and significantly decreases search time with large numToStore.
					kvp = new(weight, i);
					largerThanIndex = vals.BinarySearch(kvp, comparer);
					if (largerThanIndex < 0)
						largerThanIndex = ~largerThanIndex; // bitwise complement of a BinarySearch that does not find the value returns the index of the next largest value in the list.

					vals.Insert(largerThanIndex, kvp);
					vals.RemoveAt(numToStore);
				}

				if (i == int.MaxValue)  // Guards against overflow causing infinite looping, allows int stop to be inclusive rather than exclusive.
					break;
			}

			vals = vals.OrderBy(kvp => kvp.Key).ThenBy(kvp => kvp.Value).ToList();	// When weights are equal, ordered by ID in ascending order.
			return vals;
		}


		public static IEnumerable<KeyValuePair<float, int>> SearchThreaded(int start, int stop, int numToStore, int threads, SearchParams SearchParams, bool trimToNumToStore = true, bool logPercents = false)
		{
			var comparer = Comparer<KeyValuePair<float, int>>.Create((x, y) => x.Key <= y.Key ? -1 : 1);
			if (threads < 1)
				throw new ArgumentException("Cannot use less than one thread!");
			if (threads == 1)
				return Search(start, stop, numToStore, SearchParams, logPercents);

            Init(threads);
            int[][] chunks = Chunker(start, stop, threads);
			ConcurrentBag<IEnumerable<KeyValuePair<float, int>>> resultCollection = new();

			// Shallow copying SearchParams in the expectation that a bottleneck could arise when multiple threads access the same instance

			Parallel.For(0, threads, i =>
			{
				var res = XORShiftSearch(chunks[i][0], chunks[i][1], numToStore, SearchParams.Clone(), new InstanceXORShift128(), threads, i, logPercents);
				resultCollection.Add(res);
			});

			IEnumerable<KeyValuePair<float, int>> resultsSorted = new List<KeyValuePair<float, int>>();
			foreach (var result in resultCollection)
				resultsSorted = resultsSorted.Concat(result);

			resultsSorted = resultsSorted.OrderBy(kvp => kvp.Key).ThenBy(kvp => kvp.Value);

			if (trimToNumToStore)
				return resultsSorted.Take(numToStore);

			return resultsSorted;
		}
		private static IEnumerable<KeyValuePair<float, int>> XORShiftSearch(int start, int stop, int numToStore, SearchParams SearchParams, InstanceXORShift128 XORShift128, int threads, int whichThreadAmI, bool logPercents = false)
		{
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
			bool saturated = false;
            float percentRange = 1f + ((long)stop - (long)start);   // +1f makes it inclusive of stop. long casts avoid overflow errors.
            long startLong = start; // Avoid having to cast repeatedly

            Personality personality = default;
			NPCStats npcStats = default;
			SlugcatStats slugStats = default;
			FoodPreferences foodPref = default;
			ScavSkills scavSkills = default;
			
			int largerThanIndex;
            float lastPercent = 0f; // For pasting to console only. 
            KeyValuePair<float, int> kvp;
			for (int i = start; i <= stop; i++)
			{
                if ((i & 127) == 0)
                {
                    if (abortSearch)
                        return vals;

                    if (logPercents)
                    {
						_completions[whichThreadAmI] = 100f * (i - startLong) / percentRange;	
                        if (_completions[whichThreadAmI] - lastPercent > 0.99f)
                        {
                            Console.WriteLine(Math.Round(_completions[whichThreadAmI], 1, MidpointRounding.AwayFromZero) + "%");
                            lastPercent = _completions[whichThreadAmI];
                        }
                    }
                }

                //if ((i & 127) == 0 && AbortSearch)	// i & (2^n - 1) == 0 is true once every 2^n increments of i. Minimising how often AbortSearch needs to be accessed, which improves /performance/ by a few % in multithreaded workloads. Can also merge percent logging into here later and 
                //    return vals;
                //
                //if (logPercents && ( i == int.MaxValue || (i - start) % percentInterval == 0))	// MaxValue stopgap to avoid divide by zero exception that crashed after 2 hours min to maxvalue.
                //{
                //	percentTracker++;
                //	Console.WriteLine($"{percentTracker}%");
                //}
                #region scugs
                weight = 0f;
				if (boolPersonality)
				{
					personality = new(i, XORShift128);
					weight += PersonalityWeight(personality, SearchParams);
				}
				if (boolNpcStats)
				{
					npcStats = new(i, XORShift128);
					weight += NPCStatsWeight(npcStats, SearchParams);
				}
				if (boolSlugcatStats)
				{
					if (!boolNpcStats) npcStats = new(i, XORShift128);
					slugStats = new(npcStats);	// SlugcatStats makes no use of rng in its constructor
					weight += SlugcatStatsWeight(slugStats, SearchParams);
				}
				if (boolFoodPreferences)
				{
					if (!boolPersonality) personality = new(i, XORShift128);
					foodPref = new(i, personality, XORShift128);
					weight += FoodPreferencesWeight(foodPref, SearchParams);
				}
				#endregion
				#region scavs
				if (boolScavSkills) 
				{
					if (!boolPersonality) personality = new(i, XORShift128);
					scavSkills = new(i, personality, XORShift128, isElite);
					weight += ScavSkillsWeight(scavSkills, SearchParams);
				}
				if (boolScavVariations || boolScavBack || boolScavColors)
				{
					if (!boolPersonality && !boolScavSkills) personality = new(i, XORShift128);

					if (!(boolScavBack || boolScavColors))
					{
						weight += IndividualVariationsWeight(new IndividualVariations(personality, XORShift128, isElite), SearchParams);
					}
					else
					{
						(IndividualVariations? variations, ScavColors? color, BackTuftsAndRidges? back) graphics = Scavenger.GetGraphicsRNGParam(i, XORShift128, isElite, boolScavVariations, boolScavColors, boolScavBack, (boolPersonality || boolScavSkills || boolScavVariations) ? personality : null);

						if (boolScavVariations)
							weight += IndividualVariationsWeight((IndividualVariations)graphics.variations!, SearchParams);
						if (boolScavColors)
							weight += ScavColorsWeight((ScavColors)graphics.color!, SearchParams);
						if (boolScavBack)
							weight += ScavBackPatternsWeight(graphics.back!, SearchParams);
					}
				}
				#endregion
				if (!saturated && vals.Count < numToStore)
				{
					vals.Add(new(weight, i));
					if (vals.Count == numToStore)
					{
						//vals.Sort(comparer);
						vals = vals.OrderBy(kvp => kvp.Key).ThenBy(kvp => kvp.Value).ToList();
						saturated = true;
					}
				}
				else if (vals.Last().Key > weight)
				{
					// vals is certainly ordered at this point, as vals is sorted when vals.Count == numToStore. Therefore I can do a binary search ( log(n) ) to find the index of the next largest or equal weight, insert there ( O(n) where n is list.count - index ), and remove the final element ( O(1) ). Much faster than appending a value and sorting the whole list each time and significantly decreases search time with large numToStore.
					kvp = new(weight, i);
					largerThanIndex = vals.BinarySearch(kvp, comparer);
					if (largerThanIndex < 0)
						largerThanIndex = ~largerThanIndex; // bitwise complement of a BinarySearch that does not find the value returns the index of the next largest value in the list.

					vals.Insert(largerThanIndex, kvp);
					vals.RemoveAt(numToStore);
				}

				if (i == int.MaxValue)  // Guards against overflow causing infinite looping, allows int stop to be inclusive rather than exclusive.
					break;
			}

			return vals;
		}
		internal static int[][] Chunker(int start, int stop, int threads)
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
	[Obsolete("Use Searcher instead")]
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
			if (SearchParams.Sympathy.weight != 0f)
				weight += SearchParams.Sympathy.weight * Math.Abs(p.Sympathy - SearchParams.Sympathy.target);
			if (SearchParams.Energy.weight != 0f)
				weight += SearchParams.Energy.weight * Math.Abs(p.Energy - SearchParams.Energy.target);
			if (SearchParams.Bravery.weight != 0f)
				weight += SearchParams.Bravery.weight * Math.Abs(p.Bravery - SearchParams.Bravery.target);
			if (SearchParams.Nervous.weight != 0f)
				weight += SearchParams.Nervous.weight * Math.Abs(p.Nervous - SearchParams.Nervous.target);
			if (SearchParams.Aggression.weight != 0f)
				weight += SearchParams.Aggression.weight * Math.Abs(p.Aggression - SearchParams.Aggression.target);
			if (SearchParams.Dominance.weight != 0f)
				weight += SearchParams.Dominance.weight * Math.Abs(p.Dominance - SearchParams.Dominance.target);
			return weight;
		}
		private float NPCStatsWeight(NPCStats npc)
		{
			float weight = 0f;
			if (SearchParams.Met.weight != 0f)
				weight += SearchParams.Met.weight * Math.Abs(npc.Met - SearchParams.Met.target);
			if (SearchParams.Bal.weight != 0f)
				weight += SearchParams.Bal.weight * Math.Abs(npc.Bal - SearchParams.Bal.target);
			if (SearchParams.Size.weight != 0f)
				weight += SearchParams.Size.weight * Math.Abs(npc.Size - SearchParams.Size.target);
			if (SearchParams.Stealth.weight != 0f)
				weight += SearchParams.Stealth.weight * Math.Abs(npc.Stealth - SearchParams.Stealth.target);
			if (SearchParams.Dark.weight != 0f)
				weight += SearchParams.Dark.weight * (npc.Dark == SearchParams.Dark.target ? 1 : 0);
			if (SearchParams.EyeColor.weight != 0f)
				weight += SearchParams.EyeColor.weight * Math.Abs(npc.EyeColor - SearchParams.EyeColor.target);
			if (SearchParams.H.weight != 0f)
				weight += SearchParams.H.weight * Math.Abs(npc.H - SearchParams.H.target);
			if (SearchParams.S.weight != 0f)
				weight += SearchParams.S.weight * Math.Abs(npc.S - SearchParams.S.target);
			if (SearchParams.L.weight != 0f)
				weight += SearchParams.L.weight * Math.Abs(npc.L - SearchParams.L.target);
			if (SearchParams.Wideness.weight != 0f)
				weight += SearchParams.Wideness.weight * Math.Abs(npc.Wideness - SearchParams.Wideness.target);
			return weight;
		}
		private float SlugcatStatsWeight(SlugcatStats slug)
		{
			float weight = 0f;
			if (SearchParams.BodyWeightFac.weight != 0f)
				weight += SearchParams.BodyWeightFac.weight * Math.Abs(slug.bodyWeightFac - SearchParams.BodyWeightFac.target);
			if (SearchParams.GeneralVisibilityBonus.weight != 0f)
				weight += SearchParams.GeneralVisibilityBonus.weight * Math.Abs(slug.generalVisibilityBonus - SearchParams.GeneralVisibilityBonus.target);
			if (SearchParams.VisualStealthInSneakMode.weight != 0f)
				weight += SearchParams.VisualStealthInSneakMode.weight * Math.Abs(slug.visualStealthInSneakMode - SearchParams.VisualStealthInSneakMode.target);
			if (SearchParams.LoudnessFac.weight != 0f )
				weight += SearchParams.LoudnessFac.weight * Math.Abs(slug.loudnessFac - SearchParams.LoudnessFac.target);
			if (SearchParams.LungsFac.weight != 0f)
				weight += SearchParams.LungsFac.weight * Math.Abs(slug.lungsFac - SearchParams.LungsFac.target);
			if (SearchParams.ThrowingSkill.weight != 0f)
				weight += SearchParams.ThrowingSkill.weight * Math.Abs(slug.throwingSkill - SearchParams.ThrowingSkill.target);
			if (SearchParams.PoleClimbSpeedFac.weight != 0f)
				weight += SearchParams.PoleClimbSpeedFac.weight * Math.Abs(slug.poleClimbSpeedFac - SearchParams.PoleClimbSpeedFac.target);
			if (SearchParams.CorridorClimbSpeedFac.weight != 0f)
				weight += SearchParams.CorridorClimbSpeedFac.weight * Math.Abs(slug.corridorClimbSpeedFac - SearchParams.CorridorClimbSpeedFac.target);
			if (SearchParams.RunSpeedFac.weight != 0f)
				weight += SearchParams.RunSpeedFac.weight * Math.Abs(slug.runSpeedFac - SearchParams.RunSpeedFac.target);
			return weight;
		}

		private float FoodPreferencesWeight(FoodPreferences foodPref)
		{
			float weight = 0f;
			if (SearchParams.DangleFruit.weight != 0f)
				weight += SearchParams.DangleFruit.weight * Math.Abs(foodPref.DangleFruit - SearchParams.DangleFruit.target);
			if (SearchParams.WaterNut.weight != 0f)
				weight += SearchParams.WaterNut.weight * Math.Abs(foodPref.WaterNut - SearchParams.WaterNut.target);
			if (SearchParams.JellyFish.weight != 0f)
				weight += SearchParams.JellyFish.weight * Math.Abs(foodPref.JellyFish - SearchParams.JellyFish.target);
			if (SearchParams.SlimeMold.weight != 0f)
				weight += SearchParams.SlimeMold.weight * Math.Abs(foodPref.SlimeMold - SearchParams.SlimeMold.target);
			if (SearchParams.EggBugEgg.weight != 0f)
				weight += SearchParams.EggBugEgg.weight * Math.Abs(foodPref.EggBugEgg - SearchParams.EggBugEgg.target);
			if (SearchParams.FireEgg.weight != 0f)
				weight += SearchParams.FireEgg.weight * Math.Abs(foodPref.FireEgg - SearchParams.FireEgg.target);
			if (SearchParams.Popcorn.weight != 0f)
				weight += SearchParams.Popcorn.weight * Math.Abs(foodPref.Popcorn - SearchParams.Popcorn.target);
			if (SearchParams.GooieDuck.weight != 0f)
				weight += SearchParams.GooieDuck.weight * Math.Abs(foodPref.GooieDuck - SearchParams.GooieDuck.target);
			if (SearchParams.LilyPuck.weight != 0f)
				weight += SearchParams.LilyPuck.weight * Math.Abs(foodPref.LilyPuck - SearchParams.LilyPuck.target);
			if (SearchParams.GlowWeed.weight != 0f)
				weight += SearchParams.GlowWeed.weight * Math.Abs(foodPref.GlowWeed - SearchParams.GlowWeed.target);
			if (SearchParams.DandelionPeach.weight != 0f)
				weight += SearchParams.DandelionPeach.weight * Math.Abs(foodPref.DandelionPeach - SearchParams.DandelionPeach.target);
			if (SearchParams.Neuron.weight != 0f)
				weight += SearchParams.Neuron.weight * Math.Abs(foodPref.Neuron - SearchParams.Neuron.target);
			if (SearchParams.Centipede.weight != 0f)
				weight += SearchParams.Centipede.weight * Math.Abs(foodPref.Centipede - SearchParams.Centipede.target);
			if (SearchParams.SmallCentipede.weight != 0f)
				weight += SearchParams.SmallCentipede.weight * Math.Abs(foodPref.SmallCentipede - SearchParams.SmallCentipede.target);
			if (SearchParams.VultureGrub.weight != 0f)
				weight += SearchParams.VultureGrub.weight * Math.Abs(foodPref.VultureGrub - SearchParams.VultureGrub.target);
			if (SearchParams.SmallNeedleWorm.weight != 0f) 
				weight += SearchParams.SmallNeedleWorm.weight * Math.Abs(foodPref.SmallNeedleWorm - SearchParams.SmallNeedleWorm.target);
			if (SearchParams.Hazer.weight != 0f)
				weight += SearchParams.Hazer.weight * Math.Abs(foodPref.Hazer - SearchParams.Hazer.target);
			if (SearchParams.NotCounted.weight != 0f) 
				weight += SearchParams.NotCounted.weight * Math.Abs(foodPref.NotCounted - SearchParams.NotCounted.target);
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
			SortedList<float, Slugcat> vals = new();   // smallest value at index 0
			float weight;
			bool saturated = false;
			vals.Capacity = numToStore;
			long percentInterval = ((long)stop - (long)start) / 100;	// long cast avoids int32 overflow edge cases that cause a DivideByZero exception.
			int percentTracker = 0;

			bool personality = !((IPersonalityParams)SearchParams).AllWeightless();
			bool npcStats = !((INPCStatsParams)SearchParams).AllWeightless();
			bool slugcatStats = !((ISlugcatStatsParams)SearchParams).AllWeightless();
			bool foodPreferences = !((IFoodPreferencesParams)SearchParams).AllWeightless();

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
					slugStats = new(npc);
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
                    slugStats = new(npc);
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
	}
}
