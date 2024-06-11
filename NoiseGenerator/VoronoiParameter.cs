namespace NoiseGenerator;

public class VoronoiParameter : INoiseParameter
{
    public INoiseAlgorithms<double> Fn { get; set; }

    public VoronoiParameter(INoiseAlgorithms<double> fn)
    {
      Fn = fn;
    }
}

