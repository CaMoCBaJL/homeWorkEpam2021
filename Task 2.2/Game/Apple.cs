using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Apple : Collectables
    {
        public override EffectsOnUser EffectOnUser { get; set; }

        public override (int x, int y) Position { get; set; }

        public override ConsoleColor Color { get; set; }

        public override char Img { get; set; }

        public Apple() : base() { }

        public Apple((int x, int y) pos)
        {
            Position = pos;
            Color = ConsoleColor.Red;
            Img = 'A';
            EffectOnUser = EffectsOnUser.LifeIncrease;
        }

    }
}
