namespace Fortius.CodingChallenge;

public class Shirt(string name, Size size, Color color)
{
    public string Name { get; } = name;

    public Size Size { get; } = size;

    public Color Color { get; } = color;
}