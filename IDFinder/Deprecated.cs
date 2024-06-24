using MemoryPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
	[MemoryPackable]
	public partial class SlugManagerOld
	{
		public Dictionary<int, NPCStats> stats { get; private set; } = new();
		public Dictionary<Food, float> foodPreference { get; private set; } = new();
		[MemoryPackConstructor]
		public SlugManagerOld() { }
		public void WriteToFile(string fileName)
		{
			byte[] data = MemoryPackSerializer.Serialize<SlugManagerOld>(this);
			File.WriteAllBytes(fileName, data);
		}
		public void WriteToCSV(string fileName, bool delete = true)
		{
			bool F = File.Exists(fileName);
			if (F && delete) File.Delete(fileName);

			using (StreamWriter sw = new StreamWriter(fileName, true))
			{
				NPCStats s;
				Personality p;
				string wr = "ID,agg,brv,dom,nrg,nrv,sym,,H,S,L,Dark?,Eye,,Size,Wide,FoodPrefs:";
				foreach (string name in Enum.GetNames<Food>())
				{
					wr += $",{name}";
				}
				if (!F) sw.WriteLine(wr);
				foreach (KeyValuePair<int, NPCStats> kp in stats)
				{
					s = kp.Value;
					//p = s.Personality;
					//wr = $"{kp.Key},{p.Aggression},{p.Bravery},{p.Dominance},{p.Energy},{p.Nervous},{p.Sympathy},,{s.H},{s.S},{s.L},{s.Dark},{s.EyeColor},,{s.Size},{s.Wideness}";
					foreach (float val in foodPreference.Values)
					{
						// fundamental problem here. SlugData class groups methods for handling multiple NPCStats. foodPreference should NOT be here, it should be in NPCStats, or in a class which
						// has a single NPCStats and foodPreference. Like a "Slugcat" class, and rename SlugData to a more suitable name
					}
					sw.WriteLine(wr);
				}
			}
		}
		public void LoadFromFile(string fileName)
		{
			if (!File.Exists(fileName)) return;
			byte[] data = File.ReadAllBytes(fileName);
			SlugManagerOld? sd = MemoryPackSerializer.Deserialize<SlugManagerOld>(data);
			if (sd == null) return;
			stats = sd.stats;
		}
		private void Add(int ID, NPCStats stats)
		{
			this.stats.Add(ID, stats);
		}
		public void Add(int ID)
		{
			stats.Add(ID, new NPCStats(ID));
		}
		public void Remove(int ID)
		{
			stats.Remove(ID);
		}
		public void GenerateBounds(int start, int stop)
		{
			// inclusive
			// shenanigans happen when you hit the integer limit. It overflows. Probably because it's inclusive of stop. o7
			for (int i = start; i <= stop; i++)
			{
				stats.Add(i, new NPCStats(i));
			}
		}
		public static SlugManagerOld ReturnGenerateBounds(int start, int stop)
		{
			SlugManagerOld data = new();
			data.GenerateBounds(start, stop);
			return data;
		}

		/*public int[] SearchIDs(int[] IDs, int limit, SearchParams parameters)
		{



			return default;
		}
		public int[] SearchIDs(int start, int stop, int limit, SearchParams parameters)
		{
			List<int> IDs = new();
			for (int i = start; i <= stop; i++)
			{
				IDs.Add(i);
			}
			return SearchIDs(IDs.ToArray(), limit, parameters);
		}*/
		public SlugManagerOld Search(int limit, SearchParams parameters)
		{
			// The SlugData class contains a collection of NPCStats and IDs, therefore this searches through 

			Dictionary<float, int> results = new();
			foreach (KeyValuePair<int, NPCStats> kp in stats)
			{
				results.Add(CalculateWeight(parameters, kp.Value), kp.Key);
			}

			List<float> sorted = results.Keys.ToList();
			sorted.Sort();
			SlugManagerOld returnData = new();
			for (int i = 0; i < limit; i++)
			{
				returnData.Add(results[sorted[i]], stats[results[sorted[i]]]);
			}

			return returnData;
		}
		public float CalculateWeight(SearchParams parameters, NPCStats stats)
		{
			return default;
		}
		/*public SlugData[] Search(int start, int stop, int limit, SearchParams parameters)
		{
			// really, searchIDs should just do this and return only the ID portion rather than the entire method
			List<int> IDs = new();
			for (int i = start; i <= stop; i++)
			{
				IDs.Add(i);
			}
			return Search(limit, parameters);*/
	}
}
