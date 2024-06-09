using SixLabors.ImageSharp.PixelFormats;

namespace NoisePresentation;

public class NoiseColorFactory
{
    static Rgba32 DarkBlue  = new Rgba32(2,  43,  68 ); // dark blue: deep water
    static Rgba32 DeepBlue  = new Rgba32(9,  62,  92 ); // deep blue: water
    static Rgba32 Blue      = new Rgba32(17,  82, 112 ); // blue: shallow water
    static Rgba32 LightBlue = new Rgba32(69, 108, 118 ); // light blue: shore
    static Rgba32 Green     = new Rgba32(42, 102,  41 ); // green: grass
    static Rgba32 LightGreen= new Rgba32(115, 128,  77 ); // light green: veld
    static Rgba32 Brown     = new Rgba32(153, 143,  92 ); // brown: tundra
    static Rgba32 Grey      = new Rgba32(179, 179, 179 ); // grey: rocks
    static Rgba32 White     = new Rgba32(255, 255, 255 ); // white: snow

    static void GenerateColor(double height)
    {
        Rgba32 color;
    }
}