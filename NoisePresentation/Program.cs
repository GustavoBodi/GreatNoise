// See https://aka.ms/new-console-template for more information

using NoiseGenerator;
using NoisePresentation;

var perlinList = new List<EnhancedPerlinNoise>();
for (var i = 0; i < 10; ++i) {
  perlinList.Add(new EnhancedPerlinNoise(new PerlinNoiseParameter().SetShouldShuffle(true)));
}
var perlin = new EnhancedPerlinNoise(new PerlinNoiseParameter().SetShouldShuffle(true));
var brownian = new BrownianMotion(new BrownianMotionParameter(perlin).SetOctaves(12).SetLacunarity(0.57).SetPersistency(2.2));
var domainWarping = new DomainWarping(new DomainWarpingParameter(brownian).SetAmplitude(7, 12));
var voronoi = new Voronoi(new VoronoiParameter(perlin));
var dla = new DlaNoiseCompositor(perlin).AddAlgorithms(perlinList);
var dla_brownian = new BrownianMotion(new BrownianMotionParameter(dla).SetOctaves(12).SetLacunarity(0.57).SetPersistency(2.2));
var dla_brownian_warping = new DomainWarping(new DomainWarpingParameter(dla_brownian).SetAmplitude(7, 12));
var map = new NoiseMap<double>(1000, dla_brownian);
map.GenerateNoise();
var drawer = new MapBitmapDrawer();
drawer.ToBitmap(map, false);
