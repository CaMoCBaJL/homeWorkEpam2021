using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Wall : LandScapeElem
    {
        public override char Img { get; set; }
        public override int Width {get; set; }
        public override int Height { get; set; }
        public override ConsoleColor Color { get; set; }
        public override (int x, int y) Position { get; set; }
        public override Effect.effects EffectOnUser { get; set; }

        private Wall() { }

        public static Wall CreateVerticalWall(int height, (int x, int y) pos)
        {
            return new Wall()
            {
                EffectOnUser = Effect.effects.None,
                Color = ConsoleColor.Gray,
                Position = pos,
                Width = 1,
                Height = height,
                Img = 'I',
            };
        }

        public static Wall CreateHorizontalWall(int width, (int x, int y) pos)
        {
            return new Wall()
            {
                EffectOnUser = Effect.effects.None,
                Color = ConsoleColor.Gray,
                Position = pos,
                Width = width,
                Height = 1,
                Img = 'I',
            };
        }

        public override Type GetType()
        {
            return this.GetType();
        }
    }
}
