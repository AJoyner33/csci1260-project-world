using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld;

// One extra sort order: lightest weight first.
public class HealthCheckByWeight : IComparer<HealthCheck>
{
    public int Compare(HealthCheck? x, HealthCheck? y)
    {
        if (x is null)
            return y is null ? 0 : -1;
        if (y is null)
            return 1;
        return x.WeightKg.CompareTo(y.WeightKg);
    }
}
