using System.Runtime.InteropServices;
using System.Diagnostics;
using LibSasara;
using LibSasara.Model;
using CevioCasts;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;
using System.Text;

const string path = "..\\..\\TalkVoices\\EmotionCompare\\CeVIO_AI_Angry\\";

string jsonString = File.ReadAllText("../libs/cevio-casts/data/data.json");
Definitions defs = Definitions.FromJson(jsonString);

//キャスト一覧
defs.Casts
	.Where(c => c.Category is CevioCasts.Category.TextVocal)
	.ToList()
	.ForEach(c => Console.WriteLine($"[json]cast:[{c.Cname}] {c.Names.First(n => n.Lang == Lang.English).Display}"));

//labファイル一覧
var labs = Directory.EnumerateFiles(path, "*.lab");
labs.ToList().ForEach(l => Console.WriteLine($"{Path.GetFileName(l)}"));

var labData = await labs
	.ToAsyncEnumerable()
	.SelectAwait(async v =>
	{
		if (Path.Exists(v) is false) return default;
		var lab = await SasaraLabel.LoadAsync(v);
		return (name:Path.GetFileNameWithoutExtension(v), datalab:lab);
	})
	.Where(v => v.datalab is not null || v.datalab?.Lines?.Count() > 0)
	.ToHashSetAsync()
	;

labData.ToList().ForEach(lab => Console.WriteLine($"lab[{lab.name}]: {lab.datalab?.Lines?.Sum(v => v.Length)}"));

var phonemes = labData.First().datalab?.Lines?
	.Select(v => v.Phoneme)
	.OfType<string>();
var header = $"Name,{string.Join(",", phonemes!)}";
Console.WriteLine($"header: {header}");

var lines = labData.ToList().Select(lab =>
{
	var cast = lab.name;
	var lens = lab.datalab.Lines?
		.Select(v => v.Length.ToString(CultureInfo.InvariantCulture));
	var line = $"{cast},{string.Join(",", lens!)}";
	Console.WriteLine(line);
	return line;
});

var builder = new StringBuilder();
builder.AppendLine(header);
lines.ToList().ForEach(line => builder.AppendLine(line));

await File.WriteAllTextAsync("export.csv",builder.ToString());

//LibSasara.SasaraLabel.LoadAsync()