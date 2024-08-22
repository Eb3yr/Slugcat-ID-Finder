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
				ColorType = (BackTuftsAndRidges.ColorTypeEnum.Decoration, 2f),
				Type = ("HardBackSpikes", 1000f),
				DecorationColorL = (0.5f, 1f),
				DecorationColorS = (1f, 1f)
			};
			sParams = new()
			{
				NumberOfSpines = (40, 1f)
			};
			var dt = DateTime.Now;
			IEnumerable<KeyValuePair<float, int>> result;// = Searcher.SearchThreaded(0, 2871250, 3000, 12, sParams, false, false);
			result = Searcher.SearchThreaded(0, 1000000, 20, 2, sParams, true, true);
			Console.WriteLine("Time: " + DateTime.Now.Subtract(dt).TotalSeconds + "s");
			foreach (var kvp in result)
				Console.WriteLine(kvp.Value + ": " + kvp.Key);

			

			//Console.WriteLine("\n\n\n\n");
			//Scavenger scav;
			//List<int> final = [];
			//foreach (var kvp in result)
			//{
			//	scav = new(kvp.Value);
			//	if (scav.Colors.BodyColor.H > 0.19f || scav.Colors.BodyColor.H < 0.13f)
			//		continue;
			//	if (scav.Colors.BodyColor.S < 0.7f)
			//		continue;
			//	if (Math.Abs(scav.Colors.BodyColor.L - 0.5f) > 0.3f)
			//		continue;
			//	if (!(scav.Colors.DecorationColor.H < 0.9f && scav.Colors.DecorationColor.H > 0.1f))
			//		continue;
			//	if (scav.BackPatterns.UseDetailColor == false)
			//		continue;
			//	final.Add(kvp.Value);
			//}
			//foreach (var i in final)
			//	Console.WriteLine(i);

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}

// TODO: Investigate solutions to very slow performance with a list in Search. https://www.jacksondunstan.com/articles/3189
// If I assume it to be sorted when it has 1 element, I can maintain that state when adding further elements and use binary search to insert at the correct index
// Then I just have to call an Insert, and Remove the final element. 
// Indexing is O(1), so there's no worry about overwriting the smallest element at the end when it's at max capacity. Likewise RemoveAt(last index) is O(1)
// But ideally the sort will just be a single iteration through, insertion, another iteration with nothing and done. 

// TODO: Add UseDetailColor to IScavBackPatternsParams