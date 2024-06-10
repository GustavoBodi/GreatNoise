namespace NoiseGenerator;

public class NoiseMap<T>
{
    private T[,] _map;

    private int _mapSize;

    private Func<T, T, T>? _method;

    public NoiseMap(int mapSize)
    {
        _mapSize = mapSize;
        _map = new T[mapSize, mapSize];
    }

    public NoiseMap<T> AddProcess(Func<T, T, T> fn) {
      _method = fn;
      return this;
    }

    public void GenerateNoise() {
          Parallel.For(0, _mapSize, y => {
              for (dynamic x = 0; x < _mapSize; ++x)
              {
                  _map[x,y] = _method(x, (dynamic)y);
              }
          });
    }

    public T[,] GetMapReference()
    {
        return _map;
    }
    
}
