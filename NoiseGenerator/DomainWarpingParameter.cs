namespace NoiseGenerator;

public class DomainWarpingParameter: INoiseParameter {

    public Func<double, double, double> Fn { get; set; }

    public DomainWarpingParameter(Func<double, double, double> method)
    {
      Fn = method;
    }
}

