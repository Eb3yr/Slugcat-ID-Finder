using System.Reflection;
using System.Text.Json;

namespace IDFinder
{
	public class SlugManager
	{
		public Slugcat? this[int ID]
		{
			get
			{
				if (!Slugcats.TryGetValue(ID, out Slugcat? value))
					return null;
				return value;
			}
		}
		private readonly static JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
		public Dictionary<int, Slugcat> Slugcats { get; private set; }
		public SlugManager()
		{
			Slugcats = new();
		}
		public SlugManager(IEnumerable<Slugcat> Cats) : this()
		{
			foreach (Slugcat sc in Cats)
			{
				Slugcats.Add(sc.ID, sc);
			}
		}
		public SlugManager(IEnumerable<int> IDs) : this()
		{
			Add(IDs);
		}
		public void Add(int ID)
		{
			Slugcats.Add(ID, new(ID));
		}
		public void Add(IEnumerable<int> IDs)
		{
			foreach (int ID in IDs) { Add(ID); }
		}
		public void Remove(int ID)
		{
			Slugcats.Remove(ID);
		}
		public void WriteToCSV(string fileName, SelectedColumns? cols = null, bool delete = true)
		{
			if (cols == null) cols = SelectedColumns.All;
			bool exists = File.Exists(fileName);
			if (exists && delete) File.Delete(fileName);

			cols ??= new SelectedColumns();
            using (StreamWriter sw = new(fileName, true))
			{
				NPCStats n;
				SlugcatStats s;
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

				foreach (Slugcat sc in Slugcats.Values)
				{
					wr = "";
					n = sc.NPCStats;
					s = sc.SlugcatStats;
					p = sc.Personality;
					f = sc.FoodPreferences;

					Dictionary<string, PropertyInfo> nP = new();
                    foreach (PropertyInfo pi in typeof(NPCStats).GetProperties())
                    {
                        nP.Add(pi.Name, pi);
                    }
					Dictionary<string, PropertyInfo> sP = new();
					foreach (PropertyInfo pi in typeof(SlugcatStats).GetProperties())
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
						if (nP.TryGetValue(c, out PropertyInfo? value))
						{
							wr += value.GetValue(n) + ",";
							continue;
						}
						if (sP.TryGetValue(c, out value))
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
		public static string GetJsonMany(IEnumerable<int> IDs)
		{
			List<Slugcat> scugs = new();
			foreach (int ID in IDs)
			{
				scugs.Add(new(ID));
			}
			return JsonSerializer.Serialize<List<Slugcat>>(scugs, options);
		}
        public string GetJsonThis()
        {
            return JsonSerializer.Serialize(this, options);
        }
        public static string GetJson(int ID)
		{
			return JsonSerializer.Serialize<Slugcat>(new(ID), options);
		}
	}
}
