using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class Collectables : GameObject
    {
        abstract public Effect.effects EffectOnUser { get; set; }

    }
}
