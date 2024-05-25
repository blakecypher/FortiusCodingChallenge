using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Fortius.CodingChallenge.Infratructure.Tests;

[TestClass]
public class SearchEngineTests : SearchEngineTestsBase
{

    [Fact]
    public void Test()
    {
        var shirts = new List<Shirt>
        {
            new("Red - Small", Size.Small, Color.Red),
            new("Black - Medium", Size.Medium, Color.Black),
            new("Blue - Large", Size.Large, Color.Blue),
        };

        var searchOptions = new SearchOptions
        {
            Colors = [Color.Red],
            Sizes = [Size.Small]
        };
        
        IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
        {
            { "cache_expiry_minutes", "5" },
            { "shirt_count", "200" }
        }).Build();
        
        ISearchEngine searchEngine = new SearchEngine(config, new MemoryCache(new MemoryCacheOptions()), shirts);
        var results = searchEngine.Search(searchOptions);

        AssertResultsContainOptions(results.Result.Shirts, searchOptions);
        AssertResultsContainsSizeCount(shirts, results.Result.SizeCounts);
        AssertResultsContainsColorCount(shirts, results.Result.ColorCounts);
    }
}