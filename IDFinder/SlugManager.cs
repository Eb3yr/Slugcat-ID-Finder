using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
				FoodPreferences f;
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
					f = sc.FoodPreferences;

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
					Dictionary<string, PropertyInfo> fP = new();
					foreach (PropertyInfo pi in typeof(FoodPreferences).GetProperties())
					{
						fP.Add(pi.Name, pi);
					}
					bool flag = false;
                    foreach (string c in desiredCols)
					{
						if (!flag && c == "ID")
						{
							wr += sc.ID + ",";
							flag = true;
							continue;
						}
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
						if (fP.ContainsKey(c))
						{
							wr += fP[c].GetValue(p) + ",";
							continue;
						}
						throw new Exception("Unknown desired column");
					}
					if (wr.EndsWith(',')) wr = wr.Remove(wr.Length - 1);

					sw.WriteLine(wr);
				}
			}
		}
	}
}
