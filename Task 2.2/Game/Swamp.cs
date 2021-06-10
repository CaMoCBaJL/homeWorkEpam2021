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
        public override EffectsOnUser EffectOnUser { get; set; }

        public Swamp((int x, int y) pos, int width, int height)
        {
            Width = width;
            Height = height;
            Position = pos;
            EffectOnUser = EffectsOnUser.SpeedDecrease;
            Color = ConsoleColor.Gray;
            Img = char.ConvertFromUtf32(15)[0];
        }

    }
}

