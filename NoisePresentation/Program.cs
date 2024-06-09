// See https://aka.ms/new-console-template for more information

using NoiseGenerator;
using NoisePresentation;

var perlin = new EnhancedPerlinNoise(1000, new PerlinNoiseParameter(true));
perlin.GenerateNoise();
HeightMap noiseMap = perlin.GetNoiseMap();
var drawer = new MapBitmapDrawer();
drawer.ToBitmap(noiseMap);
