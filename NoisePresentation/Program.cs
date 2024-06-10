// See https://aka.ms/new-console-template for more information

using NoiseGenerator;
using NoisePresentation;

var map = new NoiseMap<double>(700);
var perlin = new EnhancedPerlinNoise(new PerlinNoiseParameter().SetShouldShuffle(true));
var brownian = new BrownianMotion(new BrownianMotionParameter(perlin.GenerateNoiseOnPoint).SetOctaves(12).SetLacunarity(0.57).SetPersistency(2.2));
var domainWarping = new DomainWarping(new DomainWarpingParameter(brownian.GenerateNoiseOnPoint).SetAmplitude(7, 12));
var voronoi = new Voronoi(new VoronoiParameter(perlin.GenerateNoiseOnPoint));
map.AddProcess(domainWarping.GenerateNoiseOnPoint);
map.GenerateNoise();
var drawer = new MapBitmapDrawer();
drawer.ToBitmap(map, false);
