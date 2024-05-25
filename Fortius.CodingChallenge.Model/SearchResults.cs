namespace Fortius.CodingChallenge;

public class SearchResults(List<Shirt> shirts)
{
    public List<Shirt> Shirts { get; } = shirts;

    public List<SizeCount> SizeCounts { get; } =
        shirts.GroupBy(x => x.Size).Select(x => new SizeCount(x.Key, x.Count())).ToList();

    public List<ColorCount> ColorCounts { get; } =
        shirts.GroupBy(x => x.Color).Select(x => new ColorCount(x.Key, x.Count())).ToList();
}