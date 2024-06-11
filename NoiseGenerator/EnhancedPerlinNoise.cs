using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NoiseGenerator;

public class EnhancedPerlinNoise: IDifferentialNoiseAlgorithm<double>
{
    private readonly PerlinNoiseParameter _parameters = new PerlinNoiseParameter();

    public EnhancedPerlinNoise(PerlinNoiseParameter? parameters = null)
    {
        if (parameters != null)
          _parameters = parameters;
        if (_parameters.ShouldShuffle)
          ShufflePermutationTable();
    }

    Vector<double> GetConstantVector(int v)
    {
        var h = v & 3;
        return h switch
        {
            0 => new DenseVector(new[] { 1.0, 1.0 }),
            1 => new DenseVector(new[] { -1.0, 1.0 }),
            2 => new DenseVector(new[] { -1.0, -1.0 }),
            _ => new DenseVector(new[] { 1.0, -1.0 })
        };
    }

    private void ShufflePermutationTable()
    {
        var n = _table.Length;
        var rnd = new Random();
        while (n > 1)
        {
            int k = rnd.Next(n--);
            (_table[n], _table[k]) = (_table[k], _table[n]);
        }
    }

    private double Lerp(double t, double a, double b)
    {
        return a + t * (b - a);
    }

    private double Fade(double t)
    {
        return ((6 * t - 15) * t + 10) * t * t * t;
    }

    public double GenerateNoiseOnPoint(double x, double y)
    {
      var (result, dx, dy) = GenerateNoiseOnPointWithDerivative(x, y);
      return result;
    }

    public (double, double, double) GenerateNoiseOnPointWithDerivative(double x, double y)
    {
        x *= _parameters.Frequency.X;
        y *= _parameters.Frequency.Y;
        var xWrap = (int)Math.Floor(x) & 255;
        var yWrap = (int)Math.Floor(y) & 255;
        
        var xf = x - Math.Floor(x);
        var yf = y - Math.Floor(y);

        var grad11 = Vector<double>.Build.DenseOfArray(new [] { xf - 1.0, yf - 1.0 });
        var grad01 = Vector<double>.Build.DenseOfArray(new [] { xf, yf - 1.0 });
        var grad10 = Vector<double>.Build.DenseOfArray(new [] { xf - 1.0, yf });
        var grad00 = Vector<double>.Build.DenseOfArray(new [] { (double)xf, (double)yf });

        var topRightPerm = _table[_table[xWrap + 1] + yWrap + 1];
        var topLeftPerm = _table[_table[xWrap] + yWrap + 1];
        var bottomRightPerm = _table[_table[xWrap + 1] + yWrap];
        var bottomLeftPerm = _table[_table[xWrap] + yWrap];

        var dotTopRight = grad11 * GetConstantVector(topRightPerm);
        var dotTopLeft = grad01 * GetConstantVector(topLeftPerm);
        var dotBottomRight = grad10 * GetConstantVector(bottomRightPerm);
        var dotBottomLeft = grad00 * GetConstantVector(bottomLeftPerm);

        var u = Fade(xf);
        var v = Fade(yf);

        var interpolated_x0 = Lerp(u, dotBottomLeft, dotBottomRight);

        var interpolated_x1 = Lerp(u, dotTopLeft, dotTopRight);

        var dx = (interpolated_x1 - interpolated_x0);

        var dy = ((dotTopLeft - dotBottomLeft) * (1 - u) + (dotTopRight - dotBottomRight) * u);

        return (Lerp(
            v,
            interpolated_x0,
            interpolated_x1), dx, dy);
    }

    private int[] _table = new int[512] { 151,160,137,91,90,15,
        131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
        190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
        88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
        77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
        102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
        135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
        5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
        223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
        129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
        251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
        49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
        138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,151,
        160,137,91,90,15,131,13,201,95,96,53,194,233, 7,225,140,36,103,30,69,142, 8,
        99,37,240,21,10,23,190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,
        35,11,32,57,177,33,88,237,149, 56, 87,174,20,125,136,171,168, 68,175,74,165,
        71,134,139,48,27,166,77,146,158,231, 83,111,229,122, 60,211,133,230,220,105,
        92,41,55,46,245,40,244,102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,
        208,89, 18,169,200,196,135,130,116,188,159,86,164,100,109,198,173,186, 3,64,
        52,217,226,250,124,123,5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,
        16,58,17,182,189,28,42,223,183,170,213,119,248,152, 2,44,154,163,70,221,153,
        101,155,167,43,172,9,129,22,39,253,19,98,108,110,79,113,224,232,178,185,112,
        104,218,246,97,228,251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,
        235,249,14,239,107,49,192,214,31,181,199,106,157,184, 84,204,176,115,121,50,
        45,127,4,150,254,138,236,205,93,222,114, 67,29, 24,72,243,141,128,195,78,66,
        215,61,156,180
    };
    
}
