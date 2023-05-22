using System.Diagnostics;
using LibSasara;
using LibSasara.Model;
using CevioCasts;
using System.Text.RegularExpressions;

const string path = "..\\..\\SongVoices\\AlphaValueCheck\\AlphaValueCheck.ccs";
//CeVIOFileBase ccs = await SasaraCcs.LoadAsync(path);

string jsonString = File.ReadAllText("../libs/cevio-casts/data/data.json");
Definitions defs = Definitions.FromJson(jsonString);

//キャスト一覧
defs.Casts
	.Where(c => c.Category is CevioCasts.Category.SingerSong)
	.ToList()
	.ForEach(c => Console.WriteLine($"[json]cast:[{c.Cname}] {c.Names.First(n => n.Lang == Lang.English).Display}"));

//List<TrackSet<SongUnit>> tracks = ccs.GetTrackSets<SongUnit>();

//トラック一覧
//tracks.ForEach(v => Console.WriteLine($"cast:{v.CastId}, {v.Name}"));

Cast sasara = defs.Casts
	.Where(c => c.Category is CevioCasts.Category.SingerSong && c.Product is Product.CeVIO_AI)
	.First(c => c.Names[0].Display is "さとうささら");
//IEnumerable<TrackSet<SongUnit>> template = tracks
//	.Where(c => c.CastId == sasara.Cname);

List<(string cid, string name)> castIds = defs.Casts
	.Where(c =>
		c.Category is CevioCasts.Category.SingerSong
			&& c.Product is Product.CeVIO_AI
	)
	.Select(c => (
		cid: c.Cname,
		name: c.Names.First(n => n.Lang == Lang.English).Display
	))
	.ToList()
	;

Regex max = MaxRegex();
Regex min = MinRegex();

foreach (var cast in castIds)
{
	var ccs = await SasaraCcs.LoadAsync(path);
	var tracks = ccs.GetTrackSets<SongUnit>();

	foreach (var tr in tracks)
	{
		var nid = Guid.NewGuid();
		var dup = await ccs.DuplicateAndAddTrackSetAsync(tr.GroupId, nid);

		dup.ReplaceAllCastId(cast.cid);

		var current = dup.group.Attribute("Name")?.Value ?? "";
		Debug.WriteLine($"tracname: {current}");

		var nname = cast.name;
		var newname = "";
		if (max.IsMatch(current))
		{
			newname = $"{nname}_max";
		}
		else if (min.IsMatch(current))
		{
			newname = $"{nname}_min";
		}
		else
		{
			newname = $"{nname}";
		}

		Console.WriteLine($"newtrack: {newname}");

		ccs.RawGroups
			.Find(x => new Guid(x.Attribute("Id")?.Value!) == nid)!
			.SetAttributeValue("Name", newname);
	}

	var dir = Path.GetDirectoryName(path)!;
	var file = Path.GetFileNameWithoutExtension(path);
	var newFile = $"{file}_{cast.name.Replace(" ", "_")}.ccs";
	var savePath = Path.Combine(dir, newFile);
	await ccs.SaveAsync(savePath);
}

partial class Program
{
	[GeneratedRegex("_max$", RegexOptions.Compiled)]
	private static partial Regex MaxRegex();

	[GeneratedRegex("_min$", RegexOptions.Compiled)]
	private static partial Regex MinRegex();
}