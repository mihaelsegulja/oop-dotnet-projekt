namespace WpfApp.Models;

public readonly struct Resolution
{
    public double Width { get; }
    public double Height { get; }

    public Resolution(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override bool Equals(object? obj) =>
        obj is Resolution other && Width == other.Width && Height == other.Height;

    public override int GetHashCode() => HashCode.Combine(Width, Height);
}

public static class Resolutions
{
    public static readonly Resolution Res800x600 = new(800, 600);
    public static readonly Resolution Res1000x800 = new(1000, 800);
    public static readonly Resolution Res1200x900 = new(1200, 900);
}
