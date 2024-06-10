// See https://aka.ms/new-console-template for more information

using NoiseGenerator;
using NoisePresentation;

var map = new NoiseMap<double>(1000);
var perlin = new EnhancedPerlinNoise(new PerlinNoiseParameter().SetShouldShuffle(true));
var brownian = new BrownianMotion(new BrownianMotionParameter(perlin.GenerateNoiseOnPoint).SetOctaves(12));
var domainWarping = new DomainWarping(new DomainWarpingParameter(brownian.GenerateNoiseOnPoint));
map.AddProcess(domainWarping.GenerateNoiseOnPoint);
map.GenerateNoise();
var drawer = new MapBitmapDrawer();
drawer.ToBitmap(map);
