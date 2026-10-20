using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld;

// Another sort order: earliest date first, across all animals.
public class HealthCheckByDate : IComparer<HealthCheck>
{
    public int Compare(HealthCheck? x, HealthCheck? y)
    {
        if (x is null)
            return y is null ? 0 : -1;
        if (y is null)
            return 1;
        return x.Date.CompareTo(y.Date);
    }
}
