using IDFinder;

namespace IDFinder_App
{
	public interface IIndexState
	{
		bool FolderPersonality { get; set; }
		bool FolderScug { get; set; }
		bool FolderScav { get; set; }

		bool CheckedScug { get; set; }
		bool CheckedScav { get; set; }

		bool FolderNPCStats { get; set; }
		bool FolderSlugcatStats { get; set; }
		bool FolderFoodPrefs { get; set; }
		bool FolderIVars { get; set; }
		bool FolderScavColors { get; set; }
		bool FolderScavSkills { get; set; }
		bool FolderScavBack { get; set; }

		bool CheckedPersonality { get; set; }
		bool CheckedNPCStats { get; set; }
		bool CheckedSlugcatStats { get; set; }
		bool CheckedFoodPrefs { get; set; }
		bool CheckedIVars { get; set; }
		bool CheckedScavColors { get; set; }
		bool CheckedScavSkills { get; set; }
		bool CheckedScavBack { get; set; }

		IPersonalityParams personalityParams { get; set; }
		INPCStatsParams nPCStatsParams { get; set; }
		ISlugcatStatsParams slugcatStatsParams { get; set; }
		IFoodPreferencesParams foodPreferencesParams { get; set; }
		IIndividualVariationsParams individualVariationsParams { get; set; }
		IScavColorsParams scavColorsParams { get; set; }
		IScavSkillsParams scavSkillsParams { get; set; }
		IScavBackPatternsParams scavBackPatternsParams { get; set; }
	}
	public class IndexState	: IIndexState // Singleton used to prevent Index.razor from being reset every time the page is reloaded. 
	{
		public bool FolderPersonality { get; set; } = true;
		public bool FolderScug { get; set; } = true;
		public bool FolderScav { get; set; } = true;

		public bool CheckedPersonality { get; set; } = false;
		private bool _checkedScug = false;
		public bool CheckedScug
		{
			get => _checkedScug;
			set
			{
				_checkedScug = value;
				CheckedNPCStats = value;
				CheckedSlugcatStats = value;
				CheckedFoodPrefs = value;
			}
		}
		private bool _checkedScav = false;
		public bool CheckedScav
		{
			get => _checkedScav;
			set
			{
				_checkedScav = value;
				CheckedIVars = value;
				CheckedScavColors = value;
				CheckedScavSkills = value;
				CheckedScavBack = value;
			}
		}

		public bool FolderNPCStats { get; set; } = true;
		public bool FolderSlugcatStats { get; set; } = true;
		public bool FolderFoodPrefs { get; set; } = true;
		public bool FolderIVars { get; set; } = true;
		public bool FolderScavColors { get; set; } = true;
		public bool FolderScavSkills { get; set; } = true;
		public bool FolderScavBack { get; set; } = true;
		
		public bool CheckedNPCStats { get; set; } = false;
		public bool CheckedSlugcatStats { get; set; } = false;
		public bool CheckedFoodPrefs { get; set; } = false;
		public bool CheckedIVars { get; set; } = false;
		public bool CheckedScavColors { get; set; } = false;
		public bool CheckedScavSkills { get; set; } = false;
		public bool CheckedScavBack { get; set; } = false;

		public IPersonalityParams personalityParams { get; set; } = new SearchParams();
		public INPCStatsParams nPCStatsParams { get; set; } = new SearchParams();
		public ISlugcatStatsParams slugcatStatsParams { get; set; } = new SearchParams();
		public IFoodPreferencesParams foodPreferencesParams { get; set; } = new SearchParams();
		public IIndividualVariationsParams individualVariationsParams { get; set; } = new SearchParams();
		public IScavColorsParams scavColorsParams { get; set; } = new SearchParams();
		public IScavSkillsParams scavSkillsParams { get; set; } = new SearchParams();
		public IScavBackPatternsParams scavBackPatternsParams { get; set; } = new SearchParams();
	}

	internal class TreeState
	{
		
	}
}
