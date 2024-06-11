namespace NoiseGenerator;

public interface INoiseAlgorithm<TO>
{
    TO GenerateNoiseOnPoint(TO x, TO y);
}
