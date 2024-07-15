using System.Text.Json;
using System.IO;
using System.ComponentModel;
using Unity_XORShift;
namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
			#region Search
			Searcher search = new(new Searcher.SearchParams()
			{
				Aggression = (1f, 1f),
				Bravery = (1f, 1f),
				Dominance = (1f, 1f),
				Nervous = (1f, 1f),
				Energy = (1f, 1f),
				Sympathy = (1f, 1f)
			});

			/*DateTime dt;
			dt = DateTime.Now;
			Dictionary<float, Slugcat> result = new(search.Search(0, 100000000, 6));
			Console.WriteLine("Completion time: " + DateTime.Now.Subtract(dt).TotalMilliseconds.ToString());
			foreach (KeyValuePair<float, Slugcat> kvp in result)
			{
				Console.WriteLine($"ID: {kvp.Value.ID}, weight: {kvp.Key}");
			}
			SlugManager sm = new(result.Values);
			sm.WriteToCSV("outSearched.csv")*/
			#endregion
			Run(0, 1000000000);
			Console.WriteLine("Done");
			Console.ReadLine();
		}
		static void Run(int start, int count)
		{
			Searcher search = new(new Searcher.SearchParams()
			{
				Aggression = (1f, 1f),
				Bravery = (1f, 1f),
				Dominance = (1f, 1f),
				Nervous = (1f, 1f),
				Energy = (1f, 1f),
				Sympathy = (1f, 1f)
			});
			DateTime dt;
			dt = DateTime.Now;
			Dictionary<float, Slugcat> result = new(search.Search(start, count, 6));
			Console.WriteLine("Completion time: " + DateTime.Now.Subtract(dt).TotalMilliseconds.ToString());
			foreach (KeyValuePair<float, Slugcat> kvp in result)
			{
				Console.WriteLine($"ID: {kvp.Value.ID}, weight: {kvp.Key}");
			}
			SlugManager sm = new(result.Values);
			sm.WriteToCSV($"outSearched{start}.csv");
		}
		static (int ID, float foodSum) FoodSearch(int start, int stopInclusive)
		{
			// about 10% quicker than with reflection
			float sum;
			bool flag;
			FoodPreferences fp;
			DateTime dt = DateTime.Now;
			(int ID, float foodSum) largest = new();
			for (int i = start; i <= stopInclusive; i++)
			{
				flag = true;
				sum = 0f;
				fp = new(i);
				#region properties
				if (fp.DangleFruit < 0f) { flag = false; continue; }
				else sum += fp.DangleFruit;
				if (fp.WaterNut < 0f) { flag = false; continue; }
				else sum += fp.WaterNut;
				if (fp.JellyFish < 0f) { flag = false; continue; }
				else sum += fp.JellyFish;
				if (fp.SlimeMold < 0f) { flag = false; continue; }
				else sum += fp.SlimeMold;
				if (fp.EggBugEgg < 0f) { flag = false; continue; }
				else sum += fp.EggBugEgg;
				if (fp.FireEgg < 0f) { flag = false; continue; }
				else sum += fp.FireEgg;
				if (fp.Popcorn < 0f) { flag = false; continue; }
				else sum += fp.Popcorn;
				if (fp.GooieDuck < 0f) { flag = false; continue; }
				else sum += fp.GooieDuck;
				if (fp.LilyPuck < 0f) { flag = false; continue; }
				else sum += fp.LilyPuck;
				if (fp.GlowWeed < 0f) { flag = false; continue; }
				else sum += fp.GlowWeed;
				if (fp.DandelionPeach < 0f) { flag = false; continue; }
				else sum += fp.DandelionPeach;
				if (fp.Neuron < 0f) { flag = false; continue; }
				else sum += fp.Neuron;
				if (fp.Centipede < 0f) { flag = false; continue; }
				else sum += fp.Centipede;
				if (fp.SmallCentipede < 0f) { flag = false; continue; }
				else sum += fp.SmallCentipede;
				if (fp.VultureGrub < 0f) { flag = false; continue; }
				else sum += fp.VultureGrub;
				if (fp.SmallNeedleWorm < 0f) { flag = false; continue; }
				else sum += fp.SmallNeedleWorm;
				if (fp.Hazer < 0f) { flag = false; continue; }
				else sum += fp.Hazer;
				if (fp.NotCounted < 0f) { flag = false; continue; }
				else sum += fp.NotCounted;
				#endregion
				if (!flag) continue;
				if (sum > largest.foodSum)
				{
					largest.foodSum = sum;
					largest.ID = i;
				}
			}
			Console.WriteLine($"{largest.ID}, {largest.foodSum}");
			return largest;
		}
	}
}
