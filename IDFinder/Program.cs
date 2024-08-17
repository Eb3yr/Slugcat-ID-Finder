using System.Text.Json;

namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
            //Searcher search = new(new Searcher.SearchParams()
            //{
            //    Aggression = (1f, 1f),
            //    L = (0.65f, 3f),
            //    //RunSpeedFac = (1f, 1f),
            //    //DangleFruit = (1f, 1f)
            //});
            //
            //foreach (KeyValuePair<float, Slugcat> sc in search.Search(0, 10000000, 3))
            //{
            //    Console.WriteLine($"Old, ID: {sc.Value.ID}, weight: {sc.Key}");
            //}
            //foreach (KeyValuePair<float, int> sc in search.MULTISearch(0, 10000000, 3).Result)
            //{
            //    Console.WriteLine($"New, ID: {sc.Value}, weight: {sc.Key}");
            //}

            //var options = new JsonSerializerOptions()
            //{
            //    IncludeFields = true,
            //    WriteIndented = true
            //};
            //Scavenger scav = new(0);
            //string scavStr = JsonSerializer.Serialize(scav, options);
            //File.WriteAllText("scavWhole.json", scavStr);
            //scavStr = JsonSerializer.Serialize(Scavenger.Get(0, false, true, true, true, false, true, false), options);
            //File.WriteAllText("scavPart.json", scavStr);

            PersonalityParams pp = new();
            ((ISearchParams)pp).AllNull();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
	}
}
