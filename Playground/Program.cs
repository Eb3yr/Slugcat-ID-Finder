using IDFinder;
using System.Diagnostics;
using System.IO;

ScavParams sParams = new()
{
	//EyeColorH = (0.031835206f, 1f),
	//EyeColorL = (0.34901962f, 1f),
	EyeColorH = (0.106145255f, 1f),
	EyeColorL = (0.6490196f, 1f),
	DeepPupils = (false, 1000f),
	ColoredEartlerTips = (true, 1000f),
	//ColorType = (BackTuftsAndRidges.ColorTypeEnum.Decoration, -10f),

	//HeadColorH = (0.7142857f, 1f),
	//HeadColorS = (0.43749994f, 1f),
	//HeadColorL = (0.0627451f, 1f),
	//
	//BodyColorH = (0.72727275f, 1f),
	//BodyColorS = (0.23404254f, 1f),
	//BodyColorL = (0.092156865f, 1f),

	GeneralSize = (1f, 1f),
};

Stopwatch watch = new();
watch.Start();
var result = Searcher.SearchThreaded(0, 100_000_000, 256, 12, sParams, true, true);
watch.Stop();
Console.WriteLine(watch.ElapsedMilliseconds / 1000f + "s");
//using (StreamWriter sw = new(new FileStream("scavOutput.txt", FileMode.Create)))
//{
//	foreach (var i in result)
//	{
//		Console.WriteLine(i.Value + ": " + i.Key);
//		sw.WriteLine("at <cursor> spawn Scavenger ID.-1." + i.Value);
//	}
//}

Console.ReadLine();

// TODO: Go through and make all structs being weighted readonly structs. There shouldn't be any reason to not do so