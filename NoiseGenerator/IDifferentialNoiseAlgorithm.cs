namespace NoiseGenerator;

public interface IDifferentialNoiseAlgorithm<TO> : INoiseAlgorithm<TO>
{
    (TO, TO, TO) GenerateNoiseOnPointWithDerivative(TO x, TO y);
}
