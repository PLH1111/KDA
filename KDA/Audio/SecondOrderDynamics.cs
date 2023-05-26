using System;

namespace KDA.Audio;

public class SecondOrderDynamics
{
    private double xp;// previous input
    private double y, yd; // state variables
    private readonly double w;
    private readonly double z;
    private readonly double d;
    private readonly double k1;
    private readonly double k2;
    private readonly double k3;

    public SecondOrderDynamics(double f, double z, double r, double x0)
    {
        //compute constants
        w = 2 * Math.PI * f;
        this.z = z;
        d = w * Math.Sqrt(Math.Abs((z * z) - 1));
        k1 = z / (Math.PI * f);
        k2 = 1 / (2 * Math.PI * f * (2 * Math.PI * f));
        k3 = r * z / (2 * Math.PI * f);

        // initialize variables
        xp = x0;
        y = x0;
        yd = 0;
    }

    public double Update(double deltaTime, double x)
    {
        double xd = (x - xp) / deltaTime;
        double k1_stable, k2_stable;
        if (w * deltaTime < z)
        {
            k1_stable = k1;
            k2_stable = Math.Max(Math.Max(k2, deltaTime * deltaTime / 2 + deltaTime * k1 / 2), deltaTime * k1);
        }
        else
        {
            double t1 = Math.Exp(-z * w * deltaTime);
            double alpha = 2 * t1 * (z <= 1 ? Math.Cos(deltaTime * d) : Math.Cosh(deltaTime * d));
            double beta = t1 * t1;
            double t2 = deltaTime / (1 + beta - alpha);
            k1_stable = (1 - beta) * t2;
            k2_stable = deltaTime * t2;
        }

        y += deltaTime * yd;
        yd += deltaTime * (x + k3 * xd - y - (k1_stable * yd)) / k2_stable;

        xp = x;
        return y;
    }
}
