namespace NoiseGenerator;

public class PerlinNoiseParameter: INoiseParameter
{
    public PerlinNoiseParameter(bool usesBrownianMotion)
    {
        UsesBrownianMotion = usesBrownianMotion;
    }

    public bool UsesBrownianMotion { get; set; }

    public double Lacunarity { get; set; } = 0.5;

    public double Persistency { get; set; } = 2.0;

    public Frequency Frequency { get; set; } = new Frequency(0.01, 0.01);
}
