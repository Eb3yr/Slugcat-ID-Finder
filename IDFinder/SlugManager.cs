using System.Reflection;

namespace IDFinder
{
	public class SlugManager
	{
		public Dictionary<int, Slugcat> Scugs { get; private set; }
		public SlugManager()
		{
			Scugs = new();
		}
		public SlugManager(IEnumerable<Slugcat> Cats) : this()
		{
			foreach (Slugcat sc in Cats)
			{
				this.Scugs.Add(sc.ID, sc);
			}
		}
		public SlugManager(IEnumerable<int> IDs) : this()
		{
			Add(IDs);
		}
		public void Add(int ID)
		{
			Scugs.Add(ID, new(ID));
		}
		public void Add(IEnumerable<int> IDs)
		{
			foreach (int ID in IDs) { Add(ID); }
		}
		public void Remove(int ID)
		{
			Scugs.Remove(ID);
		}
		public void WriteToCSV(string fileName, SelectedColumns cols, bool delete = true)
		{
			bool exists = File.Exists(fileName);
			if (exists && delete) File.Delete(fileName);

			cols ??= new SelectedColumns();
			using (StreamWriter sw = new(fileName, true))
			{
				NPCStats s;
				Personality p;
				FoodPreferences f;
				string wr = "";
				List<string> desiredCols = cols.GetDesiredColumnVals();

				foreach (FieldInfo prop in typeof(ColumnNames).GetFields())
				{
					if (desiredCols.Contains(prop.Name))
					{
						wr += prop.GetRawConstantValue() + ",";
					}
				}
				if (wr.EndsWith(',')) wr = wr.Remove(wr.Length - 1);
				if (!exists || delete) sw.WriteLine(wr);

				foreach (Slugcat sc in Scugs.Values)
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
						if (sP.TryGetValue(c, out PropertyInfo? value))
						{
							wr += value.GetValue(s) + ",";
							continue;
						}
						if (pP.TryGetValue(c, out value))
						{
							wr += value.GetValue(p) + ",";
							continue;
						}
						if (fP.TryGetValue(c, out value))
						{
							wr += value.GetValue(f) + ",";
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
