using System.Text.Json;
using System.IO;
using System.ComponentModel;
using Unity_XORShift;
using System;
namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
            //Searcher search = new(new Searcher.SearchParams()
            //{
            //    //Aggression = (0f, 1f),
            //    //Bravery = (0f, 1f),
            //    //Dominance = (0f, 1f),
            //    //Nervous = (0f, 1f),
            //    Energy = (1f, 1f),
            //    //Sympathy = (0f, 1f)
            //    S = (1f, 1f),
            //    L = (0.65f, 1f)
            //});
            //
            //DateTime dt;
            //dt = DateTime.Now;
            //Dictionary<float, Slugcat> result = new(search.Search(0, 10000000, 1200, true));
            //TimeSpan completion = DateTime.Now.Subtract(dt);
            //Console.WriteLine("Completion time: " + completion.TotalMilliseconds.ToString());
            //foreach (KeyValuePair<float, Slugcat> kvp in result)
            //{
            //    Console.WriteLine($"ID: {kvp.Value.ID}, weight: {kvp.Key}");
            //}
            //Console.WriteLine("Completion time: " + completion.TotalMilliseconds.ToString());
            //SlugManager sm = new(result.Values);
            //sm.WriteToCSV("outSearched.csv");

            JsonSerializerOptions options = new()
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                WriteIndented = true,
                IncludeFields = true
            };
            Scavenger aScav = new Scavenger(1000000);
            Console.WriteLine("aScav blocking: " + aScav.Skills.BlockingSkill);
            File.WriteAllText("ScavOutTest.txt", JsonSerializer.Serialize(aScav, options));
            Console.WriteLine("Done");
            Console.ReadLine();
        }
	}
}
