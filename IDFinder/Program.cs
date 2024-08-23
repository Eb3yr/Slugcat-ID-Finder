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

            Task<IEnumerable<KeyValuePair<float, int>>> task = new(() => Searcher.SearchThreaded(0, int.MaxValue / 32, 48, 12, sParams, true, false));
			DateTime timer = DateTime.Now;
			task.Start();
			Searcher.AbortSearch = true;
			result = task.Result;
			//result = Searcher.SearchThreaded(/*int.MinValue*/0, int.MaxValue / 32, 48, 12, sParams, true, false);
			Console.WriteLine("Time: " + DateTime.Now.Subtract(dt).TotalSeconds + "s");
			//foreach (var kvp in result)
			//	Console.WriteLine(kvp.Value + ": " + kvp.Key);
			
			//File.WriteAllLines("eepy.txt", result.Select(kvp => kvp.Value + ": " + kvp.Key));
			
			//Console.WriteLine("\n\n\n\n");
			//Scavenger scav;
			//List<int> final = [];
			//foreach (var kvp in result)
			//{
			//	scav = new(kvp.Value);
			//	if (scav.Colors.BodyColor.H > 0.21f || scav.Colors.BodyColor.H < 0.135f)
			//		continue;
			//	if (scav.Colors.BodyColor.S < 0.7f)
			//		continue;
			//	if (Math.Abs(scav.Colors.BodyColor.L - 0.5f) > 0.35f)
			//		continue;
			//	if (!(scav.Colors.DecorationColor.H < 0.9f && scav.Colors.DecorationColor.H > 0.1f))
			//		continue;
			//	if (scav.BackPatterns.UseDetailColor == false)
			//		continue;
			//	final.Add(kvp.Value);
			//}
			
			foreach (var i in result)
				Console.WriteLine(i.Value + ": " + i.Key);


			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}

// TODO: Add UseDetailColor to IScavBackPatternsParams
// TODO: Go through search methods and clean up unecessary conversions to lists with LINQ alternatives. ToList on large lists will be expensive and want to avoid it where possible.
// TODO: wh is the VS debugger only using 15% or so CPU when running 12 threads? Investigate.