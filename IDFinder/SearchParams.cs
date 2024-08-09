namespace IDFinder
{
    #region classes
    // How do I weigh multiple at once? Does this method allow for that? Could I dynamically generate a class that implements these for everything wanted by the user, to use in the searching algorithm? 
    // Some kind of compound search method in the base Search class that takes any number of searchparam class as arguments and calls the weight functions for each?
    // Should the weight functions be made static to facilitate this? Currently SearchParams are a property of the searching class, but a reference could be passed instead.
    public class PersonalityParams : IPersonalityParams
    {
        public (float target, float weight)? Sympathy { get; set; }
        public (float target, float weight)? Energy { get; set; }
        public (float target, float weight)? Bravery { get; set; }
        public (float target, float weight)? Nervous { get; set; }
        public (float target, float weight)? Aggression { get; set; }
        public (float target, float weight)? Dominance { get; set; }
    }
	public class SlugParams : INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams
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
	public class ScavParams : IIndividualVariationsParams, IEartlersParams, IScavColorsParams, IScavSkillsParams, IScavBackPatternsParams
	{
		public bool Elite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ScavParams() => throw new NotImplementedException("Scavenger parameters are not yet implemented.");
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
