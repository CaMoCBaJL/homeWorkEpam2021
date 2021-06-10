using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
<<<<<<< HEAD
    abstract class Collectables
    {
        public abstract ConsoleColor Color { get; set; }
=======
    abstract class Collectables : GameObject
    {
        abstract public EffectsOnUser EffectOnUser { get; set; }

        public Collectables()
        {
            Position = (0, 0);
            Img = '\0';

        }

>>>>>>> dc519e471e32cfcce97bccac106286308191a167
    }
}
