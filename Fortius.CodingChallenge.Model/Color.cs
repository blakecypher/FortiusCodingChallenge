using System.Text.Json.Serialization;

namespace Fortius.CodingChallenge;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Color
{
    Red,
    Blue,
    Yellow,
    White,
    Black
}

// Same again

// public class Color
// {
//     public static readonly Color Red = new(Guid.NewGuid(), "Red");
//     public static readonly Color Blue = new(Guid.NewGuid(), "Blue");
//     public static readonly Color Yellow = new(Guid.NewGuid(), "Yellow");
//     public static readonly Color White = new(Guid.NewGuid(), "White");
//     public static readonly Color Black = new(Guid.NewGuid(), "Black");
//
//     public static readonly List<Color> All =
//     [
//         Red,
//         Blue,
//         Yellow,
//         White,
//         Black
//     ];
//     
//     public Guid Id { get; }
//
//     public string Name { get; }
//
//     private Color(Guid id, string name)
//     {
//         Id = id;
//         Name = name;
//     }
// }