namespace NoiseGenerator;

public class DlaNoiseCompositor: INoiseAlgorithms<double>
{
    private List<IDifferentialNoiseAlgorithm<double>> _algorithms = new List<IDifferentialNoiseAlgorithm<double>>();

    private int _iterations;

    public DlaNoiseCompositor(IDifferentialNoiseAlgorithm<double> algorithm, int iterations=1)
    {
        _algorithms.Add(algorithm);
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
      var gradient_sum = 0.0;
      var height = 0.0;
      foreach (var algorithm in _algorithms) {
        var (result, dx, dy) = algorithm.GenerateNoiseOnPointWithDerivative(x, y);
        var (height_temp, gradient) = Flatten(result, dx, dy, gradient_sum);
        height += height_temp;
        gradient_sum += gradient;
      }
      return height;
    }

    private (double, double) Flatten(double height, double dx, double dy, double lastGradient) {
      double gradient = Math.Sqrt(dx * dx + dy * dy);
      return ((height) * (1 / (1 + (gradient + lastGradient))), gradient);
    }
}
