namespace NoiseGenerator;

public class PerlinNoiseParameter: INoiseParameter
{
    public PerlinNoiseParameter()
    {
    }

    public PerlinNoiseParameter SetFrequency(Frequency frequency) {
      Frequency = frequency;
      return this;
    }

    public PerlinNoiseParameter SetShouldShuffle(bool shouldShuffle) {
      ShouldShuffle = shouldShuffle;
      return this;
    }

    public bool ShouldShuffle { get; set; } = false;

    public Frequency Frequency { get; set; } = new Frequency(0.01, 0.01);
}
