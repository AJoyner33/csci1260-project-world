using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    // A promise that an animal can perform a trained trick.
    // Only some animals can keep it, so it lives outside the family tree.
    public interface ITrainable
    {
        string TrickName { get; }
        string Perform();
    }
}
