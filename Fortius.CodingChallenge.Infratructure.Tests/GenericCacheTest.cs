using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Fortius.CodingChallenge.Infratructure.Tests;

[TestClass]
public class GenericCacheTests
{
    [Fact]
    public void Constructor_WhenCacheIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        IMemoryCache cache = null;
        IConfiguration config = new ConfigurationBuilder().Build();
        
        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => new GenericCache<SearchResults[]>(cache, config));
    }
    
    [Fact]
    public void Constructor_WhenConfigIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        using IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        IConfiguration config = null;
        
        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => new GenericCache<SearchResults[]>(cache, config));
    }
    
    [Fact]
    public void Constructor_WhenCacheExpiryMinutesIsNotValidInteger_ShouldThrowFormatException()
    {
        // Arrange
        using IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
        {
            { "cache_expiry_minutes", "abc" }
        }).Build();
        
        // Act and Assert
        Assert.ThrowsException<FormatException>(() => new GenericCache<SearchResults[]>(cache, config));
    }
    
    [Fact]
    public void Constructor_WhenAllInputsAreValid_ShouldSetCacheOptions()
    {
        // Arrange
        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
        {
            { "cache_expiry_minutes", "5" }
        }).Build();
        
        // Act
        using var genericCache = new GenericCache<SearchResults[]>(cache, config);
        
        // Assert
        Assert.IsNotNull(genericCache.cacheOptions);
    }
}