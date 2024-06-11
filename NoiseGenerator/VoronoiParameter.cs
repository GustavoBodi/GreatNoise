namespace NoiseGenerator;

public class VoronoiParameter : INoiseParameter
{
    public INoiseAlgorithm<double> Fn { get; set; }

    public VoronoiParameter(INoiseAlgorithm<double> fn)
    {
      Fn = fn;
    }
}

