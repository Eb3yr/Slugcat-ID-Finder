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
			sParams = new()
			{
				H = (0.5208333f, 1f),
				S = (0.05882353f, 1f),
				L = (0.73333335f, 1f)
			};
			sParams = new()
			{
				L = (0.5f, 1f)
			};
			var dt = DateTime.Now;
			IEnumerable<KeyValuePair<float, int>> result;// = Searcher.SearchThreaded(0, 2871250, 300, 12, sParams, true, false);
			var ts = TimeSpan.Zero;
			
			dt = DateTime.Now;
			result = Searcher.SearchThreaded(0, int.MaxValue / 4, 32, 12, sParams, true, true);
			//Searcher.Search(0, 1000000, 4, sParams, true);
			ts += DateTime.Now.Subtract(dt);

			File.WriteAllLines("results.txt", result.Select(res => $"{res.Value}: {res.Key}"));

			Console.WriteLine("Time: " + ts.TotalSeconds + "s");

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}

// TODO: Add UseDetailColor to IScavBackPatternsParams
// TODO: Rework & logging to use floats rather than ints, and update the float periodically in the if( (i & 127) == 0) statements. Maybe bump them up to 255 or even 511. Under the heaviest loads these should be updating multiple times a second - ideally 10x or more. 
// TODO: !! Verify Abort's duration is long enough that everything terminates. Not just for my processor, but also for slow ones.