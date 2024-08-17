using System.Reflection;

namespace IDFinder
{
	#region classes
	// How do I weigh multiple at once? Does this method allow for that? Could I dynamically generate a class that implements these for everything wanted by the user, to use in the searching algorithm? 
	// Some kind of compound search method in the base Search class that takes any number of searchparam class as arguments and calls the weight functions for each?
	// Should the weight functions be made static to facilitate this? Currently SearchParams are a property of the searching class, but a reference could be passed instead.
	// Tbh I think each interface should have its own class, and then the SlugParams, ScavParams etc have instances of those. It'll play nice with the website GUI
	// I can cast SlugParams down to its individual interfaces, and then pass those individuals to the weighting methods. This means I can take any Set of ISearchParams, iterate through it once to grab the types within it, assign to interface-type variables, and pass those as arguments to static weighting methods.


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
		public bool Elite { get; set; }
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
		public (float target, float weight)? Sympathy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (float target, float weight)? Energy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (float target, float weight)? Bravery { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (float target, float weight)? Nervous { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (float target, float weight)? Aggression { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (float target, float weight)? Dominance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public bool Elite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
		public bool Elite { get; set; }
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
        public bool Elite { get; set; }
    }
    public interface IEartlersParams : ISearchParams
    {
        // Does this even need to be visible? The data itself is quite abstracted from its in-game appearance. It could be searched for some more interesting observations though, like eartler size, complexity, etc. Needs to be thought over and experimented with ingame. 
    }
    public interface IScavColorsParams : ISearchParams
    { 
        
    }
    public interface IScavSkillsParams : ISearchParams
    {

    }
    public interface IScavBackPatternsParams : ISearchParams
    {
        // Remember that this isn't a 1-to-1 recreation of the properties & fields of the respective classes. The information presented needs to be useful to the user, an array of spine sizes is less useful than an averaging of the spine sizes and another for the total number of spines. 
    }
    #endregion
}
