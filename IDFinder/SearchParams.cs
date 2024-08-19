using System.Reflection;
using static IDFinder.BackDecals;
using static IDFinder.BackTuftsAndRidges;

namespace IDFinder
{
	#region classes
	public class SearchParams : ISearchParams, IPersonalityParams, INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams, IIndividualVariationsParams, IEartlersParams, IScavColorsParams, IScavSkillsParams, IScavBackPatternsParams
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
		public (BackDecals target, float weight)? Type { get; set; }
		public (ColorTypeEnum target, float weight)? ColorType { get; set; }
		public (bool target, float weight)? IsColored { get; set; }
		public (int target, float weight)? ScaleGraf { get; set; }
		public (float target, float weight)? GeneralSize { get; set; }
		public (float target, float weight)? Colored { get; set; }
		public (int target, float weight)? NumberOfSpines { get; set; }
	}
	public class SlugParams : IPersonalityParams, INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams
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
	}
	public class ScavParams : IPersonalityParams, IIndividualVariationsParams, IEartlersParams, IScavColorsParams, IScavSkillsParams, IScavBackPatternsParams
	{
		public (float target, float weight)? Sympathy { get; set; }
		public (float target, float weight)? Energy { get; set; }
		public (float target, float weight)? Bravery { get; set; }
		public (float target, float weight)? Nervous { get; set; }
		public (float target, float weight)? Aggression { get; set; }
		public (float target, float weight)? Dominance { get; set; }
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
		public (BackDecals target, float weight)? Type { get; set; }
		public (ColorTypeEnum target, float weight)? ColorType { get; set; }
		public (bool target, float weight)? IsColored { get; set; }
		public (int target, float weight)? ScaleGraf { get; set; }
		public (float target, float weight)? GeneralSize { get; set; }
		public (float target, float weight)? Colored { get; set; }
		public (int target, float weight)? NumberOfSpines { get; set; }

		public ScavParams() => throw new NotImplementedException("Scavenger parameters are not yet implemented.");
	}
	public class NewSlugParams()
	{
		public PersonalityParams  PersonalityParams { get; set; } = new();
		public NPCStatsParams NPCStatsParams { get; set; } = new();
		public SlugcatStatsParams SlugcatStatsParams { get; set; } = new();
		public FoodPreferencesParams FoodPreferencesParams{ get; set; } = new();
	}
	
	public class PersonalityParams : ISearchParams
	{
		public (float target, float weight)? Sympathy { get; set; }
		public (float target, float weight)? Energy { get; set; }
		public (float target, float weight)? Bravery { get; set; }
		public (float target, float weight)? Nervous { get; set; }
		public (float target, float weight)? Aggression { get; set; }
		public (float target, float weight)? Dominance { get; set; }
	}
	public class NPCStatsParams : ISearchParams
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
	}
	public class SlugcatStatsParams : ISearchParams
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
	}
	public class FoodPreferencesParams : ISearchParams
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
	}

	public class IndividualVariationsParams : ISearchParams
	{
	}
	public class EartlersParams : ISearchParams
	{
		// Does this even need to be visible? The data itself is quite abstracted from its in-game appearance. It could be searched for some more interesting observations though, like eartler size, complexity, etc. Needs to be thought over and experimented with ingame. 
	}
	public class ScavColorsParams : ISearchParams
	{

	}
	public class ScavSkillsParams : ISearchParams
	{

	}
	public class ScavBackPatternsParams : ISearchParams
	{
		// Remember that this isn't a 1-to-1 recreation of the properties & fields of the respective classes. The information presented needs to be useful to the user, an array of spine sizes is less useful than an averaging of the spine sizes and another for the total number of spines. 
	}

	#endregion
	#region interfaces
	public interface ISearchParams
	{
		public bool AllNull()
		{
			foreach (PropertyInfo pi in this.GetType().GetProperties())
				if (pi.GetValue(this) is not null)
					return false;

			return true;
		}
	}
    public interface IPersonalityParams : ISearchParams
    {
        public (float target, float weight)? Sympathy { get; set; }
        public (float target, float weight)? Energy { get; set; }
        public (float target, float weight)? Bravery { get; set; }
        public (float target, float weight)? Nervous { get; set; }
        public (float target, float weight)? Aggression { get; set; }
        public (float target, float weight)? Dominance { get; set; }
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
    }

    public interface IIndividualVariationsParams : ISearchParams
    {
		// Elites not here as it's a parameter in the Scavenger constructor, and influences other traits. Like slugcats and scavs are different, scavs and elite scavs are also different.
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
	}
    public interface IScavSkillsParams : ISearchParams
    {
		public (float target, float weight)? BlockingSkill { get; set; }
		public (float target, float weight)? DodgeSkill { get; set; }
		public (float target, float weight)? MeleeSkill { get; set; }
		public (float target, float weight)? MidRangeSkill { get; set; }
		public (float target, float weight)? ReactionSkill { get; set; }
	}
    public interface IScavBackPatternsParams : ISearchParams
    {
		public (float target, float weight)? Top { get; set; }
		public (float target, float weight)? Bottom { get; set; }
		public (BackPattern target, float weight)? Pattern { get; set; }
		public (BackDecals target, float weight)? Type { get; set; }
		public (ColorTypeEnum target, float weight)? ColorType { get; set; }
		public (bool target, float weight)? IsColored { get; set; }
		public (int target, float weight)? ScaleGraf { get; set; }
		public (float target, float weight)? GeneralSize { get; set; }
		public (float target, float weight)? Colored { get; set; }
		public (int target, float weight)? NumberOfSpines { get; set; }
	}
    #endregion
}
