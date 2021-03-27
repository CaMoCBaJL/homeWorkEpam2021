using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class Collectables : GameObject
    {
        abstract public EffectsOnUser EffectOnUser { get; set; }

        public Collectables()
        {
            Position = (0, 0);
            Img = '\0';

        }

    }
}
