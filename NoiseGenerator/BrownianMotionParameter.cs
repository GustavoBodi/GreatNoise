namespace NoiseGenerator;

public class BrownianMotionParameter: INoiseParameter
{
    public double Lacunarity { get; set; } = 0.5;

    public double Persistency { get; set; } = 2.0;

    public double Octaves { get; set; } = 8;

    public Func<double, double, double> NoiseFn { get; set; }

    public BrownianMotionParameter(Func<double, double, double> noiseFn)
    {
      NoiseFn = noiseFn;
    }

    public BrownianMotionParameter SetLacunarity(double lacunarity) {
      Lacunarity = lacunarity;
      return this;
    }

    public BrownianMotionParameter SetOctaves(int octaves) {
      Octaves = octaves;
      return this;
    }

    public BrownianMotionParameter SetPersistency(double persistency) {
      Persistency = persistency;
      return this;
    }
    
}
