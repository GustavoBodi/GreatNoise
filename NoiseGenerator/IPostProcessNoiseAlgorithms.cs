namespace NoiseGenerator;

public interface IPostProcessNoiseAlgorithms<TO, T>
where T: INoiseParameter
{
    TO GenerateNoise(TO x, TO y, Func<double, double, double> fn);
}
