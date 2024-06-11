namespace NoiseGenerator;

public interface INoiseAlgorithms<TO>
{
    TO GenerateNoiseOnPoint(TO x, TO y);
}
