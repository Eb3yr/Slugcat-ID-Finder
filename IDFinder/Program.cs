using System.Runtime.CompilerServices;
using System.Reflection;

namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
			SlugManager manager = new(Enumerable.Range(1000, 20000));
			SelectedColumns sdc = SelectedColumns.Add([SelectedColumns.IDOnly(), SelectedColumns.PersonalityOnly(), SelectedColumns.StatsOnly()]);
			manager.WriteToCSV("Test.csv", sdc, true);
			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}
