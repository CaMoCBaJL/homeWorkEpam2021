using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class User : GameObject
    {

        public override (int x, int y) Position { get; set; }

        public override ConsoleColor Color { get; set; }

        public override char Img { get; set; }


        public User((int x, int y) pos)
        {
            Color = ConsoleColor.Cyan;
            Img = 'U';
            Position = pos;
        }

    }
}
