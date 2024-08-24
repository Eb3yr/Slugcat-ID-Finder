using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace IDFinder
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SearchParams sParams = new()
			{
				Aggression = (1f, 1f),
				Dominance = (1f, 1f),
				Energy = (1f, 1f),
				Nervous = (1f, 1f),
				Bravery = (1f, 1f),
				Sympathy = (0f, 1f),
				BlockingSkill = (1f, 1f),
				DodgeSkill = (1f, 1f),
				MeleeSkill = (1f, 1f),
				MidRangeSkill = (1f, 1f),
				ReactionSkill = (1f, 1f),
			};
			var dt = DateTime.Now;
			IEnumerable<KeyValuePair<float, int>> result;// = Searcher.SearchThreaded(0, 2871250, 300, 12, sParams, true, false);
			var ts = TimeSpan.Zero;
			
			dt = DateTime.Now;
			Searcher.SearchThreaded(0, int.MaxValue / 32, 4, 4, sParams, false, true);
			//Searcher.Search(0, 1000000, 4, sParams, true);
			ts += DateTime.Now.Subtract(dt);
			
			Console.WriteLine("Time: " + ts.TotalSeconds + "s");

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}

// TODO: Add UseDetailColor to IScavBackPatternsParams
// TODO: Rework & logging to use floats rather than ints, and update the float periodically in the if( (i & 127) == 0) statements. Maybe bump them up to 255 or even 511. Under the heaviest loads these should be updating multiple times a second - ideally 10x or more. 
// TODO: !! Verify Abort's duration is long enough that everything terminates. Not just for my processor, but also for slow ones.