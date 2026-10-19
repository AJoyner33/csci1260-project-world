using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    public interface ITrainable
    {
        string TrickName { get; }
        string Perform();
    }
}
