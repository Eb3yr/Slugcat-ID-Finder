﻿using System.Text.Json;
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
            Searcher search = new(new Searcher.SearchParams()
            {
                Aggression = (1f, 1f),
                H = (1f, 1f),
                RunSpeedFac = (1f, 1f),
                DangleFruit = (1f, 1f)
            });
            
            DateTime dt;
            dt = DateTime.Now;
            Dictionary<float, Slugcat> result = new(search.Search(0, 1000000, 12, true));
            TimeSpan completion = DateTime.Now.Subtract(dt);
            Console.WriteLine("Completion time: " + completion.TotalMilliseconds.ToString());
            foreach (KeyValuePair<float, Slugcat> kvp in result)
            {
                Console.WriteLine($"ID: {kvp.Value.ID}, weight: {kvp.Key}");
            }
            Console.WriteLine("Completion time: " + completion.TotalMilliseconds.ToString());
            SlugManager sm = new(result.Values);
            sm.WriteToCSV("outSearched.csv");

            //JsonSerializerOptions options = new()
            //{
            //    WriteIndented = true,
            //    IncludeFields = true
            //};
            //Scavenger aScav = new Scavenger(10000);
            //File.WriteAllText("ScavOutTest.txt", JsonSerializer.Serialize(aScav, options));
            Console.WriteLine("Done");
            Console.ReadLine();
        }
	}
}
