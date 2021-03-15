using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Wolf : Enemy
    {
        char _img;
        int _health;
        int _maxDamage;
        int _minDamage;
        (int x, int y) _position;

        public override char Image
        { 
            get => _img;
            set => _img = value;
        }

        public override int Health 
        { get => _health;
            set => _health = value;
        }

        public override int MaxDamage
        {
            get => new Random().Next(_minDamage, _maxDamage);
            set => _maxDamage = value;
        }

        public override int Level 
        {
            get;set;
        }

        public override ConsoleColor Color 
        {
            get; set;
        }

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
            _position.y -= 3;
        }

        public override void MoveLeft()
        {
            _position.x -= 3;
        }

        public override void MoveRigth()
        {
            _position.x += 3;
        }

        public override void MoveUp()
        {
            _position.y += 3;
        }

        public Wolf()
        {
            _health = 0;
            _position = (0, 0);
            _img = '\0';
            _maxDamage = 0;
            _minDamage = 0;
        }

        public Wolf(int health, (int x, int y) position, int level)
        {
            _health = health;
            _position = position;
            AutoLeveling(level);
            _img = '@';
        }

        public override void AutoLeveling(int level)
        {
            switch(level)
            {
                case 1:
                    _health = 4;
                    _minDamage = 1;
                    _maxDamage = 5;
                    Level = level;
                    Color = ConsoleColor.Green;
                    break;
                case 2:
                    _health = 7;
                    _minDamage = 3;
                    _maxDamage = 8;
                    Level = level;
                    Color = ConsoleColor.DarkGreen;
                    break;
                case 3:
                    _health = 10;
                    _minDamage = 5;
                    _maxDamage = 12;
                    Level = level;
                    Color = ConsoleColor.Yellow;
                    break;
                case 4:
                    _health = 15;
                    _minDamage = 10;
                    _maxDamage = 17;
                    Level = level;
                    Color = ConsoleColor.DarkRed;
                    break;
                case 5:
                    _health = 25;
                    _minDamage = 15;
                    _maxDamage = 30;
                    Level = level;
                    Color = ConsoleColor.Red;
                    break;
            }
        }
    }
}
