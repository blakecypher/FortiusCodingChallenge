using Fortius.CodingChallenge.Infratructure;
using Fortius.CodingChallenge.SearchService.SampleData;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Fortius.CodingChallenge;

public class SearchEngine(
    IConfiguration config,
    IMemoryCache resultsCache,
    IEnumerable<Shirt> shirts,
    ISampleDataBuilder? sampleDataBuilder = null)
    : ISearchEngine
{
    private readonly GenericCache<SearchResults?> resultsCache = new(resultsCache, config);

    /// <summary>
    /// FETCH from data or GET from cache
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<SearchResults?> FetchOrGetResults(SearchOptions options)
    {
        return await resultsCache.GetOrCreate("search_results", async () => await Search(options));
    }

    public async Task<SearchResults?> Search(SearchOptions options)
    {
        if (!int.TryParse(config["shirt_count"], out var shirtCount))
        {
            throw new FormatException(
                $"The value of 'shirt_count' in the configuration is not a valid integer: {config["shirtCount"]}");
        }

        IEnumerable<Shirt> shirtResult = sampleDataBuilder == null ? shirts : await sampleDataBuilder?.CreateShirts(shirtCount);

        return new SearchResults(shirtResult.ToList());
    }
}