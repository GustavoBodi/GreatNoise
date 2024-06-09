namespace NoiseGenerator;

public interface INoiseAlgorithms<out TO, T>
where T: INoiseParameter
{
    void GenerateNoise();

    HeightMap GetNoiseMap();

    int GetMapSize();

}