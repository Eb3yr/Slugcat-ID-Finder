using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IDFinder
{
	public class Searcher
	{
		// This is such a bad way to do it
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
		//const int chunkSize = 100000;   // do I need this? Could be useful for multithreading, to either dynamically generate it based on thread count or let the user select it
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

		// Split each class being searched into its own method. Eg Personality, NPCStats, etc and [MethodImpl(MethodImplOptions.AggressiveInlining)] them. This'll cut down on how many if statements I have to use. Maybe add some functionality to the sParams class to allow me to easily determine whether or not an instance contains certain groups, possibly also inline that.a
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float PersonalityWeight(Personality p)
		{
			float weight = 0f;
			if (sParams.Sympathy != null) weight += sParams.Sympathy.Value.weight * Math.Abs(p.Sympathy - sParams.Sympathy.Value.target);
			if (sParams.Energy != null) weight += sParams.Energy.Value.weight * Math.Abs(p.Energy - sParams.Energy.Value.target);
			if (sParams.Bravery != null) weight += sParams.Bravery.Value.weight * Math.Abs(p.Bravery - sParams.Bravery.Value.target);
			if (sParams.Nervous != null) weight += sParams.Nervous.Value.weight * Math.Abs(p.Nervous - sParams.Nervous.Value.target);
			if (sParams.Aggression != null) weight += sParams.Aggression.Value.weight * Math.Abs(p.Aggression - sParams.Aggression.Value.target);
			if (sParams.Dominance != null) weight += sParams.Dominance.Value.weight * Math.Abs(p.Dominance - sParams.Dominance.Value.target);
			return weight;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float NPCStatsWeight(NPCStats npc)
		{
			float weight = 0f;
			if (sParams.Met != null) weight += sParams.Met.Value.weight * Math.Abs(npc.Met - sParams.Met.Value.target);
			if (sParams.Bal != null) weight += sParams.Bal.Value.weight * Math.Abs(npc.Bal - sParams.Bal.Value.target);
			if (sParams.Size != null) weight += sParams.Size.Value.weight * Math.Abs(npc.Size - sParams.Size.Value.target);
			if (sParams.Stealth != null) weight += sParams.Stealth.Value.weight * Math.Abs(npc.Stealth - sParams.Stealth.Value.target);
			if (sParams.Dark != null) weight += sParams.Dark.Value.weight * (npc.Dark == sParams.Dark.Value.target ? 1 : 0);
			if (sParams.EyeColor != null) weight += sParams.EyeColor.Value.weight * Math.Abs(npc.EyeColor - sParams.EyeColor.Value.target);
			if (sParams.H != null) weight += sParams.H.Value.weight * Math.Abs(npc.H - sParams.H.Value.target);
			if (sParams.S != null) weight += sParams.S.Value.weight * Math.Abs(npc.S - sParams.S.Value.target);
			if (sParams.L != null) weight += sParams.L.Value.weight * Math.Abs(npc.L - sParams.L.Value.target);
			if (sParams.Wideness != null) weight += sParams.Wideness.Value.weight * Math.Abs(npc.Wideness - sParams.Wideness.Value.target);
			return weight;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float SlugcatStatsWeight(SlugcatStats slug)
		{
			float weight = 0f;
			if (sParams.BodyWeightFac != null) weight += sParams.BodyWeightFac.Value.weight * Math.Abs(slug.bodyWeightFac - sParams.BodyWeightFac.Value.target);
			if (sParams.GeneralVisibilityBonus != null) weight += sParams.GeneralVisibilityBonus.Value.weight * Math.Abs(slug.generalVisibilityBonus - sParams.GeneralVisibilityBonus.Value.target);
			if (sParams.VisualStealthInSneakMode != null) weight += sParams.VisualStealthInSneakMode.Value.weight * Math.Abs(slug.visualStealthInSneakMode - sParams.VisualStealthInSneakMode.Value.target);
			if (sParams.LoudnessFac != null )weight += sParams.LoudnessFac.Value.weight * Math.Abs(slug.loudnessFac - sParams.LoudnessFac.Value.target);
			if (sParams.LungsFac != null) weight += sParams.LungsFac.Value.weight * Math.Abs(slug.lungsFac - sParams.LungsFac.Value.target);
			if (sParams.ThrowingSkill != null) weight += sParams.ThrowingSkill.Value.weight * Math.Abs(slug.throwingSkill - sParams.ThrowingSkill.Value.target);
			if (sParams.PoleClimbSpeedFac != null) weight += sParams.PoleClimbSpeedFac.Value.weight * Math.Abs(slug.poleClimbSpeedFac - sParams.PoleClimbSpeedFac.Value.target);
			if (sParams.CorridorClimbSpeedFac != null) weight += sParams.CorridorClimbSpeedFac.Value.weight * Math.Abs(slug.corridorClimbSpeedFac - sParams.CorridorClimbSpeedFac.Value.target);
			if (sParams.RunSpeedFac != null) weight += sParams.RunSpeedFac.Value.weight * Math.Abs(slug.runSpeedFac - sParams.RunSpeedFac.Value.target);
			return weight;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float FoodPreferencesWeight(FoodPreferences foodPref)
		{
			float weight = 0f;
			if (sParams.DangleFruit != null) weight += sParams.DangleFruit.Value.weight * Math.Abs(foodPref.DangleFruit - sParams.DangleFruit.Value.target);
			if (sParams.WaterNut != null) weight += sParams.WaterNut.Value.weight * Math.Abs(foodPref.WaterNut - sParams.WaterNut.Value.target);
			if (sParams.JellyFish != null) weight += sParams.JellyFish.Value.weight * Math.Abs(foodPref.JellyFish - sParams.JellyFish.Value.target);
			if (sParams.SlimeMold != null) weight += sParams.SlimeMold.Value.weight * Math.Abs(foodPref.SlimeMold - sParams.SlimeMold.Value.target);
			if (sParams.EggBugEgg != null) weight += sParams.EggBugEgg.Value.weight * Math.Abs(foodPref.EggBugEgg - sParams.EggBugEgg.Value.target);
			if (sParams.FireEgg != null) weight += sParams.FireEgg.Value.weight * Math.Abs(foodPref.FireEgg - sParams.FireEgg.Value.target);
			if (sParams.Popcorn != null) weight += sParams.Popcorn.Value.weight * Math.Abs(foodPref.Popcorn - sParams.Popcorn.Value.target);
			if (sParams.GooieDuck != null) weight += sParams.GooieDuck.Value.weight * Math.Abs(foodPref.GooieDuck - sParams.GooieDuck.Value.target);
			if (sParams.LilyPuck != null) weight += sParams.LilyPuck.Value.weight * Math.Abs(foodPref.LilyPuck - sParams.LilyPuck.Value.target);
			if (sParams.GlowWeed != null) weight += sParams.GlowWeed.Value.weight * Math.Abs(foodPref.GlowWeed - sParams.GlowWeed.Value.target);
			if (sParams.DandelionPeach != null) weight += sParams.DandelionPeach.Value.weight * Math.Abs(foodPref.DandelionPeach - sParams.DandelionPeach.Value.target);
			if (sParams.Neuron != null) weight += sParams.Neuron.Value.weight * Math.Abs(foodPref.Neuron - sParams.Neuron.Value.target);
			if (sParams.Centipede != null) weight += sParams.Centipede.Value.weight * Math.Abs(foodPref.Centipede - sParams.Centipede.Value.target);
			if (sParams.SmallCentipede != null) weight += sParams.SmallCentipede.Value.weight * Math.Abs(foodPref.SmallCentipede - sParams.SmallCentipede.Value.target);
			if (sParams.VultureGrub != null) weight += sParams.VultureGrub.Value.weight * Math.Abs(foodPref.VultureGrub - sParams.VultureGrub.Value.target);
			if (sParams.SmallNeedleWorm != null) weight += sParams.SmallNeedleWorm.Value.weight * Math.Abs(foodPref.SmallNeedleWorm - sParams.SmallNeedleWorm.Value.target);
			if (sParams.Hazer != null) weight += sParams.Hazer.Value.weight * Math.Abs(foodPref.Hazer - sParams.Hazer.Value.target);
			if (sParams.NotCounted != null) weight += sParams.NotCounted.Value.weight * Math.Abs(foodPref.NotCounted - sParams.NotCounted.Value.target);
			return weight;
		}
		#region Searches
		public static IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int stop, int numToStore, SearchParams searchParams)
		{
			return new Searcher(searchParams).Search(start, stop, numToStore);
		}
		public IEnumerable<KeyValuePair<float, Slugcat>> Search(int start, int count, int numToStore)
		{
			SortedList<float, Slugcat> vals = [];   // smallest value at index 0
			float weight;
			bool saturated = false;
			vals.Capacity = numToStore;

			bool personality, npcStats, slugcatStats, foodPreferences;
			personality = !(sParams.Sympathy is null && sParams.Energy is null && sParams.Bravery is null && sParams.Nervous is null && sParams.Aggression is null && sParams.Dominance is null);
			npcStats = !(sParams.Met is null && sParams.Bal is null && sParams.Size is null && sParams.Stealth is null && sParams.Dark is null && sParams.EyeColor is null && sParams.H is null && sParams.S is null && sParams.L is null && sParams.Wideness is null);
			slugcatStats = !(sParams.BodyWeightFac is null && sParams.GeneralVisibilityBonus is null && sParams.VisualStealthInSneakMode is null && sParams.LoudnessFac is null && sParams.LungsFac is null && sParams.ThrowingSkill is null && sParams.PoleClimbSpeedFac is null && sParams.CorridorClimbSpeedFac is null && sParams.RunSpeedFac is null);
			foodPreferences = !(sParams.DangleFruit is null && sParams.WaterNut is null && sParams.JellyFish is null && sParams.SlimeMold is null && sParams.EggBugEgg is null && sParams.FireEgg is null && sParams.Popcorn is null && sParams.GooieDuck is null && sParams.LilyPuck is null && sParams.GlowWeed is null && sParams.DandelionPeach is null && sParams.Neuron is null && sParams.Centipede is null && sParams.SmallCentipede is null && sParams.VultureGrub is null && sParams.SmallNeedleWorm is null && sParams.Hazer is null && sParams.NotCounted is null);

			Personality? p = null;
			NPCStats? npc = null;
			SlugcatStats? slugStats = null;
			FoodPreferences? foodPref = null;
			for (int i = start; i < start + count; i++)
			{
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
			return vals;
		}

		// Poor idea to use IEnumerable<int> range for large collections. 1 billion int32s is 4GB of memory. Yikes.
		// https://timdeschryver.dev/blog/process-your-list-in-parallel-to-make-it-faster-in-dotnet
		#endregion
	}
}
