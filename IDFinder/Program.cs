using System.Text.Json;
using System.IO;
namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
			/*
			JsonSerializerOptions options = new JsonSerializerOptions()
			{
				WriteIndented = true,
			};
			string str = JsonSerializer.Serialize<Slugcat>(new(1000), options);
			File.WriteAllText("out.json", str);

			string str2 = JsonSerializer.Serialize<SlugManager>(new(Enumerable.Range(1000, 20)), options);
			File.WriteAllText("outMany.json", str2);
			*/


			//File.WriteAllText("outTest.json", SlugManager.GetJsonMany([1000, 1001, 1002]));

			//Console.WriteLine("Done");
			//Console.ReadLine();
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
