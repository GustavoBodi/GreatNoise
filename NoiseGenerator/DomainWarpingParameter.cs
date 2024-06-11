namespace NoiseGenerator;

public class DomainWarpingParameter: INoiseParameter {

    public INoiseAlgorithm<double> Fn { get; set; }

    public double AmplitudeX { get; set; } = 4;

    public double AmplitudeY { get; set; } = 4;

    public DomainWarpingParameter SetAmplitude(double amplitudex, double amplitudey) {
      AmplitudeX = amplitudex;
      AmplitudeY = amplitudey;
      return this;
    }

    public DomainWarpingParameter(INoiseAlgorithm<double> method)
    {
      Fn = method;
    }
}

