using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Bear : Enemy
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
            Position = (Position.x ,Position.y + 2);
        }

        public override void MoveLeft()
        {
            Position = (Position.x - 2, Position.y);
        }

        public override void MoveRigth()
        {
            Position = (Position.x + 2, Position.y);
        }

        public override void MoveUp()
        {
            Position = (Position.x, Position.y - 2);
        }

        public Bear() : base() { }

        public Bear((int x, int y) position, int level) : base(position, level)
        {
            Img = 'B';
        }

        public override void AutoLeveling(int level)
        {
            switch (level)
            {
                case 1:
                    Health = 13;
                    MinDamage = 5;
                    MaxDamage = 8;
                    Level = level;
                    Color = ConsoleColor.White;
                    break;
                case 2:
                    Health = 20;
                    MinDamage = 8;
                    MaxDamage = 12;
                    Level = level;
                    Color = ConsoleColor.Cyan;
                    break;
                case 3:
                    Health = 30;
                    MinDamage = 10;
                    MaxDamage = 18;
                    Level = level;
                    Color = ConsoleColor.Yellow;
                    break;
                case 4:
                    Health = 40;
                    MinDamage = 15;
                    MaxDamage = 25;
                    Level = level;
                    Color = ConsoleColor.DarkRed;
                    break;
                case 5:
                    Health = 55;
                    MinDamage = 20;
                    MaxDamage = 36;
                    Level = level;
                    Color = ConsoleColor.Red;
                    break;
            }
        }

    }
}
