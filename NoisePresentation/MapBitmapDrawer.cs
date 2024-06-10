using System.Drawing.Imaging;
using NoiseGenerator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = System.Drawing.Color;

namespace NoisePresentation;

public unsafe class MapBitmapDrawer
{
    public MapBitmapDrawer()
    {
    }

    public void ToBitmap(NoiseMap<double> noiseMap)
    {
        var rawImage = noiseMap.GetMapReference();
        int width = rawImage.GetLength(1);
        int height = rawImage.GetLength(0);

        using (var image = new Image<Rgba32>(width, height))
        {
            // Loop through the array and set pixel colors
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Assuming the values in the array represent color intensities (0-255)
                    var n = rawImage[x, y];
                    n += 1.0;
                    n *= 0.5;

                    int c = (int)Math.Round(255 * n);
                    if (c > 255) {
                      c = 255;
                    } else if (c < 0) {
                      c = 0;
                    }

                    var colorNum = (byte)c;
                    
                    var color = new Rgba32(colorNum, colorNum, colorNum, 255);
                    image[x, y] = color;
                }
            }

            // Save the image to a PNG file
            image.Save("output.png");
        }
    }
}
