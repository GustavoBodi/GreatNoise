using MathNet.Numerics.LinearAlgebra;

namespace NoiseGenerator;

public class DlaNoiseCompositor: INoiseAlgorithm<double>
{
    private List<IDifferentialNoiseAlgorithm<double>> _algorithms = new List<IDifferentialNoiseAlgorithm<double>>();

    private int _iterations;

    public DlaNoiseCompositor(int iterations=1)
    {
        _iterations = iterations;
    }

    public DlaNoiseCompositor AddAlgorithm(IDifferentialNoiseAlgorithm<double> algorithm) {
      _algorithms.Add(algorithm);
      return this;
    }

    public DlaNoiseCompositor AddAlgorithms(IEnumerable<IDifferentialNoiseAlgorithm<double>> algorithms) {
      _algorithms.AddRange(algorithms);
      return this;
    }

    public double GenerateNoiseOnPoint(double x, double y)
    {
      var lastGradient = Vector<double>.Build.DenseOfArray(new [] { 0.0, 0.0 });
      var height = 0.0;
      foreach (var algorithm in _algorithms) {
        var (result, dx, dy) = algorithm.GenerateNoiseOnPointWithDerivative(x, y);
        var newGradient = Vector<double>.Build.DenseOfArray(new [] { dx, dy });
        var (heightTemp, gradient) = Flatten(result, newGradient, lastGradient);
        height += heightTemp;
        lastGradient = gradient;
      }
      return height;
    }

    private (double, Vector<double>) Flatten(double height, Vector<double> newGradient, Vector<double> lastGradient)
    {
      newGradient = (newGradient + lastGradient).Normalize(2);
      double gradientMagnitude = newGradient.L2Norm();
      double scalingFactor = 1.0 / (1.0 + gradientMagnitude);
      double scaledHeight = height * scalingFactor;
      return (scaledHeight, newGradient);
    }
}
