using System.Text.Json;
using System.Text.Json.Serialization;

namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
            
            PersonalityParams pp = new();
            ((ISearchParams)pp).AllNull();

			Scavenger scav = new(0);

			JsonSerializerOptions opt = new()
			{
				WriteIndented = true,
				Converters =
				{
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				},
				IncludeFields = true
			};
			string json = JsonSerializer.Serialize(scav, opt);
			json += "\n\nBack Patterns:\n";
			json += $"type: {scav.BackPatterns.GetType()}\n";
			json += JsonSerializer.Serialize(scav.BackPatterns as object, opt);
			File.WriteAllText("scav.json", json);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
	}
}