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

// TODO: back patterns class, colors properly formatted with JSON, searching scavs
// !! BackDecals isn't matching up with what's shown by the ingame finder.
// Colours, variations, skills, personality all are fine. Back Patterns is getting __EVERYTHING__ wrong, including the subclass and pattern.
// There's some RNG in the tailsegments part of the scavengergraphics constructor. Not sure where yet, but the state changes from before to after it. 


//
//
//
//
// FOUND IT: BodyPart.Reset calls UnityEngine.Random.value once, so once for each tail segment.
//
//
//
//