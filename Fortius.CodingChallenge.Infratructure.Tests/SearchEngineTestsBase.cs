using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Fortius.CodingChallenge.Infratructure.Tests;

public class SearchEngineTestsBase
{
    protected static void AssertResultsContainOptions(List<Shirt> shirts, SearchOptions options)
    {
        Assert.That(shirts, Is.Not.Null);

        var found = false;
        foreach (var shirt in shirts)
        {
            if (options.Sizes.Contains(shirt.Size) && options.Colors.Contains(shirt.Color))
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            Assert.That(actual: found, Is.True, $"'{string.Join(",", options.Sizes.Select(s => s))}' and '{string.Join(",", options.Colors.Select(c => c))}' found in results");
            Console.WriteLine(string.Join(",", options.Sizes.Select(s => s),",",$"{string.Join(" +", options.Colors.Select(c => c), "found in results")}"));
        }
        else
        {
            Assert.Fail($"'{string.Join(",", options.Sizes.Select(s => s))}' and '{string.Join(",", options.Colors.Select(c => c))}' not found in results");
        }
    }

    protected static void AssertResultsContainsSizeCount(IEnumerable<Shirt> shirts,
        List<SizeCount> sizeCounts)
    {
        Assert.That(sizeCounts, Is.Not.Null);

        foreach (Size size in Enum.GetValues(typeof(Size)).Cast<Size>()) // Cast allows for enum comparison
        {
            /*
               FOD is faster than SOD (FirstOrDefault or SingleOrDefault) as SOD raises an exception if it finds more than one
               and has to enumerate the whole collection whereas FOD doesn't
               However Find is always faster!
            */

            var sizeCount = sizeCounts.Find(s => s.Size == size);
            Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size}' not found in results");

            var expectedSizeCount = shirts.Count(s => s.Size == size);

            Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount),
                $"Size count for '{sizeCount.Size}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
        }
    }

    protected static void AssertResultsContainsColorCount(List<Shirt> shirts,
        List<ColorCount> colorCounts)
    {
        Assert.That(colorCounts, Is.Not.Null);

        foreach (var shirt in shirts)
        {
            var colorCount = colorCounts.Find(c => c.Color == shirt.Color);
            Assert.That(colorCount, Is.Not.Null, $"Color count for '{shirt.Color}' not found in results");
            
            var expectedColorCount = shirts.Count(s => s.Color == shirt.Color); 
            
            Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount), $" Size count for '{colorCount.Color}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
        }
    }
}