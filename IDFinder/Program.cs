using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace IDFinder
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SearchParams sParams;// = new()
			 //{
			 //	Aggression = (1f, 1f),
			 //	Dominance = (1f, 1f),
			 //	Energy = (1f, 1f),
			 //	Nervous = (1f, 1f),
			 //	Bravery = (1f, 1f),
			 //	Sympathy = (0f, 1f),
			 //	BlockingSkill = (1f, 1f),
			 //	DodgeSkill = (1f, 1f),
			 //	MeleeSkill = (1f, 1f),
			 //	MidRangeSkill = (1f, 1f),
			 //	ReactionSkill = (1f, 1f),
			 //};
			 //sParams = new()
			 //{
			 //	H = (0.5208333f, 1f),
			 //	S = (0.05882353f, 1f),
			 //	L = (0.73333335f, 1f)
			 //};
			 //sParams = new ScavParams()
			 //{
			 //	EyeColorH = (1f, 1f),
			 //	HeadColorH = (0.5f, 1f),
			 //	DeepPupils = (true, 1f)
			 //};
			sParams = new ScavParams()
			{
				DecorationColorL = (0.5f, 1f),
				DecorationColorS = (1f, 1f),
				NumberOfSpines = (40, 1f),
				Pattern = (BackDecals.BackPattern.DoubleSpineRidge, 0.1f),
				ColorType = (BackTuftsAndRidges.ColorTypeEnum.Decoration, 1f),
				GeneralSize = (1f, 0.5f)
			};
			var dt = DateTime.Now;
			IEnumerable<KeyValuePair<float, int>> result;// = Searcher.SearchThreaded(0, 2871250, 300, 12, sParams, true, false);
			var ts = TimeSpan.Zero;
			
			dt = DateTime.Now;
			result = Searcher.SearchThreaded(int.MinValue, int.MaxValue, 1024, 12, sParams, true, true);
			//Searcher.Search(0, 1000000, 4, sParams, true);
			ts += DateTime.Now.Subtract(dt);

			File.WriteAllLines("results.txt", result.Select(res => $"{res.Value}: {res.Key}"));

			foreach (var i in result)
				Console.WriteLine(i.Value + ": " + i.Key);

			Console.WriteLine("Time: " + ts.TotalSeconds + "s");

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}

// NOTE: If you want anything BUT a target, use a negative weight. 