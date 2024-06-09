namespace NoiseGenerator;

public class HeightMap
{
    private double[,] _map;

    private int _mapSize;

    public HeightMap(int mapSize)
    {
        _mapSize = mapSize;
        _map = new double[mapSize, mapSize];
    }

    public double[,] GetMapReference()
    {
        return _map;
    }
    
}