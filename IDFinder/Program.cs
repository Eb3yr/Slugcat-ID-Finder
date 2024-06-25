using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
using System.Threading;
using System.Runtime.Versioning;

namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{

			//SlugManager manager = new(Enumerable.Range(1000, 20000));
			//SelectedColumns sdc = SelectedColumns.Add([SelectedColumns.IDOnly(), SelectedColumns.PersonalityOnly(), SelectedColumns.StatsOnly(), SelectedColumns.FoodPrefOnly()]);
			//manager.WriteToCSV("Test.csv", sdc, true);
			//Console.WriteLine("Done");
			//Console.ReadLine();

			/*DateTime dt = DateTime.Now;
			new SlugManager([FoodSearchNoReflection(1, int.MaxValue).ID]).WriteToCSV("LargestFoodSumPos.csv", new(), true);
			Console.WriteLine("deltaTime (s) = " + DateTime.Now.Subtract(dt));
			Console.ReadLine();*/

			Console.ReadLine();
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
