using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace IDFinder
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SearchParams sParams;
			//sParams = new ScavParams()
			//{
			//	NumberOfSpines = (0, 1f),
			//	ArmThickness = (0f, 1f),
			//	NeckThickness = (0f, 1f),
			//	WaistWidth = (1f, 1f)
			//};
			sParams = new ScavParams()
			{
				NumberOfSpines = (2, 1f),
				Pattern = (BackDecals.BackPattern.RandomBackBlotch, 10f),
				DecorationColorS = (1f, 1f),
			};
			var dt = DateTime.Now;
			IEnumerable<KeyValuePair<float, int>> result;// = Searcher.SearchThreaded(0, 2871250, 300, 12, sParams, true, false);
			var ts = TimeSpan.Zero;
			
			dt = DateTime.Now;
			result = Searcher.SearchThreaded(0, int.MaxValue / 64, 24, 12, sParams, true);
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