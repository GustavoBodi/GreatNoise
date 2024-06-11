namespace NoiseGenerator;

public class NoiseMap<F>
{
    private F[,] _map;

    private int _mapSize;

    private INoiseAlgorithm<F> _algorithm;

    public NoiseMap(int mapSize, INoiseAlgorithm<F> algorithm)
    {
        _algorithm = algorithm;
        _mapSize = mapSize;
        _map = new F[mapSize, mapSize];
    }

    public void GenerateNoise() {
          Parallel.For(0, _mapSize, y => {
              Parallel.For(0, _mapSize, x => {
                  _map[x,y] = _algorithm.GenerateNoiseOnPoint((dynamic)x, (dynamic)y);
              });
          });
    }

    public F[,] GetMapReference()
    {
        return _map;
    }
    
}
