using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
    public interface ISearchGroup
    {
        // Array of these passed to the searching function. Iterate through these, switch across their types and determine value of bools used to determine which weighting functions are called in each iteration. Each SearchGroup should match the parameters and fields of their respective classes. Each weighting function will need to be updated to accept their respective SearchGroup class.
        // Is an interface more appropriate?
        // I need to consider this architecture. How do I get this information to the searcher? How do I, once I've determined which search groups are given, pass those search groups to their respective functions? Do I use a dictionary? I could use an enum for example. This needs to be expandable. SearchParams isn't, but neither is this without considerable thought.
        // Something like Dictionary<SearchGroups enum, SearchGroup object> and each if() statement that calls a weighting function passes the value for its corresponding key. if bool personality = true, Dictionary[SearchGroups.personality] gets passed to PersonalityWeight() alongside the actual Personality object with its property values. This may be the cleanest way to do so.
    }
    public class PersonalityGroup : ISearchGroup
    {
        public (float target, float weight)? Sympathy = null;
        public (float target, float weight)? Energy = null;
        public (float target, float weight)? Bravery = null;
        public (float target, float weight)? Nervous = null;
        public (float target, float weight)? Aggression = null;
        public (float target, float weight)? Dominance = null;
    }
    public class NPCStatsGroup : ISearchGroup
    {
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
    }
    public class SlugcatStatsGroup : ISearchGroup
    {
        public (float target, float weight)? BodyWeightFac = null;
        public (float target, float weight)? GeneralVisibilityBonus = null;
        public (float target, float weight)? VisualStealthInSneakMode = null;
        public (float target, float weight)? LoudnessFac = null;
        public (float target, float weight)? LungsFac = null;
        public (int target, float weight)? ThrowingSkill = null;
        public (float target, float weight)? PoleClimbSpeedFac = null;
        public (float target, float weight)? CorridorClimbSpeedFac = null;
        public (float target, float weight)? RunSpeedFac = null;
    }
    public class FoodPreferencesGroup : ISearchGroup
    {
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
    }

    public class IndividualVariationsGroup : ISearchGroup
    {
        
    }
    public class EartlersGroup : ISearchGroup
    {
        // Does this even need to be visible? The data itself is quite abstracted from its in-game appearance. It could be searched for some more interesting observations though, like eartler size, complexity, etc. Needs to be thought over and experimented with ingame. 
    }
    public class ScavColorsGroup : ISearchGroup
    { 
        
    }
    public class ScavSkillsGroup : ISearchGroup
    {

    }
    public class ScavBackPatternsGroup : ISearchGroup
    {
        // Remember that this isn't a 1-to-1 recreation of the properties & fields of the respective classes. The information presented needs to be useful to the user, an array of spine sizes is less useful than an averaging of the spine sizes and another for the total number of spines. 
    }
}
