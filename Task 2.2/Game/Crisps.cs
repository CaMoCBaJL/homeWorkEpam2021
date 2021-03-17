using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Crisps : Collectables
    {
        public override Effect.effects EffectOnUser { get; set; }
        public override (int x, int y) Position { get; set; }
        public override ConsoleColor Color { get; set; }
        public override char Img { get; set; }

        public Crisps((int x, int y) pos)
        {
            Position = pos;
            Color = ConsoleColor.DarkYellow;
            EffectOnUser = Effect.effects.SpeedIncrease;
            Img = char.ConvertFromUtf32(169)[0];
        }

        public override Type GetType()
        {
            return this.GetType();
        }
    }
}
