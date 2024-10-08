﻿using System.Collections.Concurrent;

namespace IDFinder
{
	// Have an ISearcher interface, each creature has its own class that implements it. This'll avoid having a disgusting number of parameters, and SearchParams can be a nested class
	public static class Searcher
	{
		private static readonly IComparer<KeyValuePair<float, int>> comparer = Comparer<KeyValuePair<float, int>>.Create((x, y) => x.Key.CompareTo(y.Key));
		private static bool abortSearch = false;   // I'd rather this be a private field and have an Abort() method invoked that sets it to true, awaits a Task.Delay, and sets it back to false afterwards. I feel there's too much risk to forget to reset it, or for race condition shenanigans. 
		private static float[] _completions = [];
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
		private static float NPCStatsWeight(NPCStats npc, INPCStatsParams sParams)
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
				weight += sParams.Dark.Value.weight * (npc.Dark == sParams.Dark.Value.target ? 0 : 1);
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
		private static float SlugcatStatsWeight(SlugcatStats slug, ISlugcatStatsParams sParams)
		{
			float weight = 0f;
			if (sParams.BodyWeightFac != null)
				weight += sParams.BodyWeightFac.Value.weight * Math.Abs(slug.bodyWeightFac - sParams.BodyWeightFac.Value.target);
			if (sParams.GeneralVisibilityBonus != null)
				weight += sParams.GeneralVisibilityBonus.Value.weight * Math.Abs(slug.generalVisibilityBonus - sParams.GeneralVisibilityBonus.Value.target);
			if (sParams.VisualStealthInSneakMode != null)
				weight += sParams.VisualStealthInSneakMode.Value.weight * Math.Abs(slug.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.Value.target);
			if (sParams.LoudnessFac != null)
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
		private static float FoodPreferencesWeight(FoodPreferences foodPref, IFoodPreferencesParams sParams)
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
		private static float IndividualVariationsWeight(IndividualVariations iVars, IIndividualVariationsParams sParams)
		{
			float weight = 0f;
			if (sParams.WaistWidth != null)
				weight += sParams.WaistWidth.Value.weight * Math.Abs(sParams.WaistWidth.Value.target - iVars.WaistWidth);
			if (sParams.HeadSize != null)
				weight += sParams.HeadSize.Value.weight * Math.Abs(sParams.HeadSize.Value.target - iVars.HeadSize);
			if (sParams.EartlerWidth != null)
				weight += sParams.EartlerWidth.Value.weight * Math.Abs(sParams.EartlerWidth.Value.target - iVars.EartlerWidth);
			if (sParams.NeckThickness != null)
				weight += sParams.NeckThickness.Value.weight * Math.Abs(sParams.NeckThickness.Value.target - iVars.NeckThickness);
			if (sParams.HandsHeadColor != null)
				weight += sParams.HandsHeadColor.Value.weight * Math.Abs(sParams.HandsHeadColor.Value.target - iVars.HandsHeadColor);
			if (sParams.EyeSize != null)
				weight += sParams.EyeSize.Value.weight * Math.Abs(sParams.EyeSize.Value.target - iVars.EyeSize);
			if (sParams.NarrowEyes != null)
				weight += sParams.NarrowEyes.Value.weight * Math.Abs(sParams.NarrowEyes.Value.target - iVars.NarrowEyes);
			if (sParams.EyesAngle != null)
				weight += sParams.EyesAngle.Value.weight * Math.Abs(sParams.EyesAngle.Value.target - iVars.EyesAngle);
			if (sParams.Fatness != null)
				weight += sParams.Fatness.Value.weight * Math.Abs(sParams.Fatness.Value.target - iVars.Fatness);
			if (sParams.NarrowWaist != null)
				weight += sParams.NarrowWaist.Value.weight * Math.Abs(sParams.NarrowWaist.Value.target - iVars.NarrowWaist);
			if (sParams.LegsSize != null)
				weight += sParams.LegsSize.Value.weight * Math.Abs(sParams.LegsSize.Value.target - iVars.LegsSize);
			if (sParams.ArmThickness != null)
				weight += sParams.ArmThickness.Value.weight * Math.Abs(sParams.ArmThickness.Value.target - iVars.ArmThickness);
			if (sParams.WideTeeth != null)
				weight += sParams.WideTeeth.Value.weight * Math.Abs(sParams.WideTeeth.Value.target - iVars.WideTeeth);
			if (sParams.PupilSize != null)
				weight += sParams.PupilSize.Value.weight * Math.Abs(sParams.PupilSize.Value.target - iVars.PupilSize);
			if (sParams.Scruffy != null)
				weight += sParams.Scruffy.Value.weight * Math.Abs(sParams.Scruffy.Value.target - iVars.Scruffy);
			if (sParams.ColoredEartlerTips != null)
				weight += sParams.ColoredEartlerTips.Value.weight * (sParams.ColoredEartlerTips.Value.target == iVars.ColoredEartlerTips ? 0f : 1f);
			if (sParams.DeepPupils != null)
				weight += sParams.DeepPupils.Value.weight * (sParams.DeepPupils.Value.target == iVars.DeepPupils ? 0f : 1f);
			if (sParams.ColoredPupils != null)
				weight += sParams.ColoredPupils.Value.weight * Math.Abs(sParams.ColoredPupils.Value.target - iVars.ColoredPupils);
			if (sParams.TailSegs != null)
				weight += sParams.TailSegs.Value.weight * Math.Abs(sParams.TailSegs.Value.target - iVars.TailSegs);
			if (sParams.GeneralMelanin != null)
				weight += sParams.GeneralMelanin.Value.weight * Math.Abs(sParams.GeneralMelanin.Value.target - iVars.GeneralMelanin);
			return weight;
		}
		private static float EartlersWeight() => throw new NotImplementedException();
		private static float ScavColorsWeight(ScavColors colors, IScavColorsParams sParams)
		{
			float weight = 0f;
			if (sParams.BellyColorH != null)
				weight += sParams.BellyColorH.Value.weight * Math.Abs(sParams.BellyColorH.Value.target - colors.BellyColor.H);
			if (sParams.BellyColorS != null)
					weight += sParams.BellyColorS.Value.weight * Math.Abs(sParams.BellyColorS.Value.target - colors.BellyColor.S);
			if (sParams.BellyColorL != null)
			weight += sParams.BellyColorL.Value.weight * Math.Abs(sParams.BellyColorL.Value.target - colors.BellyColor.L);
			if (sParams.BodyColorH != null)
				weight += sParams.BodyColorH.Value.weight * Math.Abs(sParams.BodyColorH.Value.target - colors.BodyColor.H);
			if (sParams.BodyColorS != null)
					weight += sParams.BodyColorS.Value.weight * Math.Abs(sParams.BodyColorS.Value.target - colors.BodyColor.S);
			if (sParams.BodyColorL != null)
			weight += sParams.BodyColorL.Value.weight * Math.Abs(sParams.BodyColorL.Value.target - colors.BodyColor.L);
			if (sParams.DecorationColorH != null)
				weight += sParams.DecorationColorH.Value.weight * Math.Abs(sParams.DecorationColorH.Value.target - colors.DecorationColor.H);
			if (sParams.DecorationColorS != null)
					weight += sParams.DecorationColorS.Value.weight * Math.Abs(sParams.DecorationColorS.Value.target - colors.DecorationColor.S);
			if (sParams.DecorationColorL != null)
			weight += sParams.DecorationColorL.Value.weight * Math.Abs(sParams.DecorationColorL.Value.target - colors.DecorationColor.L);
			if (sParams.EyeColorH != null)
				weight += sParams.EyeColorH.Value.weight * Math.Abs(sParams.EyeColorH.Value.target - colors.EyeColor.H);
			if (sParams.EyeColorL != null)
					weight += sParams.EyeColorL.Value.weight * Math.Abs(sParams.EyeColorL.Value.target - colors.EyeColor.L);
			if (sParams.HeadColorH != null)
			weight += sParams.HeadColorH.Value.weight * Math.Abs(sParams.HeadColorH.Value.target - colors.HeadColor.H);
			if (sParams.HeadColorS != null)
				weight += sParams.HeadColorS.Value.weight * Math.Abs(sParams.HeadColorS.Value.target - colors.HeadColor.S);
			if (sParams.HeadColorL != null)
					weight += sParams.HeadColorL.Value.weight * Math.Abs(sParams.HeadColorL.Value.target - colors.HeadColor.L);
			if (sParams.BellyColorBlack != null)
			weight += sParams.BellyColorBlack.Value.weight * Math.Abs(sParams.BellyColorBlack.Value.target - colors.BellyColorBlack);
			if (sParams.BodyColorBlack != null)
				weight += sParams.BodyColorBlack.Value.weight * Math.Abs(sParams.BodyColorBlack.Value.target - colors.BodyColorBlack);
			if (sParams.HeadColorBlack != null)
					weight += sParams.HeadColorBlack.Value.weight * Math.Abs(sParams.HeadColorBlack.Value.target - colors.HeadColorBlack);
			return weight;
		}
		private static float ScavSkillsWeight(ScavSkills skills, IScavSkillsParams sParams)
		{
			float weight = 0f;
			if (sParams.BlockingSkill != null)
				weight += sParams.BlockingSkill.Value.weight * Math.Abs(sParams.BlockingSkill.Value.target - skills.BlockingSkill);
			if (sParams.DodgeSkill != null)
				weight += sParams.DodgeSkill.Value.weight * Math.Abs(sParams.DodgeSkill.Value.target - skills.DodgeSkill);
			if (sParams.MeleeSkill != null)
				weight += sParams.MeleeSkill.Value.weight * Math.Abs(sParams.MeleeSkill.Value.target - skills.MeleeSkill);
			if (sParams.MidRangeSkill != null)
				weight += sParams.MidRangeSkill.Value.weight * Math.Abs(sParams.MidRangeSkill.Value.target - skills.MidRangeSkill);
			if (sParams.ReactionSkill != null)
				weight += sParams.ReactionSkill.Value.weight * Math.Abs(sParams.ReactionSkill.Value.target - skills.ReactionSkill);
			return weight;
		}
		private static float ScavBackPatternsWeight(BackTuftsAndRidges back, IScavBackPatternsParams sParams)
		{
			float weight = 0f;
			if (sParams.Top != null)
				weight += sParams.Top.Value.weight * Math.Abs(sParams.Top.Value.target - back.Top);
			if (sParams.Bottom != null)
				weight += sParams.Bottom.Value.weight * Math.Abs(sParams.Bottom.Value.target - back.Bottom);
			if (sParams.Pattern != null)
				weight += sParams.Pattern.Value.weight * Math.Abs(sParams.Pattern.Value.target - back.Pattern);
			if (sParams.Type != null)
				weight += sParams.Type.Value.weight * (sParams.Type.Value.target == back.Type ? 0f : 1f);
			if (sParams.ColorType != null)
				weight += sParams.ColorType.Value.weight * Math.Abs(sParams.ColorType.Value.target - back.ColorType);
			if (sParams.IsColored != null)
				weight += sParams.IsColored.Value.weight * (sParams.IsColored.Value.target == back.IsColored ? 0f : 1f);
			if (sParams.ScaleGraf != null)
				weight += sParams.ScaleGraf.Value.weight * Math.Abs(sParams.ScaleGraf.Value.target - back.ScaleGraf);
			if (sParams.GeneralSize != null)
				weight += sParams.GeneralSize.Value.weight * Math.Abs(sParams.GeneralSize.Value.target - back.GeneralSize);
			if (sParams.Colored != null)
				weight += sParams.Colored.Value.weight * Math.Abs(sParams.Colored.Value.target - back.Colored);
			if (sParams.NumberOfSpines != null)
				weight += sParams.NumberOfSpines.Value.weight * Math.Abs(sParams.NumberOfSpines.Value.target - back.NumberOfSpines);
			return weight;
		}
		#endregion
		public static IEnumerable<KeyValuePair<float, int>> Search(int start, int stop, int numToStore, SearchParams SearchParams, bool logPercents = false)  // logPercents doesn't fit anything other than console.
		{
			// Must remember to update XORShiftSearch when updating this method. Not merged into one as different object constructors are used for each struct/class being searched.
			Init(1);
			bool boolPersonality = !((IPersonalityParams)SearchParams).AllNull();
			bool boolNpcStats = !((INPCStatsParams)SearchParams).AllNull();
			bool boolSlugcatStats = !((ISlugcatStatsParams)SearchParams).AllNull();
			bool boolFoodPreferences = !((IFoodPreferencesParams)SearchParams).AllNull();
			bool boolScavVariations = !((IIndividualVariationsParams)SearchParams).AllNull();
			bool boolScavColors = !((IScavColorsParams)SearchParams).AllNull();
			bool boolScavSkills = !((IScavSkillsParams)SearchParams).AllNull();
			bool boolScavBack = !((IScavBackPatternsParams)SearchParams).AllNull();
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
			ConcurrentBag<IEnumerable<KeyValuePair<float, int>>> resultCollection = [];

			// Shallow copying SearchParams in the expectation that a bottleneck could arise when multiple threads access the same instance

			Parallel.For(0, threads, i =>
			{
				var res = XORShiftSearch(chunks[i][0], chunks[i][1], numToStore, SearchParams.Clone(), new InstanceXORShift128(), threads, i, logPercents);
				resultCollection.Add(res);
			});

			IEnumerable<KeyValuePair<float, int>> resultsSorted = [];
			foreach (var result in resultCollection)
				resultsSorted = resultsSorted.Concat(result);

			resultsSorted = resultsSorted.OrderBy(kvp => kvp.Key).ThenBy(kvp => kvp.Value);

			if (trimToNumToStore)
				return resultsSorted.Take(numToStore);

			return resultsSorted;
		}
		private static IEnumerable<KeyValuePair<float, int>> XORShiftSearch(int start, int stop, int numToStore, SearchParams SearchParams, InstanceXORShift128 XORShift128, int threads, int whichThreadAmI, bool logPercents = false)
		{
			bool boolPersonality = !((IPersonalityParams)SearchParams).AllNull();
			bool boolNpcStats = !((INPCStatsParams)SearchParams).AllNull();
			bool boolSlugcatStats = !((ISlugcatStatsParams)SearchParams).AllNull();
			bool boolFoodPreferences = !((IFoodPreferencesParams)SearchParams).AllNull();
			bool boolScavVariations = !((IIndividualVariationsParams)SearchParams).AllNull();
			bool boolScavColors = !((IScavColorsParams)SearchParams).AllNull();
			bool boolScavSkills = !((IScavSkillsParams)SearchParams).AllNull();
			bool boolScavBack = !((IScavBackPatternsParams)SearchParams).AllNull();
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
				chunks[i] = [start + i * (int)chunkSize + 1, start + (i + 1) * (int)chunkSize];
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

			bool personality = !((IPersonalityParams)SearchParams).AllNull();
			bool npcStats = !((INPCStatsParams)SearchParams).AllNull();
			bool slugcatStats = !((ISlugcatStatsParams)SearchParams).AllNull();
			bool foodPreferences = !((IFoodPreferencesParams)SearchParams).AllNull();

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
