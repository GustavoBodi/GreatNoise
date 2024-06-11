namespace NoiseGenerator;

public class DomainWarpingParameter: INoiseParameter {

    public INoiseAlgorithms<double> Fn { get; set; }

    public double AmplitudeX { get; set; } = 4;

    public double AmplitudeY { get; set; } = 4;

    public DomainWarpingParameter SetAmplitude(double amplitudex, double amplitudey) {
      AmplitudeX = amplitudex;
      AmplitudeY = amplitudey;
      return this;
    }

    public DomainWarpingParameter(INoiseAlgorithms<double> method)
    {
      Fn = method;
    }
}

