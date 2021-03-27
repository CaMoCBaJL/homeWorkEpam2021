using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class LandScapeElem:GameObject
    { 
        abstract public EffectsOnUser EffectOnUser { get; set; }

        abstract public int Width { get; set; }

        abstract public int Height { get; set; }

    }
}
