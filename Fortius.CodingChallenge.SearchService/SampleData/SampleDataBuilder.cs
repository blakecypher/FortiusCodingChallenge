namespace Fortius.CodingChallenge.SearchService.SampleData;

public class SampleDataBuilder : ISampleDataBuilder
{
    private readonly Random _random = new();

    public async Task<IEnumerable<Shirt>?> CreateShirts(int numberOfShirts)
    {
        return await Task.Run(() =>
        {
            var results = new List<Shirt>();

            for (var i = 0; i < numberOfShirts; i++)
            {
                var randomColor = GetRandomColor();
                var randomSize = GetRandomSize();
                results.Add(new Shirt($"{randomColor} - {randomSize}", randomSize, randomColor));
            }
            return results;
        });
    }
       
    private Size GetRandomSize()
    {
        var sizes = Enum.GetValues(typeof(Size));
        var index = _random.Next(0, sizes.Length);
        return (Size)sizes.GetValue(index)!;
    }

    private Color GetRandomColor()
    {
        var sizes = Enum.GetValues(typeof(Color));
        var index = _random.Next(0, sizes.Length);
        return (Color)sizes.GetValue(index)!;
    }
}

public interface ISampleDataBuilder
{
    Task<IEnumerable<Shirt>?> CreateShirts(int numberOfShirts);
}