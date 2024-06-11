namespace NoiseGenerator;

public class Voronoi : INoiseAlgorithms<double>
{
    private VoronoiParameter _parameters { get; set; }

    public Voronoi(VoronoiParameter parameters)
    {
        _parameters = parameters;
    }

    public double GenerateNoiseOnPoint(double x, double y)
    {
        double px = Math.Floor(x);
        double py = Math.Floor(y);
        double fx = x - px;
        double fy = y - py;

        double res = 0.0;
        for( int j=-1; j<=1; j++ )
        for( int i=-1; i<=1; i++ )
        {
            double bx = i;
            double by = j;
            double rx = bx - fx + _parameters.Fn.GenerateNoiseOnPoint(x + bx, y + by);
            double ry = by - fy + _parameters.Fn.GenerateNoiseOnPoint(x + bx, y + by);
            double d = rx * rx + ry * ry;

            res += 1.0 / Math.Pow(d, 8.0);
        }

        return 1 - Math.Pow(1.0 / res, 1.0/16.0);
      }
}
