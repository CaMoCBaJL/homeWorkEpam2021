using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Swamp : LandScapeElem
    {
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override char Img { get; set; }
        public override ConsoleColor Color { get; set; }
        public override (int x, int y) Position { get; set; }
        public override Effect.effects EffectOnUser { get; set; }

        public Swamp((int x, int y) pos, int width, int height)
        {
            EffectOnUser = Effect.effects.SpeedDecrease;
            Color = ConsoleColor.Gray;
            Width = width;
            Height = height;
            Position = pos;
            Img = char.ConvertFromUtf32(15)[0];
        }

        public override Type GetType()
        {
            return this.GetType();
        }
    }
}

