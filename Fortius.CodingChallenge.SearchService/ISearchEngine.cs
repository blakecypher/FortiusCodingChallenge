namespace Fortius.CodingChallenge;

public interface ISearchEngine
{
    Task<SearchResults?> Search(SearchOptions options);
    Task<SearchResults?> FetchOrGetResults(SearchOptions options);
}