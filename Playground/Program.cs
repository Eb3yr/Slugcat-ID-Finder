using IDFinder;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

var opt = new JsonSerializerOptions()
{
	IncludeFields = true
};

// We'll check args to avoid building two separate executables. This is stinky.
if (args.Length > 0)
{
	// Args will be (ID, startID, endIDInclusive, sParamsFile
	int ID = int.Parse(args[0]);
	int start = int.Parse(args[1]);
	int endIncl = int.Parse(args[2]);
	int numToStore = int.Parse(args[3]);
	string json = File.ReadAllText(args[4]);
	
	SearchParams sp = JsonSerializer.Deserialize<SearchParams>(json, opt) ?? throw new ArgumentNullException(nameof(args));
	EventHandler<SearchProgressEventArgs>? progEvent = null;
	progEvent += (sender, args) =>
	{
		Console.WriteLine($"{float.Round(args.Progress, 1)}%");
	};

	var result = Searcher.Search(start, endIncl, numToStore, sp, progressEventHandler: progEvent, progressInterval: 5000);

	File.WriteAllText($"finderdata/searcherRes{ID}.csv", JsonSerializer.Serialize(result, opt));
}
else
{
	Directory.CreateDirectory("finderdata");
	if (File.Exists("finderdata/searchparams.json"))
		File.Delete("finderdata/searchparams.json");

	foreach (var file in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "finderdata/searcherRes*.csv"))
		File.Delete(file);

	SearchParams sParams = new()
	{
		Scruffy = (1f, 1f),
		HeadColorL = (0.5f, 1f),
		HeadColorS = (1f, 1f),
		ReactionSkill = (1f, 0.2f),
		NumberOfSpines = (40, 0.25f),
		IsColored = (true, 0.4f),
	};
	
	string json = JsonSerializer.Serialize(sParams, opt);
	File.WriteAllText("finderdata/searchparams.json", json);

	string exeName = AppDomain.CurrentDomain.FriendlyName;

	int start = 0;
	int stopIncl = int.MaxValue / 64;
	int numToStore = 32;
	int threads = 12;

	Stopwatch watch = Stopwatch.StartNew();
	var chunks = Searcher.Chunker(start, stopIncl, threads);

	List<ProcessStartInfo> psi = [];
	for (int i = 0; i < chunks.Length; i++)
	{
		psi.Add(new ProcessStartInfo(exeName, [i.ToString(), chunks[i][0].ToString(), chunks[i][1].ToString(), numToStore.ToString(), "finderdata/searchparams.json"]));
	}

	List<Process> processes = [];
	foreach (var si in psi)
		processes.Add(Process.Start(si) ?? throw new NullReferenceException($"Startinfo null: {JsonSerializer.Serialize(si)}"));

	List<Task> ptasks = [];
	foreach (var p in processes)
		ptasks.Add(p.WaitForExitAsync());

	await Task.WhenAll(ptasks);
	watch.Stop();
	Console.WriteLine($"All tasks finished after {watch.ElapsedMilliseconds / 1000f}s");

	List<KeyValuePair<float, int>> results = [];
	for (int i = 0; i < processes.Count; i++)
	{
		List<KeyValuePair<float, int>> ll = JsonSerializer.Deserialize<List<KeyValuePair<float, int>>>(File.ReadAllText($"finderdata/searcherRes{i}.csv")) ?? throw new NullReferenceException();
		results.AddRange(ll);
	}

	results = results.OrderBy(kvp => kvp.Key).ThenBy(kvp => kvp.Value).ToList();
	if (results[1].Key < results[0].Key)
		throw new Exception($"Ordering is weird.\n{results.Select(x => JsonSerializer.Serialize(x)).Aggregate((cur, next) => cur is null ? next : $"{cur}\n{next}")}");

	results.RemoveRange(numToStore, results.Count - numToStore);

	foreach (var kvp in results)
		Console.WriteLine($"{kvp.Key}\t: {kvp.Value}");

	Console.ReadLine();
}

// SearchParams sParams = new()
// {
// Scruffy = (1f, 1f),
// HeadColorL = (0.5f, 1f),
// HeadColorS = (1f, 1f),
// ReactionSkill = (1f, 0.2f),
// NumberOfSpines = (40, 0.25f),
// IsColored = (true, 0.4f),
// };
// 
// EventHandler<SearchProgressEventArgs>? progEvent = null;
// progEvent += (sender, args) =>
// {
// Console.WriteLine($"{float.Round(args.Progress, 1)}%");
// };
// 
// Stopwatch watch = new();
// watch.Start();
// var result = Searcher.SearchThreaded(0, int.MaxValue / 256, 32, Environment.ProcessorCount, sParams, true, progressEventHandler: progEvent, progressInterval: 5_000);
// watch.Stop();
// Console.WriteLine(watch.ElapsedMilliseconds / 1000f + "s");
// 
// foreach (var i in result)
// {
// Console.WriteLine(i.Value + ": " + i.Key);
// }
// 
// Console.ReadLine();