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
				BodyColorL = (0.5f, 1f),
				HeadColorL = (0.5f, 1f),
				BodyColorS = (1f, 1f),
				HeadColorS = (1f, 1f),
				BodyColorH = (0.16f, 0.5f),
				HeadColorH = (0.16f, 0.5f),
				NumberOfSpines = (40, 2f),
				GeneralSize = (1f, 1f),
				//ColorType = (BackTuftsAndRidges.ColorTypeEnum.Decoration, 2f),
				//Type = ("HardBackSpikes", 1f),
				DecorationColorH = (0.16f, 0.5f),
				DecorationColorL = (0.5f, 0.5f),
				DecorationColorS = (1f, 0.5f)
			};
			var dt = DateTime.Now;
			var result = Searcher.Search(0, 2871250, 30000, sParams, true);
			Console.WriteLine("Time: " + DateTime.Now.Subtract(dt).TotalSeconds + "s");
			//foreach (var kvp in result)
			//	Console.WriteLine(kvp.Value + ": " + kvp.Key);

			Console.WriteLine("\n\n\n\n");
			Scavenger scav;
			List<int> final = [];
			foreach (var kvp in result)
			{
				scav = new(kvp.Value);
				if (scav.Colors.BodyColor.H > 0.18f || scav.Colors.BodyColor.H < 0.14f)
					continue;
				if (scav.Colors.BodyColor.S < 0.8f)
					continue;
				if (Math.Abs(scav.Colors.BodyColor.L - 0.5f) > 0.2f)
					continue;
				final.Add(kvp.Value);
			}
			foreach (var i in final)
				Console.WriteLine(i);

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}