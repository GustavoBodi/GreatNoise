namespace NoiseGenerator;

public interface INoiseAlgorithms<TO, T>
where T: INoiseParameter
{
    TO GenerateNoiseOnPoint(TO x, TO y);
}
