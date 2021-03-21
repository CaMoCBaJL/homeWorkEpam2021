using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Bear : Enemy
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

        public override char Img { get; set; }


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

        public override int Hit()
        {
            return Level * MaxDamage;
        }

        public override (int x, int y) Position { get => _position; set => _position = value; }

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
                _maxDamage += Level * 3;
                _minDamage += Level;
            }
        }

        public override void MoveDown()
        {
            _position.y -= 2;
        }

        public override void MoveLeft()
        {
            _position.x -= 2;
        }

        public override void MoveRigth()
        {
            _position.x += 2;
        }

        public override void MoveUp()
        {
            _position.y += 2;
        }

        private Bear()
        {
            _health = 0;
            _position = (0, 0);
            Img = '\0';
            _maxDamage = 0;
            _minDamage = 0;
        }

        public Bear((int x, int y) position, int level)
        {
            _position = position;
            AutoLeveling(level);
            Img = 'B';
        }

        public override void AutoLeveling(int level)
        {
            switch (level)
            {
                case 1:
                    _health = 13;
                    _minDamage = 5;
                    _maxDamage = 8;
                    Level = level;
                    Color = ConsoleColor.White;
                    break;
                case 2:
                    _health = 20;
                    _minDamage = 8;
                    _maxDamage = 12;
                    Level = level;
                    Color = ConsoleColor.Cyan;
                    break;
                case 3:
                    _health = 30;
                    _minDamage = 10;
                    _maxDamage = 18;
                    Level = level;
                    Color = ConsoleColor.Yellow;
                    break;
                case 4:
                    _health = 40;
                    _minDamage = 15;
                    _maxDamage = 25;
                    Level = level;
                    Color = ConsoleColor.DarkRed;
                    break;
                case 5:
                    _health = 55;
                    _minDamage = 20;
                    _maxDamage = 36;
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
