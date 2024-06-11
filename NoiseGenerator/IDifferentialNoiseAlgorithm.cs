namespace NoiseGenerator;

public interface IDifferentialNoiseAlgorithm<TO> : INoiseAlgorithms<TO>
{
    (TO, TO, TO) GenerateNoiseOnPointWithDerivative(TO x, TO y);
}
