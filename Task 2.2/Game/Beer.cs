using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Beer : Collectables
    {
        public override EffectsOnUser EffectOnUser { get; set; }

        public override (int x, int y) Position { get; set; }

        public override ConsoleColor Color { get; set; }

        public override char Img { get; set; }

        public Beer() : base() { }

        public Beer((int x, int y) pos)
        {
            Position = pos;
            Color = ConsoleColor.DarkYellow;
            EffectOnUser = EffectsOnUser.DamageIncrease;
            Img = '8'; 
        }

    }
}
