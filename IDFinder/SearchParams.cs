using System.Reflection;
using static IDFinder.BackDecals;
using static IDFinder.BackTuftsAndRidges;

namespace IDFinder
{
	#region classes
	// Add implicit casts to SearchParams from SlugParams and ScavParams. That way the latter can be passed as a parameter without breaking anything in Search.cs
	public class SearchParams : ISearchParams, IPersonalityParams, INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams, IIndividualVariationsParams, IEartlersParams, IScavColorsParams, IScavSkillsParams, IScavBackPatternsParams	 // H, S, L should be more clear that it's slugcat npcstats. Likewise for some others. So long as everything has unique names no bugs should arise, but I wouldn't recommend directly assigning properties in this class, rather use SlugParams, ScavParams etc once I implement operators. 
	{
		public SearchParams Clone() => (SearchParams)this.MemberwiseClone();
		public (float target, float weight)? Sympathy { get; set; }
		public (float target, float weight)? Energy { get; set; }
		public (float target, float weight)? Bravery { get; set; }
		public (float target, float weight)? Nervous { get; set; }
		public (float target, float weight)? Aggression { get; set; }
		public (float target, float weight)? Dominance { get; set; }
		public (float target, float weight)? Met { get; set; }
		public (float target, float weight)? Bal { get; set; }
		public (float target, float weight)? Size { get; set; }
		public (float target, float weight)? Stealth { get; set; }
		public (bool target, float weight)? Dark { get; set; }
		public (float target, float weight)? EyeColor { get; set; }
		public (float target, float weight)? H { get; set; }
		public (float target, float weight)? S { get; set; }
		public (float target, float weight)? L { get; set; }
		public (float target, float weight)? Wideness { get; set; }
		public (float target, float weight)? BodyWeightFac { get; set; }
		public (float target, float weight)? GeneralVisibilityBonus { get; set; }
		public (float target, float weight)? VisualStealthInSneakMode { get; set; }
		public (float target, float weight)? LoudnessFac { get; set; }
		public (float target, float weight)? LungsFac { get; set; }
		public (int target, float weight)? ThrowingSkill { get; set; }
		public (float target, float weight)? PoleClimbSpeedFac { get; set; }
		public (float target, float weight)? CorridorClimbSpeedFac { get; set; }
		public (float target, float weight)? RunSpeedFac { get; set; }
		public (float target, float weight)? DangleFruit { get; set; }
		public (float target, float weight)? WaterNut { get; set; }
		public (float target, float weight)? JellyFish { get; set; }
		public (float target, float weight)? SlimeMold { get; set; }
		public (float target, float weight)? EggBugEgg { get; set; }
		public (float target, float weight)? FireEgg { get; set; }
		public (float target, float weight)? Popcorn { get; set; }
		public (float target, float weight)? GooieDuck { get; set; }
		public (float target, float weight)? LilyPuck { get; set; }
		public (float target, float weight)? GlowWeed { get; set; }
		public (float target, float weight)? DandelionPeach { get; set; }
		public (float target, float weight)? Neuron { get; set; }
		public (float target, float weight)? Centipede { get; set; }
		public (float target, float weight)? SmallCentipede { get; set; }
		public (float target, float weight)? VultureGrub { get; set; }
		public (float target, float weight)? SmallNeedleWorm { get; set; }
		public (float target, float weight)? Hazer { get; set; }
		public (float target, float weight)? NotCounted { get; set; }
		public bool Elite { get; set; } = false;
		public (float target, float weight)? WaistWidth { get; set; }
		public (float target, float weight)? HeadSize { get; set; }
		public (float target, float weight)? EartlerWidth { get; set; }
		public (float target, float weight)? NeckThickness { get; set; }
		public (float target, float weight)? HandsHeadColor { get; set; }
		public (float target, float weight)? EyeSize { get; set; }
		public (float target, float weight)? NarrowEyes { get; set; }
		public (float target, float weight)? EyesAngle { get; set; }
		public (float target, float weight)? Fatness { get; set; }
		public (float target, float weight)? NarrowWaist { get; set; }
		public (float target, float weight)? LegsSize { get; set; }
		public (float target, float weight)? ArmThickness { get; set; }
		public (float target, float weight)? WideTeeth { get; set; }
		public (float target, float weight)? PupilSize { get; set; }
		public (float target, float weight)? Scruffy { get; set; }
		public (bool target, float weight)? ColoredEartlerTips { get; set; }
		public (bool target, float weight)? DeepPupils { get; set; }
		public (int target, float weight)? ColoredPupils { get; set; }
		public (int target, float weight)? TailSegs { get; set; }
		public (float target, float weight)? GeneralMelanin { get; set; }
		public (float target, float weight)? BellyColorH { get; set; }
		public (float target, float weight)? BellyColorS { get; set; }
		public (float target, float weight)? BellyColorL { get; set; }
		public (float target, float weight)? BodyColorH { get; set; }
		public (float target, float weight)? BodyColorS { get; set; }
		public (float target, float weight)? BodyColorL { get; set; }
		public (float target, float weight)? DecorationColorH { get; set; }
		public (float target, float weight)? DecorationColorS { get; set; }
		public (float target, float weight)? DecorationColorL { get; set; }
		public (float target, float weight)? EyeColorH { get; set; }
		public (float target, float weight)? EyeColorL { get; set; }
		public (float target, float weight)? HeadColorH { get; set; }
		public (float target, float weight)? HeadColorS { get; set; }
		public (float target, float weight)? HeadColorL { get; set; }
		public (float target, float weight)? BellyColorBlack { get; set; }
		public (float target, float weight)? BodyColorBlack { get; set; }
		public (float target, float weight)? HeadColorBlack { get; set; }
		public (float target, float weight)? BlockingSkill { get; set; }
		public (float target, float weight)? DodgeSkill { get; set; }
		public (float target, float weight)? MeleeSkill { get; set; }
		public (float target, float weight)? MidRangeSkill { get; set; }
		public (float target, float weight)? ReactionSkill { get; set; }
		public (float target, float weight)? Top { get; set; }
		public (float target, float weight)? Bottom { get; set; }
		public (BackPattern target, float weight)? Pattern { get; set; }
		public (string target, float weight)? Type { get; set; }
		public (ColorTypeEnum target, float weight)? ColorType { get; set; }
		public (bool target, float weight)? IsColored { get; set; }
		public (int target, float weight)? ScaleGraf { get; set; }
		public (float target, float weight)? GeneralSize { get; set; }
		public (float target, float weight)? Colored { get; set; }
		public (int target, float weight)? NumberOfSpines { get; set; }
	}
	public class SlugParams : ISearchParams, IPersonalityParams, INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams
	{
		public (float target, float weight)? Sympathy { get; set; }
		public (float target, float weight)? Energy { get; set; }
		public (float target, float weight)? Bravery { get; set; }
		public (float target, float weight)? Nervous { get; set; }
		public (float target, float weight)? Aggression { get; set; }
		public (float target, float weight)? Dominance { get; set; }
		public (float target, float weight)? Met { get; set; }
		public (float target, float weight)? Bal { get; set; }
		public (float target, float weight)? Size { get; set; }
		public (float target, float weight)? Stealth { get; set; }
		public (bool target, float weight)? Dark { get; set; }
		public (float target, float weight)? EyeColor { get; set; }
		public (float target, float weight)? H { get; set; }
		public (float target, float weight)? S { get; set; }
		public (float target, float weight)? L { get; set; }
		public (float target, float weight)? Wideness { get; set; }
		public (float target, float weight)? BodyWeightFac { get; set; }
		public (float target, float weight)? GeneralVisibilityBonus { get; set; }
		public (float target, float weight)? VisualStealthInSneakMode { get; set; }
		public (float target, float weight)? LoudnessFac { get; set; }
		public (float target, float weight)? LungsFac { get; set; }
		public (int target, float weight)? ThrowingSkill { get; set; }
		public (float target, float weight)? PoleClimbSpeedFac { get; set; }
		public (float target, float weight)? CorridorClimbSpeedFac { get; set; }
		public (float target, float weight)? RunSpeedFac { get; set; }
		public (float target, float weight)? DangleFruit { get; set; }
		public (float target, float weight)? WaterNut { get; set; }
		public (float target, float weight)? JellyFish { get; set; }
		public (float target, float weight)? SlimeMold { get; set; }
		public (float target, float weight)? EggBugEgg { get; set; }
		public (float target, float weight)? FireEgg { get; set; }
		public (float target, float weight)? Popcorn { get; set; }
		public (float target, float weight)? GooieDuck { get; set; }
		public (float target, float weight)? LilyPuck { get; set; }
		public (float target, float weight)? GlowWeed { get; set; }
		public (float target, float weight)? DandelionPeach { get; set; }
		public (float target, float weight)? Neuron { get; set; }
		public (float target, float weight)? Centipede { get; set; }
		public (float target, float weight)? SmallCentipede { get; set; }
		public (float target, float weight)? VultureGrub { get; set; }
		public (float target, float weight)? SmallNeedleWorm { get; set; }
		public (float target, float weight)? Hazer { get; set; }
		public (float target, float weight)? NotCounted { get; set; }
		public static implicit operator SearchParams(SlugParams slug)
		{
			return new()
			{
				Sympathy = slug.Sympathy,
				Energy = slug.Energy,
				Bravery = slug.Bravery,
				Nervous = slug.Nervous,
				Aggression = slug.Aggression,
				Dominance = slug.Dominance,
				Met = slug.Met,
				Bal = slug.Bal,
				Size = slug.Size,
				Stealth = slug.Stealth,
				Dark = slug.Dark,
				EyeColor = slug.EyeColor,
				H = slug.H,
				S = slug.S,
				L = slug.L,
				Wideness = slug.Wideness,
				BodyWeightFac = slug.BodyWeightFac,
				GeneralVisibilityBonus = slug.GeneralVisibilityBonus,
				VisualStealthInSneakMode = slug.VisualStealthInSneakMode,
				LoudnessFac = slug.LoudnessFac,
				LungsFac = slug.LungsFac,
				ThrowingSkill = slug.ThrowingSkill,
				PoleClimbSpeedFac = slug.PoleClimbSpeedFac,
				CorridorClimbSpeedFac = slug.CorridorClimbSpeedFac,
				RunSpeedFac = slug.RunSpeedFac,
				DangleFruit = slug.DangleFruit,
				WaterNut = slug.WaterNut,
				JellyFish = slug.JellyFish,
				SlimeMold = slug.SlimeMold,
				EggBugEgg = slug.EggBugEgg,
				FireEgg = slug.FireEgg,
				Popcorn = slug.Popcorn,
				GooieDuck = slug.GooieDuck,
				LilyPuck = slug.LilyPuck,
				GlowWeed = slug.GlowWeed,
				DandelionPeach = slug.DandelionPeach,
				Neuron = slug.Neuron,
				Centipede = slug.Centipede,
				SmallCentipede = slug.SmallCentipede,
				VultureGrub = slug.VultureGrub,
				SmallNeedleWorm = slug.SmallNeedleWorm,
				Hazer = slug.Hazer,
				NotCounted = slug.NotCounted
			};
		}
	}
	public class ScavParams : ISearchParams, IPersonalityParams, IIndividualVariationsParams, IEartlersParams, IScavColorsParams, IScavSkillsParams, IScavBackPatternsParams
	{
		public (float target, float weight)? Sympathy { get; set; }
		public (float target, float weight)? Energy { get; set; }
		public (float target, float weight)? Bravery { get; set; }
		public (float target, float weight)? Nervous { get; set; }
		public (float target, float weight)? Aggression { get; set; }
		public (float target, float weight)? Dominance { get; set; }
		public bool Elite { get; set; } = false;
		public (float target, float weight)? WaistWidth { get; set; }
		public (float target, float weight)? HeadSize { get; set; }
		public (float target, float weight)? EartlerWidth { get; set; }
		public (float target, float weight)? NeckThickness { get; set; }
		public (float target, float weight)? HandsHeadColor { get; set; }
		public (float target, float weight)? EyeSize { get; set; }
		public (float target, float weight)? NarrowEyes { get; set; }
		public (float target, float weight)? EyesAngle { get; set; }
		public (float target, float weight)? Fatness { get; set; }
		public (float target, float weight)? NarrowWaist { get; set; }
		public (float target, float weight)? LegsSize { get; set; }
		public (float target, float weight)? ArmThickness { get; set; }
		public (float target, float weight)? WideTeeth { get; set; }
		public (float target, float weight)? PupilSize { get; set; }
		public (float target, float weight)? Scruffy { get; set; }
		public (bool target, float weight)? ColoredEartlerTips { get; set; }
		public (bool target, float weight)? DeepPupils { get; set; }
		public (int target, float weight)? ColoredPupils { get; set; }
		public (int target, float weight)? TailSegs { get; set; }
		public (float target, float weight)? GeneralMelanin { get; set; }
		public (float target, float weight)? BellyColorH { get; set; }
		public (float target, float weight)? BellyColorS { get; set; }
		public (float target, float weight)? BellyColorL { get; set; }
		public (float target, float weight)? BodyColorH { get; set; }
		public (float target, float weight)? BodyColorS { get; set; }
		public (float target, float weight)? BodyColorL { get; set; }
		public (float target, float weight)? DecorationColorH { get; set; }
		public (float target, float weight)? DecorationColorS { get; set; }
		public (float target, float weight)? DecorationColorL { get; set; }
		public (float target, float weight)? EyeColorH { get; set; }
		public (float target, float weight)? EyeColorL { get; set; }
		public (float target, float weight)? HeadColorH { get; set; }
		public (float target, float weight)? HeadColorS { get; set; }
		public (float target, float weight)? HeadColorL { get; set; }
		public (float target, float weight)? BellyColorBlack { get; set; }
		public (float target, float weight)? BodyColorBlack { get; set; }
		public (float target, float weight)? HeadColorBlack { get; set; }
		public (float target, float weight)? BlockingSkill { get; set; }
		public (float target, float weight)? DodgeSkill { get; set; }
		public (float target, float weight)? MeleeSkill { get; set; }
		public (float target, float weight)? MidRangeSkill { get; set; }
		public (float target, float weight)? ReactionSkill { get; set; }
		public (float target, float weight)? Top { get; set; }
		public (float target, float weight)? Bottom { get; set; }
		public (BackPattern target, float weight)? Pattern { get; set; }
		public (string target, float weight)? Type { get; set; }
		public (ColorTypeEnum target, float weight)? ColorType { get; set; }
		public (bool target, float weight)? IsColored { get; set; }
		public (int target, float weight)? ScaleGraf { get; set; }
		public (float target, float weight)? GeneralSize { get; set; }
		public (float target, float weight)? Colored { get; set; }
		public (int target, float weight)? NumberOfSpines { get; set; }
		public static implicit operator SearchParams(ScavParams scav)
		{
			return new()
			{
				Sympathy = scav.Sympathy,
				Energy = scav.Energy,
				Bravery = scav.Bravery,
				Nervous = scav.Nervous,
				Aggression = scav.Aggression,
				Dominance = scav.Dominance,
				Elite = scav.Elite,
				WaistWidth = scav.WaistWidth,
				HeadSize = scav.HeadSize,
				EartlerWidth = scav.EartlerWidth,
				NeckThickness = scav.NeckThickness,
				HandsHeadColor = scav.HandsHeadColor,
				EyeSize = scav.EyeSize,
				NarrowEyes = scav.NarrowEyes,
				EyesAngle = scav.EyesAngle,
				Fatness = scav.Fatness,
				NarrowWaist = scav.NarrowWaist,
				LegsSize = scav.LegsSize,
				ArmThickness = scav.ArmThickness,
				WideTeeth = scav.WideTeeth,
				PupilSize = scav.PupilSize,
				Scruffy = scav.Scruffy,
				ColoredEartlerTips = scav.ColoredEartlerTips,
				DeepPupils = scav.DeepPupils,
				ColoredPupils = scav.ColoredPupils,
				TailSegs = scav.TailSegs,
				GeneralMelanin = scav.GeneralMelanin,
				BellyColorH = scav.BellyColorH,
				BellyColorS = scav.BellyColorS,
				BellyColorL = scav.BellyColorL,
				BodyColorH = scav.BodyColorH,
				BodyColorS = scav.BodyColorS,
				BodyColorL = scav.BodyColorL,
				DecorationColorH = scav.DecorationColorH,
				DecorationColorS = scav.DecorationColorS,
				DecorationColorL = scav.DecorationColorL,
				EyeColorH = scav.EyeColorH,
				EyeColorL = scav.EyeColorL,
				HeadColorH = scav.HeadColorH,
				HeadColorS = scav.HeadColorS,
				HeadColorL = scav.HeadColorL,
				BellyColorBlack = scav.BellyColorBlack,
				BodyColorBlack = scav.BodyColorBlack,
				HeadColorBlack = scav.HeadColorBlack,
				BlockingSkill = scav.BlockingSkill,
				DodgeSkill = scav.DodgeSkill,
				MeleeSkill = scav.MeleeSkill,
				MidRangeSkill = scav.MidRangeSkill,
				ReactionSkill = scav.ReactionSkill,
				Top = scav.Top,
				Bottom = scav.Bottom,
				Pattern = scav.Pattern,
				Type = scav.Type,
				ColorType = scav.ColorType,
				IsColored = scav.IsColored,
				ScaleGraf = scav.ScaleGraf,
				GeneralSize = scav.GeneralSize,
				Colored = scav.Colored,
				NumberOfSpines = scav.NumberOfSpines
			};
		}
	}
	#endregion
	#region interfaces
	public interface ISearchParams { }
    public interface IPersonalityParams : ISearchParams
    {
        public (float target, float weight)? Sympathy { get; set; }
        public (float target, float weight)? Energy { get; set; }
        public (float target, float weight)? Bravery { get; set; }
        public (float target, float weight)? Nervous { get; set; }
        public (float target, float weight)? Aggression { get; set; }
        public (float target, float weight)? Dominance { get; set; }
		public bool AllNull()
		{
			if (Sympathy is null &&
				Energy is null &&
				Bravery is null &&
				Nervous is null &&
				Aggression is null &&
				Dominance is null)
				return true;

			return false;
		}
    }
    public interface INPCStatsParams : ISearchParams
    {
        public (float target, float weight)? Met { get; set; }
        public (float target, float weight)? Bal { get; set; }
        public (float target, float weight)? Size { get; set; }
        public (float target, float weight)? Stealth { get; set; }
        public (bool target, float weight)? Dark { get; set; }
        public (float target, float weight)? EyeColor { get; set; }
        public (float target, float weight)? H { get; set; }
        public (float target, float weight)? S { get; set; }
        public (float target, float weight)? L { get; set; }
        public (float target, float weight)? Wideness { get; set; }
		public bool AllNull()
		{
			if (Met is null &&
				Bal is null &&
				Size is null &&
				Stealth is null &&
				Dark is null &&
				EyeColor is null &&
				H is null &&
				S is null &&
				L is null &&
				Wideness is null)
				return true;

			return false;
		}
    }
    public interface ISlugcatStatsParams : ISearchParams
    {
        public (float target, float weight)? BodyWeightFac { get; set; }
        public (float target, float weight)? GeneralVisibilityBonus { get; set; }
        public (float target, float weight)? VisualStealthInSneakMode { get; set; }
        public (float target, float weight)? LoudnessFac { get; set; }
        public (float target, float weight)? LungsFac { get; set; }
        public (int target, float weight)? ThrowingSkill { get; set; }
        public (float target, float weight)? PoleClimbSpeedFac { get; set; }
        public (float target, float weight)? CorridorClimbSpeedFac { get; set; }
        public (float target, float weight)? RunSpeedFac { get; set; }
		public bool AllNull()
		{
			if (BodyWeightFac is null &&
				GeneralVisibilityBonus is null &&
				VisualStealthInSneakMode is null &&
				LoudnessFac is null &&
				LungsFac is null &&
				ThrowingSkill is null &&
				PoleClimbSpeedFac is null &&
				CorridorClimbSpeedFac is null &&
				RunSpeedFac is null)
				return true;

			return false;
		}
    }
    public interface IFoodPreferencesParams : ISearchParams
    {
        public (float target, float weight)? DangleFruit { get; set; }
        public (float target, float weight)? WaterNut { get; set; }
        public (float target, float weight)? JellyFish { get; set; }
        public (float target, float weight)? SlimeMold { get; set; }
        public (float target, float weight)? EggBugEgg { get; set; }
        public (float target, float weight)? FireEgg { get; set; }
        public (float target, float weight)? Popcorn { get; set; }
        public (float target, float weight)? GooieDuck { get; set; }
        public (float target, float weight)? LilyPuck { get; set; }
        public (float target, float weight)? GlowWeed { get; set; }
        public (float target, float weight)? DandelionPeach { get; set; }
        public (float target, float weight)? Neuron { get; set; }
        public (float target, float weight)? Centipede { get; set; }
        public (float target, float weight)? SmallCentipede { get; set; }
        public (float target, float weight)? VultureGrub { get; set; }
        public (float target, float weight)? SmallNeedleWorm { get; set; }
        public (float target, float weight)? Hazer { get; set; }
        public (float target, float weight)? NotCounted { get; set; }
		public bool AllNull()
		{
			if (DangleFruit is null &&
				WaterNut is null &&
				JellyFish is null &&
				SlimeMold is null &&
				EggBugEgg is null &&
				FireEgg is null &&
				Popcorn  is null &&
				GooieDuck is null &&
				LilyPuck is null &&
				GlowWeed is null &&
				DandelionPeach  is null &&
				Neuron is null &&
				Centipede is null &&
				SmallCentipede is null &&
				VultureGrub is null &&
				SmallNeedleWorm is null &&
				Hazer is null &&
				NotCounted is null)
				return true;

			return false;
		}
    }

    public interface IIndividualVariationsParams : ISearchParams
    {
		// Elites not here as it's a parameter in the Scavenger constructor, and influences other traits. Like slugcats and scavs are different, scavs and elite scavs are also different.
		public bool Elite { get; set; }
		public (float target, float weight)? WaistWidth { get; set; }
		public (float target, float weight)? HeadSize { get; set; }
		public (float target, float weight)? EartlerWidth { get; set; }
		public (float target, float weight)? NeckThickness { get; set; }
		public (float target, float weight)? HandsHeadColor { get; set; }
		public (float target, float weight)? EyeSize { get; set; }
		public (float target, float weight)? NarrowEyes { get; set; }
		public (float target, float weight)? EyesAngle { get; set; }
		public (float target, float weight)? Fatness { get; set; }
		public (float target, float weight)? NarrowWaist { get; set; }
		public (float target, float weight)? LegsSize { get; set; }
		public (float target, float weight)? ArmThickness { get; set; }
		public (float target, float weight)? WideTeeth { get; set; }
		public (float target, float weight)? PupilSize { get; set; }
		public (float target, float weight)? Scruffy { get; set; }
		public (bool target, float weight)? ColoredEartlerTips { get; set; }
		public (bool target, float weight)? DeepPupils { get; set; }
		public (int target, float weight)? ColoredPupils { get; set; }
		public (int target, float weight)? TailSegs { get; set; }
		public (float target, float weight)? GeneralMelanin { get; set; }
		public bool AllNull()
		{
			if (WaistWidth is null &&
				HeadSize is null &&
				EartlerWidth is null &&
				NeckThickness is null &&
				HandsHeadColor is null &&
				EyeSize is null &&
				NarrowEyes is null &&
				EyesAngle is null &&
				Fatness is null &&
				NarrowWaist is null &&
				LegsSize is null &&
				ArmThickness is null &&
				WideTeeth  is null &&
				PupilSize  is null &&
				Scruffy is null &&
				ColoredEartlerTips is null &&
				DeepPupils is null &&
				ColoredPupils is null &&
				TailSegs is null &&
				GeneralMelanin is null)
				return true;

			return false;
		}
	}
    public interface IEartlersParams : ISearchParams
    {
        // Does this even need to be visible? The data itself is quite abstracted from its in-game appearance. It could be searched for some more interesting observations though, like eartler size, complexity, etc. Needs to be thought over and experimented with ingame. 
    }
    public interface IScavColorsParams : ISearchParams
    {
		// I think this is better than the alternative of HSLColor structs, because it'd be convoluted figuring out which color channels to check. 
		public (float target, float weight)? BellyColorH { get; set; }
		public (float target, float weight)? BellyColorS { get; set; }
		public (float target, float weight)? BellyColorL { get; set; }
		public (float target, float weight)? BodyColorH { get; set; }
		public (float target, float weight)? BodyColorS { get; set; }
		public (float target, float weight)? BodyColorL { get; set; }
		public (float target, float weight)? DecorationColorH { get; set; }
		public (float target, float weight)? DecorationColorS { get; set; }
		public (float target, float weight)? DecorationColorL { get; set; }
		public (float target, float weight)? EyeColorH { get; set; }
		public (float target, float weight)? EyeColorL { get; set; }
		public (float target, float weight)? HeadColorH { get; set; }
		public (float target, float weight)? HeadColorS { get; set; }
		public (float target, float weight)? HeadColorL { get; set; }
		public (float target, float weight)? BellyColorBlack { get; set; }
		public (float target, float weight)? BodyColorBlack { get; set; }
		public (float target, float weight)? HeadColorBlack { get; set; }
		public bool AllNull()
		{
			if (BellyColorH is null &&
				BellyColorS is null &&
				BellyColorL is null &&
				BodyColorH is null &&
				BodyColorS is null &&
				BodyColorL is null &&
				DecorationColorH is null &&
				DecorationColorS is null &&
				DecorationColorL is null &&
				EyeColorH is null &&
				EyeColorL is null &&
				HeadColorH is null &&
				HeadColorS is null &&
				HeadColorL is null &&
				BellyColorBlack is null &&
				BodyColorBlack is null &&
				HeadColorBlack is null)
				return true;

			return false;
		}
	}
    public interface IScavSkillsParams : ISearchParams
    {
		public (float target, float weight)? BlockingSkill { get; set; }
		public (float target, float weight)? DodgeSkill { get; set; }
		public (float target, float weight)? MeleeSkill { get; set; }
		public (float target, float weight)? MidRangeSkill { get; set; }
		public (float target, float weight)? ReactionSkill { get; set; }
		public bool AllNull()
		{
			if (BlockingSkill is null &&
				DodgeSkill is null &&
				MeleeSkill is null &&
				MidRangeSkill is null &&
				ReactionSkill is null)
				return true;

			return false;
		}
	}
    public interface IScavBackPatternsParams : ISearchParams
    {
		public (float target, float weight)? Top { get; set; }
		public (float target, float weight)? Bottom { get; set; }
		public (BackPattern target, float weight)? Pattern { get; set; }
		public (string target, float weight)? Type { get; set; }	// Target would be BackDecals not string, but this circumvents calling ToString() constantly to compare it.
		public (ColorTypeEnum target, float weight)? ColorType { get; set; }
		public (bool target, float weight)? IsColored { get; set; }
		public (int target, float weight)? ScaleGraf { get; set; }	// Necessary?
		public (float target, float weight)? GeneralSize { get; set; }
		public (float target, float weight)? Colored { get; set; }
		public (int target, float weight)? NumberOfSpines { get; set; }
		public bool AllNull()
		{
			if (Top is null &&
				Bottom is null &&
				Pattern is null &&
				Type is null &&
				ColorType is null &&
				IsColored is null &&
				ScaleGraf is null &&
				GeneralSize is null &&
				Colored is null &&
				NumberOfSpines is null)
				return true;

			return false;
		}
	}
    #endregion
}
