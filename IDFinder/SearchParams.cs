using System.Reflection;
using static IDFinder.BackDecals;
using static IDFinder.BackTuftsAndRidges;

namespace IDFinder
{
	#region classes
	public class SearchParams : ISearchParams, IPersonalityParams, INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams, IIndividualVariationsParams, IEartlersParams, IScavColorsParams, IScavSkillsParams, IScavBackPatternsParams     // H, S, L should be more clear that it's slugcat npcstats. Likewise for some others. So long as everything has unique names no bugs should arise, but I wouldn't recommend directly assigning properties in this class, rather use SlugParams, ScavParams.
																																																	{
		#region Properties
		public (float target, float weight) Sympathy { get; set; }
		public (float target, float weight) Energy { get; set; }
		public (float target, float weight) Bravery { get; set; }
		public (float target, float weight) Nervous { get; set; }
		public (float target, float weight) Aggression { get; set; }
		public (float target, float weight) Dominance { get; set; }
		public (float target, float weight) Met { get; set; }
		public (float target, float weight) Bal { get; set; }
		public (float target, float weight) Size { get; set; }
		public (float target, float weight) Stealth { get; set; }
		public (bool target, float weight) Dark { get; set; }
		public (float target, float weight) EyeColor { get; set; }
		public (float target, float weight) H { get; set; }
		public (float target, float weight) S { get; set; }
		public (float target, float weight) L { get; set; }
		public (float target, float weight) Wideness { get; set; }
		public (float target, float weight) BodyWeightFac { get; set; }
		public (float target, float weight) GeneralVisibilityBonus { get; set; }
		public (float target, float weight) VisualStealthInSneakMode { get; set; }
		public (float target, float weight) LoudnessFac { get; set; }
		public (float target, float weight) LungsFac { get; set; }
		public (int target, float weight) ThrowingSkill { get; set; }
		public (float target, float weight) PoleClimbSpeedFac { get; set; }
		public (float target, float weight) CorridorClimbSpeedFac { get; set; }
		public (float target, float weight) RunSpeedFac { get; set; }
		public (float target, float weight) DangleFruit { get; set; }
		public (float target, float weight) WaterNut { get; set; }
		public (float target, float weight) JellyFish { get; set; }
		public (float target, float weight) SlimeMold { get; set; }
		public (float target, float weight) EggBugEgg { get; set; }
		public (float target, float weight) FireEgg { get; set; }
		public (float target, float weight) Popcorn { get; set; }
		public (float target, float weight) GooieDuck { get; set; }
		public (float target, float weight) LilyPuck { get; set; }
		public (float target, float weight) GlowWeed { get; set; }
		public (float target, float weight) DandelionPeach { get; set; }
		public (float target, float weight) Neuron { get; set; }
		public (float target, float weight) Centipede { get; set; }
		public (float target, float weight) SmallCentipede { get; set; }
		public (float target, float weight) VultureGrub { get; set; }
		public (float target, float weight) SmallNeedleWorm { get; set; }
		public (float target, float weight) Hazer { get; set; }
		public (float target, float weight) NotCounted { get; set; }
		public bool Elite { get; set; } = false;
		public (float target, float weight) WaistWidth { get; set; }
		public (float target, float weight) HeadSize { get; set; }
		public (float target, float weight) EartlerWidth { get; set; }
		public (float target, float weight) NeckThickness { get; set; }
		public (float target, float weight) HandsHeadColor { get; set; }
		public (float target, float weight) EyeSize { get; set; }
		public (float target, float weight) NarrowEyes { get; set; }
		public (float target, float weight) EyesAngle { get; set; }
		public (float target, float weight) Fatness { get; set; }
		public (float target, float weight) NarrowWaist { get; set; }
		public (float target, float weight) LegsSize { get; set; }
		public (float target, float weight) ArmThickness { get; set; }
		public (float target, float weight) WideTeeth { get; set; }
		public (float target, float weight) PupilSize { get; set; }
		public (float target, float weight) Scruffy { get; set; }
		public (bool target, float weight) ColoredEartlerTips { get; set; }
		public (bool target, float weight) DeepPupils { get; set; }
		public (int target, float weight) ColoredPupils { get; set; }
		public (int target, float weight) TailSegs { get; set; }
		public (float target, float weight) GeneralMelanin { get; set; }
		public (float target, float weight) BellyColorH { get; set; }
		public (float target, float weight) BellyColorS { get; set; }
		public (float target, float weight) BellyColorL { get; set; }
		public (float target, float weight) BodyColorH { get; set; }
		public (float target, float weight) BodyColorS { get; set; }
		public (float target, float weight) BodyColorL { get; set; }
		public (float target, float weight) DecorationColorH { get; set; }
		public (float target, float weight) DecorationColorS { get; set; }
		public (float target, float weight) DecorationColorL { get; set; }
		public (float target, float weight) EyeColorH { get; set; }
		public (float target, float weight) EyeColorL { get; set; }
		public (float target, float weight) HeadColorH { get; set; }
		public (float target, float weight) HeadColorS { get; set; }
		public (float target, float weight) HeadColorL { get; set; }
		public (float target, float weight) BellyColorBlack { get; set; }
		public (float target, float weight) BodyColorBlack { get; set; }
		public (float target, float weight) HeadColorBlack { get; set; }
		public (float target, float weight) BlockingSkill { get; set; }
		public (float target, float weight) DodgeSkill { get; set; }
		public (float target, float weight) MeleeSkill { get; set; }
		public (float target, float weight) MidRangeSkill { get; set; }
		public (float target, float weight) ReactionSkill { get; set; }
		public (float target, float weight) Top { get; set; }
		public (float target, float weight) Bottom { get; set; }
		public (BackPattern target, float weight) Pattern { get; set; }
		public (string target, float weight) Type { get; set; } = ("HardBackSpikes", 0f);	// Arbitrarily picking HardBackSpikes to be default so IDFinder-App default isn't null
		public (ColorTypeEnum target, float weight) ColorType { get; set; }
		public (bool target, float weight) IsColored { get; set; }
		public (int target, float weight) ScaleGraf { get; set; }
		public (float target, float weight) GeneralSize { get; set; }
		public (float target, float weight) Colored { get; set; }
		public (int target, float weight) NumberOfSpines { get; set; }
		#endregion
		public SearchParams Clone() => (SearchParams)this.MemberwiseClone();
		public static SearchParams operator +(SearchParams left, SearchParams right)
		{
			var res = left.Clone();
			if (right.Sympathy.weight != 0f) res.Sympathy = right.Sympathy;
			if (right.Energy.weight != 0f) res.Energy = right.Energy;
			if (right.Bravery.weight != 0f) res.Bravery = right.Bravery;
			if (right.Nervous.weight != 0f) res.Nervous = right.Nervous;
			if (right.Aggression.weight != 0f) res.Aggression = right.Aggression;
			if (right.Dominance.weight != 0f) res.Dominance = right.Dominance;
			if (right.Met.weight != 0f) res.Met = right.Met;
			if (right.Bal.weight != 0f) res.Bal = right.Bal;
			if (right.Size.weight != 0f) res.Size = right.Size;
			if (right.Stealth.weight != 0f) res.Stealth = right.Stealth;
			if (right.Dark.weight != 0f) res.Dark = right.Dark;
			if (right.EyeColor.weight != 0f) res.EyeColor = right.EyeColor;
			if (right.H.weight != 0f) res.H = right.H;
			if (right.S.weight != 0f) res.S = right.S;
			if (right.L.weight != 0f) res.L = right.L;
			if (right.Wideness.weight != 0f) res.Wideness = right.Wideness;
			if (right.BodyWeightFac.weight != 0f) res.BodyWeightFac = right.BodyWeightFac;
			if (right.GeneralVisibilityBonus.weight != 0f) res.GeneralVisibilityBonus = right.GeneralVisibilityBonus;
			if (right.VisualStealthInSneakMode.weight != 0f) res.VisualStealthInSneakMode = right.VisualStealthInSneakMode;
			if (right.LoudnessFac.weight != 0f) res.LoudnessFac = right.LoudnessFac;
			if (right.LungsFac.weight != 0f) res.LungsFac = right.LungsFac;
			if (right.ThrowingSkill.weight != 0f) res.ThrowingSkill = right.ThrowingSkill;
			if (right.PoleClimbSpeedFac.weight != 0f) res.PoleClimbSpeedFac = right.PoleClimbSpeedFac;
			if (right.CorridorClimbSpeedFac.weight != 0f) res.CorridorClimbSpeedFac = right.CorridorClimbSpeedFac;
			if (right.RunSpeedFac.weight != 0f) res.RunSpeedFac = right.RunSpeedFac;
			if (right.DangleFruit.weight != 0f) res.DangleFruit = right.DangleFruit;
			if (right.WaterNut.weight != 0f) res.WaterNut = right.WaterNut;
			if (right.JellyFish.weight != 0f) res.JellyFish = right.JellyFish;
			if (right.SlimeMold.weight != 0f) res.SlimeMold = right.SlimeMold;
			if (right.EggBugEgg.weight != 0f) res.EggBugEgg = right.EggBugEgg;
			if (right.FireEgg.weight != 0f) res.FireEgg = right.FireEgg;
			if (right.Popcorn.weight != 0f) res.Popcorn = right.Popcorn;
			if (right.GooieDuck.weight != 0f) res.GooieDuck = right.GooieDuck;
			if (right.LilyPuck.weight != 0f) res.LilyPuck = right.LilyPuck;
			if (right.GlowWeed.weight != 0f) res.GlowWeed = right.GlowWeed;
			if (right.DandelionPeach.weight != 0f) res.DandelionPeach = right.DandelionPeach;
			if (right.Neuron.weight != 0f) res.Neuron = right.Neuron;
			if (right.Centipede.weight != 0f) res.Centipede = right.Centipede;
			if (right.SmallCentipede.weight != 0f) res.SmallCentipede = right.SmallCentipede;
			if (right.VultureGrub.weight != 0f) res.VultureGrub = right.VultureGrub;
			if (right.SmallNeedleWorm.weight != 0f) res.SmallNeedleWorm = right.SmallNeedleWorm;
			if (right.Hazer.weight != 0f) res.Hazer = right.Hazer;
			if (right.NotCounted.weight != 0f) res.NotCounted = right.NotCounted;

			if (right.Elite != false) res.Elite = right.Elite;

			if (right.WaistWidth.weight != 0f) res.WaistWidth = right.WaistWidth;
			if (right.HeadSize.weight != 0f) res.HeadSize = right.HeadSize;
			if (right.EartlerWidth.weight != 0f) res.EartlerWidth = right.EartlerWidth;
			if (right.NeckThickness.weight != 0f) res.NeckThickness = right.NeckThickness;
			if (right.HandsHeadColor.weight != 0f) res.HandsHeadColor = right.HandsHeadColor;
			if (right.EyeSize.weight != 0f) res.EyeSize = right.EyeSize;
			if (right.NarrowEyes.weight != 0f) res.NarrowEyes = right.NarrowEyes;
			if (right.EyesAngle.weight != 0f) res.EyesAngle = right.EyesAngle;
			if (right.Fatness.weight != 0f) res.Fatness = right.Fatness;
			if (right.NarrowWaist.weight != 0f) res.NarrowWaist = right.NarrowWaist;
			if (right.LegsSize.weight != 0f) res.LegsSize = right.LegsSize;
			if (right.ArmThickness.weight != 0f) res.ArmThickness = right.ArmThickness;
			if (right.WideTeeth.weight != 0f) res.WideTeeth = right.WideTeeth;
			if (right.PupilSize.weight != 0f) res.PupilSize = right.PupilSize;
			if (right.Scruffy.weight != 0f) res.Scruffy = right.Scruffy;
			if (right.ColoredEartlerTips.weight != 0f) res.ColoredEartlerTips = right.ColoredEartlerTips;
			if (right.DeepPupils.weight != 0f) res.DeepPupils = right.DeepPupils;
			if (right.ColoredPupils.weight != 0f) res.ColoredPupils = right.ColoredPupils;
			if (right.TailSegs.weight != 0f) res.TailSegs = right.TailSegs;
			if (right.GeneralMelanin.weight != 0f) res.GeneralMelanin = right.GeneralMelanin;
			if (right.BellyColorH.weight != 0f) res.BellyColorH = right.BellyColorH;
			if (right.BellyColorS.weight != 0f) res.BellyColorS = right.BellyColorS;
			if (right.BellyColorL.weight != 0f) res.BellyColorL = right.BellyColorL;
			if (right.BodyColorH.weight != 0f) res.BodyColorH = right.BodyColorH;
			if (right.BodyColorS.weight != 0f) res.BodyColorS = right.BodyColorS;
			if (right.BodyColorL.weight != 0f) res.BodyColorL = right.BodyColorL;
			if (right.DecorationColorH.weight != 0f) res.DecorationColorH = right.DecorationColorH;
			if (right.DecorationColorS.weight != 0f) res.DecorationColorS = right.DecorationColorS;
			if (right.DecorationColorL.weight != 0f) res.DecorationColorL = right.DecorationColorL;
			if (right.EyeColorH.weight != 0f) res.EyeColorH = right.EyeColorH;
			if (right.EyeColorL.weight != 0f) res.EyeColorL = right.EyeColorL;
			if (right.HeadColorH.weight != 0f) res.HeadColorH = right.HeadColorH;
			if (right.HeadColorS.weight != 0f) res.HeadColorS = right.HeadColorS;
			if (right.HeadColorL.weight != 0f) res.HeadColorL = right.HeadColorL;
			if (right.BellyColorBlack.weight != 0f) res.BellyColorBlack = right.BellyColorBlack;
			if (right.BodyColorBlack.weight != 0f) res.BodyColorBlack = right.BodyColorBlack;
			if (right.HeadColorBlack.weight != 0f) res.HeadColorBlack = right.HeadColorBlack;
			if (right.BlockingSkill.weight != 0f) res.BlockingSkill = right.BlockingSkill;
			if (right.DodgeSkill.weight != 0f) res.DodgeSkill = right.DodgeSkill;
			if (right.MeleeSkill.weight != 0f) res.MeleeSkill = right.MeleeSkill;
			if (right.MidRangeSkill.weight != 0f) res.MidRangeSkill = right.MidRangeSkill;
			if (right.ReactionSkill.weight != 0f) res.ReactionSkill = right.ReactionSkill;
			if (right.Top.weight != 0f) res.Top = right.Top;
			if (right.Bottom.weight != 0f) res.Bottom = right.Bottom;
			if (right.Pattern.weight != 0f) res.Pattern = right.Pattern;
			if (right.Type.weight != 0f) res.Type = right.Type;
			if (right.ColorType.weight != 0f) res.ColorType = right.ColorType;
			if (right.IsColored.weight != 0f) res.IsColored = right.IsColored;
			if (right.ScaleGraf.weight != 0f) res.ScaleGraf = right.ScaleGraf;
			if (right.GeneralSize.weight != 0f) res.GeneralSize = right.GeneralSize;
			if (right.Colored.weight != 0f) res.Colored = right.Colored;
			if (right.NumberOfSpines.weight != 0f) res.NumberOfSpines = right.NumberOfSpines;

			return res;
		}

	}
	public class SlugParams : ISearchParams, IPersonalityParams, INPCStatsParams, ISlugcatStatsParams, IFoodPreferencesParams
	{
		public (float target, float weight) Sympathy { get; set; }
		public (float target, float weight) Energy { get; set; }
		public (float target, float weight) Bravery { get; set; }
		public (float target, float weight) Nervous { get; set; }
		public (float target, float weight) Aggression { get; set; }
		public (float target, float weight) Dominance { get; set; }
		public (float target, float weight) Met { get; set; }
		public (float target, float weight) Bal { get; set; }
		public (float target, float weight) Size { get; set; }
		public (float target, float weight) Stealth { get; set; }
		public (bool target, float weight) Dark { get; set; }
		public (float target, float weight) EyeColor { get; set; }
		public (float target, float weight) H { get; set; }
		public (float target, float weight) S { get; set; }
		public (float target, float weight) L { get; set; }
		public (float target, float weight) Wideness { get; set; }
		public (float target, float weight) BodyWeightFac { get; set; }
		public (float target, float weight) GeneralVisibilityBonus { get; set; }
		public (float target, float weight) VisualStealthInSneakMode { get; set; }
		public (float target, float weight) LoudnessFac { get; set; }
		public (float target, float weight) LungsFac { get; set; }
		public (int target, float weight) ThrowingSkill { get; set; }
		public (float target, float weight) PoleClimbSpeedFac { get; set; }
		public (float target, float weight) CorridorClimbSpeedFac { get; set; }
		public (float target, float weight) RunSpeedFac { get; set; }
		public (float target, float weight) DangleFruit { get; set; }
		public (float target, float weight) WaterNut { get; set; }
		public (float target, float weight) JellyFish { get; set; }
		public (float target, float weight) SlimeMold { get; set; }
		public (float target, float weight) EggBugEgg { get; set; }
		public (float target, float weight) FireEgg { get; set; }
		public (float target, float weight) Popcorn { get; set; }
		public (float target, float weight) GooieDuck { get; set; }
		public (float target, float weight) LilyPuck { get; set; }
		public (float target, float weight) GlowWeed { get; set; }
		public (float target, float weight) DandelionPeach { get; set; }
		public (float target, float weight) Neuron { get; set; }
		public (float target, float weight) Centipede { get; set; }
		public (float target, float weight) SmallCentipede { get; set; }
		public (float target, float weight) VultureGrub { get; set; }
		public (float target, float weight) SmallNeedleWorm { get; set; }
		public (float target, float weight) Hazer { get; set; }
		public (float target, float weight) NotCounted { get; set; }
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
		public (float target, float weight) Sympathy { get; set; }
		public (float target, float weight) Energy { get; set; }
		public (float target, float weight) Bravery { get; set; }
		public (float target, float weight) Nervous { get; set; }
		public (float target, float weight) Aggression { get; set; }
		public (float target, float weight) Dominance { get; set; }
		public bool Elite { get; set; } = false;
		public (float target, float weight) WaistWidth { get; set; }
		public (float target, float weight) HeadSize { get; set; }
		public (float target, float weight) EartlerWidth { get; set; }
		public (float target, float weight) NeckThickness { get; set; }
		public (float target, float weight) HandsHeadColor { get; set; }
		public (float target, float weight) EyeSize { get; set; }
		public (float target, float weight) NarrowEyes { get; set; }
		public (float target, float weight) EyesAngle { get; set; }
		public (float target, float weight) Fatness { get; set; }
		public (float target, float weight) NarrowWaist { get; set; }
		public (float target, float weight) LegsSize { get; set; }
		public (float target, float weight) ArmThickness { get; set; }
		public (float target, float weight) WideTeeth { get; set; }
		public (float target, float weight) PupilSize { get; set; }
		public (float target, float weight) Scruffy { get; set; }
		public (bool target, float weight) ColoredEartlerTips { get; set; }
		public (bool target, float weight) DeepPupils { get; set; }
		public (int target, float weight) ColoredPupils { get; set; }
		public (int target, float weight) TailSegs { get; set; }
		public (float target, float weight) GeneralMelanin { get; set; }
		public (float target, float weight) BellyColorH { get; set; }
		public (float target, float weight) BellyColorS { get; set; }
		public (float target, float weight) BellyColorL { get; set; }
		public (float target, float weight) BodyColorH { get; set; }
		public (float target, float weight) BodyColorS { get; set; }
		public (float target, float weight) BodyColorL { get; set; }
		public (float target, float weight) DecorationColorH { get; set; }
		public (float target, float weight) DecorationColorS { get; set; }
		public (float target, float weight) DecorationColorL { get; set; }
		public (float target, float weight) EyeColorH { get; set; }
		public (float target, float weight) EyeColorL { get; set; }
		public (float target, float weight) HeadColorH { get; set; }
		public (float target, float weight) HeadColorS { get; set; }
		public (float target, float weight) HeadColorL { get; set; }
		public (float target, float weight) BellyColorBlack { get; set; }
		public (float target, float weight) BodyColorBlack { get; set; }
		public (float target, float weight) HeadColorBlack { get; set; }
		public (float target, float weight) BlockingSkill { get; set; }
		public (float target, float weight) DodgeSkill { get; set; }
		public (float target, float weight) MeleeSkill { get; set; }
		public (float target, float weight) MidRangeSkill { get; set; }
		public (float target, float weight) ReactionSkill { get; set; }
		public (float target, float weight) Top { get; set; }
		public (float target, float weight) Bottom { get; set; }
		public (BackPattern target, float weight) Pattern { get; set; }
		public (string target, float weight) Type { get; set; } = ("HardBackSpikes", 0f);   // Arbitrarily picking HardBackSpikes to be default so IDFinder-App default isn't null
		public (ColorTypeEnum target, float weight) ColorType { get; set; }
		public (bool target, float weight) IsColored { get; set; }
		public (int target, float weight) ScaleGraf { get; set; }
		public (float target, float weight) GeneralSize { get; set; }
		public (float target, float weight) Colored { get; set; }
		public (int target, float weight) NumberOfSpines { get; set; }
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
        public (float target, float weight) Sympathy { get; set; }
        public (float target, float weight) Energy { get; set; }
        public (float target, float weight) Bravery { get; set; }
        public (float target, float weight) Nervous { get; set; }
        public (float target, float weight) Aggression { get; set; }
        public (float target, float weight) Dominance { get; set; }
		public bool AllWeightless()
		{
			if ((Sympathy.weight == 0f) &&
				(Energy.weight == 0f) &&
				(Nervous.weight == 0f) &&
				(Bravery.weight == 0f) &&
				(Aggression.weight == 0f) &&
				(Dominance.weight == 0f))
				return true;

			return false;
		}
    }
    public interface INPCStatsParams : ISearchParams
    {
        public (float target, float weight) Met { get; set; }
        public (float target, float weight) Bal { get; set; }
        public (float target, float weight) Size { get; set; }
        public (float target, float weight) Stealth { get; set; }
        public (bool target, float weight) Dark { get; set; }
        public (float target, float weight) EyeColor { get; set; }
        public (float target, float weight) H { get; set; }
        public (float target, float weight) S { get; set; }
        public (float target, float weight) L { get; set; }
        public (float target, float weight) Wideness { get; set; }
		public bool AllWeightless()
		{
			if ((Met.weight == 0f) &&
				(Bal.weight == 0f) &&
				(Size .weight == 0f) &&
				(Stealth.weight == 0f) &&
				(Dark.weight == 0f) &&
				(EyeColor.weight == 0f) &&
				(H.weight == 0f) &&
				(S.weight == 0f) &&
				(L.weight == 0f) &&
				(Wideness.weight == 0f))
				return true;

			return false;
		}
    }
    public interface ISlugcatStatsParams : ISearchParams
    {
        public (float target, float weight) BodyWeightFac { get; set; }
        public (float target, float weight) GeneralVisibilityBonus { get; set; }
        public (float target, float weight) VisualStealthInSneakMode { get; set; }
        public (float target, float weight) LoudnessFac { get; set; }
        public (float target, float weight) LungsFac { get; set; }
        public (int target, float weight) ThrowingSkill { get; set; }
        public (float target, float weight) PoleClimbSpeedFac { get; set; }
        public (float target, float weight) CorridorClimbSpeedFac { get; set; }
        public (float target, float weight) RunSpeedFac { get; set; }
		public bool AllWeightless()
		{
			if ((BodyWeightFac.weight == 0f) &&
				(GeneralVisibilityBonus.weight == 0f) &&
				(VisualStealthInSneakMode.weight == 0f) &&
				(LoudnessFac.weight == 0f) &&
				(LungsFac.weight == 0f) &&
				(ThrowingSkill.weight == 0f) &&
				(PoleClimbSpeedFac.weight == 0f) &&
				(CorridorClimbSpeedFac.weight == 0f) &&
				(RunSpeedFac.weight == 0f))
				return true;

			return false;
		}
    }
    public interface IFoodPreferencesParams : ISearchParams
    {
        public (float target, float weight) DangleFruit { get; set; }
        public (float target, float weight) WaterNut { get; set; }
        public (float target, float weight) JellyFish { get; set; }
        public (float target, float weight) SlimeMold { get; set; }
        public (float target, float weight) EggBugEgg { get; set; }
        public (float target, float weight) FireEgg { get; set; }
        public (float target, float weight) Popcorn { get; set; }
        public (float target, float weight) GooieDuck { get; set; }
        public (float target, float weight) LilyPuck { get; set; }
        public (float target, float weight) GlowWeed { get; set; }
        public (float target, float weight) DandelionPeach { get; set; }
        public (float target, float weight) Neuron { get; set; }
        public (float target, float weight) Centipede { get; set; }
        public (float target, float weight) SmallCentipede { get; set; }
        public (float target, float weight) VultureGrub { get; set; }
        public (float target, float weight) SmallNeedleWorm { get; set; }
        public (float target, float weight) Hazer { get; set; }
        public (float target, float weight) NotCounted { get; set; }
		public bool AllWeightless()
		{
			if ((DangleFruit.weight == 0f) &&
				(WaterNut.weight == 0f) &&
				(JellyFish.weight == 0f) &&
				(SlimeMold.weight == 0f) &&
				(EggBugEgg.weight == 0f) &&
				(FireEgg.weight == 0f) &&
				(Popcorn.weight == 0f) &&
				(GooieDuck.weight == 0f) &&
				(LilyPuck.weight == 0f) &&
				(GlowWeed.weight == 0f) &&
				(DandelionPeach.weight == 0f) &&
				(Neuron.weight == 0f) &&
				(Centipede.weight == 0f) &&
				(SmallCentipede.weight == 0f) &&
				(VultureGrub.weight == 0f) &&
				(SmallNeedleWorm.weight == 0f) &&
				(Hazer.weight == 0f) &&
				(NotCounted.weight == 0f))
				return true;

			return false;
		}
    }

    public interface IIndividualVariationsParams : ISearchParams
    {
		// Elites not here as it's a parameter in the Scavenger constructor, and influences other traits. Like slugcats and scavs are different, scavs and elite scavs are also different.
		public bool Elite { get; set; }
		public (float target, float weight) WaistWidth { get; set; }
		public (float target, float weight) HeadSize { get; set; }
		public (float target, float weight) EartlerWidth { get; set; }
		public (float target, float weight) NeckThickness { get; set; }
		public (float target, float weight) HandsHeadColor { get; set; }
		public (float target, float weight) EyeSize { get; set; }
		public (float target, float weight) NarrowEyes { get; set; }
		public (float target, float weight) EyesAngle { get; set; }
		public (float target, float weight) Fatness { get; set; }
		public (float target, float weight) NarrowWaist { get; set; }
		public (float target, float weight) LegsSize { get; set; }
		public (float target, float weight) ArmThickness { get; set; }
		public (float target, float weight) WideTeeth { get; set; }
		public (float target, float weight) PupilSize { get; set; }
		public (float target, float weight) Scruffy { get; set; }
		public (bool target, float weight) ColoredEartlerTips { get; set; }
		public (bool target, float weight) DeepPupils { get; set; }
		public (int target, float weight) ColoredPupils { get; set; }
		public (int target, float weight) TailSegs { get; set; }
		public (float target, float weight) GeneralMelanin { get; set; }
		public bool AllWeightless()
		{
			if ((WaistWidth.weight == 0f) &&
				(HeadSize.weight == 0f) &&
				(EartlerWidth.weight == 0f) &&
				(NeckThickness .weight == 0f) &&
				(HandsHeadColor.weight == 0f) &&
				(EyeSize.weight == 0f) &&
				(NarrowEyes.weight == 0f) &&
				(EyesAngle.weight == 0f) &&
				(Fatness.weight == 0f) &&
				(NarrowWaist.weight == 0f) &&
				(LegsSize.weight == 0f) &&
				(ArmThickness.weight == 0f) &&
				(WideTeeth.weight == 0f) &&
				(PupilSize.weight == 0f) &&
				(Scruffy.weight == 0f) &&
				(ColoredEartlerTips.weight == 0f) &&
				(DeepPupils.weight == 0f) &&
				(ColoredPupils .weight == 0f) &&
				(TailSegs.weight == 0f) &&
				(GeneralMelanin.weight == 0f))
				return true;

			return false;
		}
	}
    public interface IEartlersParams : ISearchParams
    {
        // Does this even need to be visible The data itself is quite abstracted from its in-game appearance. It could be searched for some more interesting observations though, like eartler size, complexity, etc. Needs to be thought over and experimented with ingame. 
    }
    public interface IScavColorsParams : ISearchParams
    {
		// I think this is better than the alternative of HSLColor structs, because it'd be convoluted figuring out which color channels to check. 
		public (float target, float weight) BellyColorH { get; set; }
		public (float target, float weight) BellyColorS { get; set; }
		public (float target, float weight) BellyColorL { get; set; }
		public (float target, float weight) BodyColorH { get; set; }
		public (float target, float weight) BodyColorS { get; set; }
		public (float target, float weight) BodyColorL { get; set; }
		public (float target, float weight) DecorationColorH { get; set; }
		public (float target, float weight) DecorationColorS { get; set; }
		public (float target, float weight) DecorationColorL { get; set; }
		public (float target, float weight) EyeColorH { get; set; }
		public (float target, float weight) EyeColorL { get; set; }
		public (float target, float weight) HeadColorH { get; set; }
		public (float target, float weight) HeadColorS { get; set; }
		public (float target, float weight) HeadColorL { get; set; }
		public (float target, float weight) BellyColorBlack { get; set; }
		public (float target, float weight) BodyColorBlack { get; set; }
		public (float target, float weight) HeadColorBlack { get; set; }
		public bool AllWeightless()
		{
			if ((BellyColorH.weight == 0f) &&
				(BellyColorS.weight == 0f) &&
				(BellyColorL.weight == 0f) &&
				(BodyColorH.weight == 0f) &&
				(BodyColorS.weight == 0f) &&
				(BodyColorL.weight == 0f) &&
				(DecorationColorH.weight == 0f) &&
				(DecorationColorS.weight == 0f) &&
				(DecorationColorL.weight == 0f) &&
				(EyeColorH.weight == 0f) &&
				(EyeColorL.weight == 0f) &&
				(HeadColorH.weight == 0f) &&
				(HeadColorS.weight == 0f) &&
				(HeadColorL.weight == 0f) &&
				(BellyColorBlack .weight == 0f) &&
				(BodyColorBlack.weight == 0f) &&
				(HeadColorBlack.weight == 0f))
				return true;

			return false;
		}
	}
    public interface IScavSkillsParams : ISearchParams
    {
		public (float target, float weight) BlockingSkill { get; set; }
		public (float target, float weight) DodgeSkill { get; set; }
		public (float target, float weight) MeleeSkill { get; set; }
		public (float target, float weight) MidRangeSkill { get; set; }
		public (float target, float weight) ReactionSkill { get; set; }
		public bool AllWeightless()
		{
			if ((BlockingSkill .weight == 0f) &&
				(DodgeSkill.weight == 0f) &&
				(MeleeSkill.weight == 0f) &&
				(MidRangeSkill.weight == 0f) &&
				(ReactionSkill.weight == 0f))
				return true;

			return false;
		}
	}
    public interface IScavBackPatternsParams : ISearchParams
    {
		public (float target, float weight) Top { get; set; }
		public (float target, float weight) Bottom { get; set; }
		public (BackPattern target, float weight) Pattern { get; set; }
		public (string target, float weight) Type { get; set; }	// Target would be BackDecals not string, but this circumvents calling ToString() constantly to compare it.
		public (ColorTypeEnum target, float weight) ColorType { get; set; }
		public (bool target, float weight) IsColored { get; set; }
		public (int target, float weight) ScaleGraf { get; set; }	// At some point map the scale graphics so I know what to target
		public (float target, float weight) GeneralSize { get; set; }
		public (float target, float weight) Colored { get; set; }
		public (int target, float weight) NumberOfSpines { get; set; }
		public bool AllWeightless()
		{
			if ((Top.weight == 0f) &&
				(Bottom.weight == 0f) &&
				(Pattern.weight == 0f) &&
				(Type.weight == 0f) &&
				(ColorType.weight == 0f) &&
				(IsColored.weight == 0f) &&
				(ScaleGraf.weight == 0f) &&
				(GeneralSize.weight == 0f) &&
				(Colored.weight == 0f) &&
				(NumberOfSpines.weight == 0f))
				return true;

			return false;
		}
	}
    #endregion
}
