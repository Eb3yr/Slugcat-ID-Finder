using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Diagnostics;

namespace IDFinder
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SearchParams sParams;
			sParams = new()
			{
				Scruffy = (1f, 1f),
				HeadColorL = (0.5f, 1f),
				HeadColorS = (1f, 1f),
				ReactionSkill = (1f, 0.2f),
				NumberOfSpines = (40, 0.25f),
				IsColored = (true, 0.4f),
			};
			//sParams = new ScavParams()
			//{
			//	NumberOfSpines = (2, 1f),
			//	Pattern = (BackDecals.BackPattern.RandomBackBlotch, 10f),
			//	DecorationColorS = (1f, 1f),
			//};

			EventHandler<SearchProgressEventArgs>? progEvent = null;
			progEvent += (sender, args) =>
			{
				Console.WriteLine($"{float.Round(args.Progress, 1)}%");
			};

			IEnumerable<KeyValuePair<float, int>> result;
			
			Stopwatch sw = Stopwatch.StartNew();
			result = Searcher.SearchThreaded(0, int.MaxValue / 64, 32, 12, sParams, true, progressEventHandler: progEvent, progressInterval: 5000);
			sw.Stop();

			File.WriteAllLines("results.txt", result.Select(res => $"{res.Value}: {res.Key}"));

			foreach (var i in result)
				Console.WriteLine(i.Value + ": " + i.Key);

			Console.WriteLine($"Time: {sw.ElapsedMilliseconds / 1000f}s");

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}

// NOTE: If you want anything BUT a target, use a negative weight. 