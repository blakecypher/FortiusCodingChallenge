using System.Text.Json.Serialization;

namespace Fortius.CodingChallenge;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Size
{
    Small,
    Medium,
    Large
}

// Someone doesn't understand what enums are for ?

// public class Size
// {
//     public static readonly Size Small = new(Guid.NewGuid(), "Small");
//     public static readonly Size Medium = new(Guid.NewGuid(), "Medium");
//     public static readonly Size Large = new(Guid.NewGuid(), "Large");
//
//     public Guid Id { get; }
//
//     public string Name { get; }
//
//     public static readonly List<Size> All =
//     [
//         Small,
//         Medium,
//         Large
//     ];
//
//     private Size(Guid id, string name)
//     {
//         Id = id;
//         Name = name;
//     }
// }