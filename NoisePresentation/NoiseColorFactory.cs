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

    public NoiseColorFactory()
    {
    }

    public Rgba32 GenerateColor(double height)
    {
        if (height < 0.10) return new Rgba32(32, 77, 117); // Deep Ocean
        else if (height < 0.2) return new Rgba32(54, 122, 189); // Shallow Water
        else if (height < 0.3) return new Rgba32(118, 165, 175); // Beach
        else if (height < 0.4) return new Rgba32(194, 178, 128); // Sand
        else if (height < 0.6) return new Rgba32(141, 178, 85); // Grassland
        else if (height < 0.75) return new Rgba32(34, 139, 34); // Forest Green
        else if (height < 0.98) return new Rgba32(126, 94, 96); // Mountain Range
        else return new Rgba32(255, 248, 240); // Snow Peaks
    }
}
