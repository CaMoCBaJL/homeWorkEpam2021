using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class User : GameObject
    {
        public void Move (Keys key)
        {
            switch (key)
            {
                case Keys.Rigth:
                    Position = (Position.x + 1, Position.y);
                    break;
                case Keys.Left:
                    Position = (Position.x - 1, Position.y);
                    break;
                case Keys.Up:
                    Position = (Position.x, Position.y - 1);
                    break;
                case Keys.Down:
                    Position = (Position.x, Position.y + 1);
                    break;
            }
        }

        public override (int x, int y) Position { get; set; }
        public override ConsoleColor Color { get; set; }
        public override char Img { get; set; }

        public User((int x, int y) pos)
        {
            Color = ConsoleColor.Cyan;
            Img = 'U';
            Position = pos;
        }

        public override Type GetType()
        {
            return this.GetType();
        }
    }
}
