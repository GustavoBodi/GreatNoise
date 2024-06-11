namespace NoiseGenerator;

public class BrownianMotion: INoiseAlgorithms<double>
{
    public BrownianMotionParameter _parameters;

    public BrownianMotion(BrownianMotionParameter parameters)
    {
      _parameters = parameters;
    }

    public double GenerateNoiseOnPoint(double x, double y)
    {
        var result = 0.0;
        var amplitude = 1.0;
        var frequency = 1.0;

        for (var octave = 0; octave < _parameters.Octaves; ++octave)
        {
            result += amplitude * _parameters.NoiseFn.GenerateNoiseOnPoint(x * frequency, y * frequency);

            amplitude *= _parameters.Lacunarity;
            frequency *= _parameters.Persistency;
        }

        return result;
    }
}
