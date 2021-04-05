using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Wolf : Enemy
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
            Position = (Position.x, Position.y + 3);
        }

        public override void MoveLeft()
        {
            Position = (Position.x - 3, Position.y);
        }

        public override void MoveRigth()
        {
            Position = (Position.x + 3, Position.y);
        }

        public override void MoveUp()
        {
            Position = (Position.x, Position.y - 3);
        }

        public Wolf() : base() { }

        public Wolf((int x, int y) position, int level) : base(position, level)
        {
            Img = 'W';
        }

        public override void AutoLeveling(int level)
        {
            switch(level)
            {
                case 1:
                    Health = 4;
                    MinDamage = 1;
                    MaxDamage = 5;
                    Level = level;
                    Color = ConsoleColor.White;
                    break;
                case 2:
                    Health = 7;
                    MinDamage = 3;
                    MaxDamage = 8;
                    Level = level;
                    Color = ConsoleColor.Cyan;
                    break;
                case 3:
                    Health = 10;
                    MinDamage = 5;
                    MaxDamage = 12;
                    Level = level;
                    Color = ConsoleColor.Yellow;
                    break;
                case 4:
                    Health = 15;
                    MinDamage = 10;
                    MaxDamage = 17;
                    Level = level;
                    Color = ConsoleColor.DarkRed;
                    break;
                case 5:
                    Health = 25;
                    MinDamage = 15;
                    MaxDamage = 30;
                    Level = level;
                    Color = ConsoleColor.Red;
                    break;
            }
        }
    }
}
