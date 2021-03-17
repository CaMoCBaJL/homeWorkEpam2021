using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Beer : Collectables
    {
        public override Effect.effects EffectOnUser { get; set; }
        public override (int x, int y) Position { get; set; }
        public override ConsoleColor Color { get; set; }
        public override char Img { get; set; }

        public Beer((int x, int y) pos)
        {
            Position = pos;
            Color = ConsoleColor.DarkYellow;
            EffectOnUser = Effect.effects.DamageIncrease;
            Img = char.ConvertFromUtf32(338)[0]; 
        }

        public override Type GetType()
        {
            return this.GetType();
        }
    }
}
