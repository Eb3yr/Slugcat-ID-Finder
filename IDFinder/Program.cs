﻿using System.Text.Json;
using System.IO;
using System.ComponentModel;
using Unity_XORShift;
namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
            Searcher search = new(new Searcher.SearchParams()
            {
                Aggression = (0f, 1f),
                Bravery = (0f, 1f),
                Dominance = (0f, 1f),
                Nervous = (0f, 1f),
                Energy = (0f, 1f),
                Sympathy = (0f, 1f)
            });

            DateTime dt;
            dt = DateTime.Now;
            Dictionary<float, Slugcat> result = new(search.Search(0, 100000000, 911, true));
            Console.WriteLine("Completion time: " + DateTime.Now.Subtract(dt).TotalMilliseconds.ToString());
            foreach (KeyValuePair<float, Slugcat> kvp in result)
            {
                Console.WriteLine($"ID: {kvp.Value.ID}, weight: {kvp.Key}");
            }
            SlugManager sm = new(result.Values);
            sm.WriteToCSV("outSearched.csv");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
	}
}
