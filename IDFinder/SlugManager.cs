using MemoryPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
	public class SlugManager
	{
		public Dictionary<int, Slugcat> Cats { get; private set; }
		public SlugManager()
		{
			Cats = new();
		}
		public SlugManager(IEnumerable<Slugcat> Cats) : this()
		{
			foreach (Slugcat sc in Cats)
			{
				this.Cats.Add(sc.ID, sc);
			}
		}
		public SlugManager(IEnumerable<int> IDs) : this()
		{
			Add(IDs);
		}
		public void Add(int ID)
		{
			Cats.Add(ID, new(ID));
		}
		public void Add(IEnumerable<int> IDs)
		{
			foreach (int ID in IDs) { Add(ID); }
		}
		public void Remove(int ID)
		{
			Cats.Remove(ID);
		}
		public void WriteToCSV(string fileName, SelectedColumns cols, bool delete = true)
		{
			bool exists = File.Exists(fileName);
			if (exists && delete) File.Delete(fileName);

			if (cols == null) cols = new SelectedColumns();
			using (StreamWriter sw = new StreamWriter(fileName, true))
			{
				NPCStats s;
				Personality p;
				Dictionary<Food, float> fp;
				string wr = "";
				List<string> desiredCols = cols.GetDesiredColumnVals();
				// I don't think GetProperties is suitable here, as DataColumnNames doesn't actually have properties, just constant fields. 
				foreach (FieldInfo prop in typeof(ColumnNames).GetFields())
				{
					if (desiredCols.Contains(prop.Name))
					{
						wr += prop.GetRawConstantValue() + ",";
					}
				}
				if (wr.EndsWith(',')) wr = wr.Remove(wr.Length - 1);
				if (!exists || delete) sw.WriteLine(wr);

				foreach (Slugcat sc in Cats.Values)
				{
					wr = "";
					s = sc.Stats;
					p = sc.Personality;
					fp = sc.FoodPreference;

					Dictionary<string, PropertyInfo> sP = new();
                    foreach (PropertyInfo pi in typeof(NPCStats).GetProperties())
                    {
                        sP.Add(pi.Name, pi);
                    }
					Dictionary<string, PropertyInfo> pP = new();
					foreach (PropertyInfo pi in typeof(Personality).GetProperties())
					{
						pP.Add(pi.Name, pi);
					}

                    foreach (string c in desiredCols)
					{
						if (sP.ContainsKey(c))
						{
							wr += sP[c].GetValue(s) + ",";
							continue;
						}
						if (pP.ContainsKey(c))
						{
							wr += pP[c].GetValue(p) + ",";
							continue;
						}
						if (c == "ID")
						{
							wr += sc.ID + ",";
							continue;
						}
						bool exit = false;
						foreach (KeyValuePair<Food, float> kp in fp)
						{
							if (kp.Key.ToString() == c)
							{
								wr += fp[kp.Key] + ",";
								exit = true;
								break;
							}
						}
						if (exit) continue;
					}
					if (wr.EndsWith(',')) wr = wr.Remove(wr.Length - 1);

					sw.WriteLine(wr);
				}
				/*foreach (KeyValuePair<int, NPCStats> kp in stats)
				{
					s = kp.Value;
					p = s.Personality;
					wr = $"{kp.Key},{p.Aggression},{p.Bravery},{p.Dominance},{p.Energy},{p.Nervous},{p.Sympathy},,{s.H},{s.S},{s.L},{s.Dark},{s.EyeColor},,{s.Size},{s.Wideness}";
					foreach (float val in foodPreference.Values)
					{
						// fundamental problem here. SlugData class groups methods for handling multiple NPCStats. foodPreference should NOT be here, it should be in NPCStats, or in a class which
						// has a single NPCStats and foodPreference. Like a "Slugcat" class, and rename SlugData to a more suitable name
					}
					sw.WriteLine(wr);
				}*/
			}
		}
	}
}
