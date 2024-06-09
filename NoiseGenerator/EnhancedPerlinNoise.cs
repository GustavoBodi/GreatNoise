using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NoiseGenerator;

public class EnhancedPerlinNoise: INoiseAlgorithms<double, PerlinNoiseParameter>
{
    private HeightMap _map;
    
    private readonly int _mapSize;

    private readonly PerlinNoiseParameter _parameters;

    public EnhancedPerlinNoise(int mapSize, PerlinNoiseParameter parameters)
    {
        _parameters = parameters;
        _mapSize = mapSize;
        _map = new HeightMap(mapSize);
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

    public void GenerateNoise()
    {
        var map = _map.GetMapReference();
        Parallel.For(0, GetMapSize(), y =>
        {
            //for (var y = 0; y < GetMapSize(); ++y)
            for (var x = 0; x < GetMapSize(); ++x)
            {
                double n;
                if (!_parameters.UsesBrownianMotion)
                {
                    n = GenerateNoiseOnPoint(x * _parameters.Frequency.X, y * _parameters.Frequency.X);
                }
                else
                { 
                    n = GenerateFractalBrownianMotionOnPoint(x, y, 8);
                    var q0 = GenerateFractalBrownianMotionOnPoint(x, y, 8);
                    var q1 = GenerateFractalBrownianMotionOnPoint(x + 100, y + 130, 8);
                    var r0 = GenerateFractalBrownianMotionOnPoint(x + 6*q0 + 17, y + 92 + 4*q1, 8);
                    var r1 = GenerateFractalBrownianMotionOnPoint(x + 6*q0 + 83, y + 4 * q1 + 28, 8);
                    n = GenerateFractalBrownianMotionOnPoint(x + 4 * r0, y + 4 * r1, 8);
                }

                n += 1.0;
                n *= 0.5;

                int c = (int)Math.Round(255 * n);
                if (c > 255) {
                  c = 255;
                } else if (c < 0) {
                  c = 0;
                }
                map[y, x] = c;

            }
        });
    }

    public HeightMap GetNoiseMap()
    {
        return _map;
    }

    public int GetMapSize()
    {
        return _mapSize;
    }

    private double Lerp(double t, double a, double b)
    {
        return a + t * (b - a);
    }

    private double Fade(double t)
    {
        return ((6 * t - 15) * t + 10) * t * t * t;
    }

    double GenerateNoiseOnPoint(double x, double y)
    {
        var xWrap = (int)Math.Floor(x) & 255;
        var yWrap = (int)Math.Floor(y) & 255;
        
        var xf = x - Math.Floor(x);
        var yf = y - Math.Floor(y);

        var topRight = Vector<double>.Build.DenseOfArray(new [] { xf - 1.0, yf - 1.0 });
        var topLeft = Vector<double>.Build.DenseOfArray(new [] { xf, yf - 1.0 });
        var bottomRight = Vector<double>.Build.DenseOfArray(new [] { xf - 1.0, yf });
        var bottomLeft = Vector<double>.Build.DenseOfArray(new [] { (double)xf, (double)yf });

        var topRightPerm = _table[_table[xWrap + 1] + yWrap + 1];
        var topLeftPerm = _table[_table[xWrap] + yWrap + 1];
        var bottomRightPerm = _table[_table[xWrap + 1] + yWrap];
        var bottomLeftPerm = _table[_table[xWrap] + yWrap];

        var dotTopRight = topRight * GetConstantVector(topRightPerm);
        var dotTopLeft = topLeft * GetConstantVector(topLeftPerm);
        var dotBottomRight = bottomRight * GetConstantVector(bottomRightPerm);
        var dotBottomLeft = bottomLeft * GetConstantVector(bottomLeftPerm);

        var u = Fade(xf);
        var v = Fade(yf);

        return Lerp(
            u,
            Lerp(v, dotBottomLeft, dotTopLeft),
            Lerp(v, dotBottomRight, dotTopRight));
    }

    public double GenerateFractalBrownianMotionOnPoint(double x, double y, int octaves)
    {
        var result = 0.0;
        var amplitude = 1.0;
        var frequency = 0.005;

        for (var octave = 0; octave < octaves; ++octave)
        {
            var noise = GenerateNoiseOnPoint(x * frequency, y * frequency);
            var n = amplitude * noise;
            result += n;

            frequency *= _parameters.Persistency;
            amplitude *= _parameters.Lacunarity;
        }

        return result;
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
        160,137,91,90,15,131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,
        37,240,21,10,23,190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,
        11,32,57,177,33,88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,
        139,48,27,166,77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,
        46,245,40,244,102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208,89,
        18,169,200,196,135,130,116,188,159,86,164,100,109,198,173,186,3,64,52,217,
        226,250,124,123,5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,
        17,182,189,28,42,223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,
        167, 43,172,9,129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,
        246,97,228,251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,
        239,107,49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127,4,150,
        254,138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
    };
    
}
