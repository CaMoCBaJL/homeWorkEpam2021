using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Snake : Enemy
    {
        int _health;
        int _maxDamage;
        int _minDamage;
        (int x, int y) _position;

        public override int Health
        {
            get => _health;
            set => _health = value;
        }

        public override int MaxDamage
        {
            get => new Random().Next(_minDamage, _maxDamage);
            set => _maxDamage = value;
        }

        public override int Level
        {
            get; set;
        }

        public override ConsoleColor Color
        {
            get; set;
        }
        public override (int x, int y) Position { get => _position; set => _position = value; }
        public override char Img { get; set; }

        public override int Hit()
        {
            return Level * MaxDamage;
        }

        public override bool IsDead(int hitValue)
        {
            _health -= hitValue;
            return _health - hitValue > 0;
        }

        public override void Hitted(int damageValue)
        {
            if (IsDead(damageValue))
                Color = ConsoleColor.Gray;
            else
            {
                _maxDamage += Level * 2;
                _minDamage += Level;
            }
        }

        public override void MoveDown()
        {
            
        }

        public override void MoveLeft()
        {
        }

        public override void MoveRigth()
        {
        }

        public override void MoveUp()
        {
        }

        public Snake()
        {
            _health = 0;
            _position = (0, 0);
            Img = '\0';
            _maxDamage = 0;
            _minDamage = 0;
        }

        public Snake(int health, (int x, int y) position, int level)
        {
            _health = health;
            _position = position;
            AutoLeveling(level);
            Img = char.ConvertFromUtf32(1134)[0];
        }

        public override void AutoLeveling(int level)
        {
            switch (level)
            {
                case 1:
                    _health = 1;
                    _minDamage = 7;
                    _maxDamage = 12;
                    Level = level;
                    Color = ConsoleColor.Green;
                    break;
                case 2:
                    _health = 3;
                    _minDamage = 13;
                    _maxDamage = 20;
                    Level = level;
                    Color = ConsoleColor.DarkGreen;
                    break;
                case 3:
                    _health = 7;
                    _minDamage = 19;
                    _maxDamage = 26;
                    Level = level;
                    Color = ConsoleColor.Yellow;
                    break;
                case 4:
                    _health = 9;
                    _minDamage = 30;
                    _maxDamage = 37;
                    Level = level;
                    Color = ConsoleColor.DarkRed;
                    break;
                case 5:
                    _health = 15;
                    _minDamage = 40;
                    _maxDamage = 50;
                    Level = level;
                    Color = ConsoleColor.Red;
                    break;
            }
        }

        public override Type GetType()
        {
            return this.GetType();
        }
    }
}
