using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Snake : Enemy
    {
        public override (int x, int y) Position { get; set; }

        public override int Health { get; set; }

        public override char Img { get; set; }

        public override int MaxDamage { get; set; }

        public override int MinDamage { get; set; }

        public override int Level { get; set; }

        public override ConsoleColor Color { get; set; }

        public override void MoveDown()
        {
            Position = (Position.x, Position.y + 1);
        }

        public override void MoveLeft()
        {
            Position = (Position.x - 1, Position.y);
        }

        public override void MoveRigth()
        {
            Position = (Position.x + 1, Position.y);
        }

        public override void MoveUp()
        {
            Position = (Position.x, Position.y - 1);
        }

        public Snake() : base() { }

        public Snake((int x, int y) position, int level) : base(position, level)
        {
            Img = 'S';
        }

        public override void AutoLeveling(int level)
        {
            switch (level)
            {
                case 1:
                    Health = 1;
                    MinDamage = 7;
                    MaxDamage = 12;
                    Level = level;
                    Color = ConsoleColor.White;
                    break;
                case 2:
                    Health = 3;
                    MinDamage = 13;
                    MaxDamage = 20;
                    Level = level;
                    Color = ConsoleColor.Cyan;
                    break;
                case 3:
                    Health = 7;
                    MinDamage = 19;
                    MaxDamage = 26;
                    Level = level;
                    Color = ConsoleColor.Yellow;
                    break;
                case 4:
                    Health = 9;
                    MinDamage = 30;
                    MaxDamage = 37;
                    Level = level;
                    Color = ConsoleColor.DarkRed;
                    break;
                case 5:
                    Health = 15;
                    MinDamage = 40;
                    MaxDamage = 50;
                    Level = level;
                    Color = ConsoleColor.Red;
                    break;
            }
        }

    }
}
