using Unity_XORShift;
using MemoryPack;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace IDFinder
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
			//foreach (FieldInfo fi in typeof(DataColumnNames).GetFields())
			//{
			//	Console.WriteLine($"{fi.Name}, {fi.GetRawConstantValue()}");
			//}

			SlugManager manager = new(Enumerable.Range(1000, 20000));
			SelectedColumns sdc = SelectedColumns.Add([SelectedColumns.IDOnly(), SelectedColumns.PersonalityOnly(), SelectedColumns.StatsOnly()]);
			manager.WriteToCSV("Test.csv", sdc, true);
			Console.WriteLine("Done");
			Console.ReadLine();

			#region LargeGen
			//SlugData data = SlugData.ReturnGenerateBounds(int.MinValue, int.MaxValue);
			SlugManagerOld data = new();
			//int x = (int)Math.Pow(2, 19);
			DateTime now;
			Console.Write("Start ID: ");
			int startID, endID;
			int.TryParse(Console.ReadLine(), out startID);
			Console.Write("End ID: ");
			int.TryParse(Console.ReadLine(), out endID);

			// Should only run once in this config, can't be arsed to write more
			for (int i = startID; i < endID; i += endID)
			{
				now = DateTime.Now;
				data = SlugManagerOld.ReturnGenerateBounds(i, endID);
				data.WriteToCSV("data.csv", false);
				data = new();
				Console.WriteLine("Writing from i = " + i + ", Δtime = " + DateTime.Now.Subtract(now).TotalMilliseconds);
			}

			data.Add(int.MaxValue);
			data.WriteToCSV("data.csv", false);

			Console.WriteLine("ended");
			Console.ReadLine();
			#endregion
		}

		static List<NPCStats> GenerateNPCStats(int start, int stop)
		{
			List<NPCStats> ls = new();

			for (int i = start; i < stop; i++)
			{
				ls.Add(new(i));
			}
			return ls;
		}
		static void WriteSlugData(string fileName, SlugManagerOld stats)
		{
			byte[] data = MemoryPackSerializer.Serialize<SlugManagerOld>(stats);
			File.WriteAllBytes(fileName, data);
		}
		static SlugManagerOld ReadSlugData(string fileName)
		{
			Console.WriteLine("1");
			byte[] data = File.ReadAllBytes(fileName);
			Console.WriteLine("2");
			SlugManagerOld? ls = MemoryPackSerializer.Deserialize<SlugManagerOld>(data);
			Console.WriteLine("3");
			return ls;
		}
		static void GeneratePersonalities(int start, int stop)
		{
			Dictionary<int, Personality> list = new();
			for (int i = start; i < stop; i++)
			{
				list.Add(i, new Personality(i));
			}
			WriteLists(list, "Personalities.csv");
		}
		static void WriteLists(Dictionary<int, Personality> list, string fileName, FileMode fileMode = FileMode.Append)
		{
			string line;
			bool exists = true;
			//if (!File.Exists(fileName)) exists = false;
			using (StreamWriter sw = new(new FileStream(fileName, fileMode)))
			{
				if (exists) sw.WriteLine("id,aggression,bravery,dominance,energy,nervous,sympathy");
				foreach (KeyValuePair<int, Personality> kp in list)
				{
					line = $"{kp.Key},{kp.Value.Aggression},{kp.Value.Bravery},{kp.Value.Dominance},{kp.Value.Energy},{kp.Value.Nervous},{kp.Value.Sympathy}";
					sw.WriteLine(line);
				}
				sw.WriteLine("-,-,-,-,-,-,-");
			}
		}
	}
}
