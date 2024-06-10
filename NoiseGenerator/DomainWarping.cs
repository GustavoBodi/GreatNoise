namespace NoiseGenerator;

public class DomainWarping : INoiseAlgorithms<double, DomainWarpingParameter>
{
    private DomainWarpingParameter _parameters;

    public DomainWarping(DomainWarpingParameter parameters)
    {
      _parameters = parameters;
    }

    public double GenerateNoiseOnPoint(double x, double y)
    {
        double offsetX = 7 * 10;
        double offsetY = 1.3 * 10;
        double qx = _parameters.Fn(x, y);
        double qy = _parameters.Fn(x + offsetX, y + offsetY);
        double rx = _parameters.Fn(x + 2.0*qx + 1.7, y + 2.0*qy + 9.2);
        double ry = _parameters.Fn(x + 2.0*qx + 8.3, y + 2.0*qy + 2.8);
        return _parameters.Fn(x + 80*rx, y + 70*ry);
    }
}
