namespace NoiseGenerator;

public interface IPostProcessNoiseAlgorithms<TO>
{
    TO GenerateNoise(TO x, TO y, Func<double, double, double> fn);
}
