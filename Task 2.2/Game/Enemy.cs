using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class Enemy : GameObject
    {
        public abstract void MoveRigth();

        public abstract void MoveLeft();

        public abstract void MoveUp();

        public abstract void MoveDown();

        public abstract int Health { get; set; }

        public abstract int MaxDamage { get; set; }

        public abstract int MinDamage { get; set; }

        public abstract int Level { get; set; }

        public abstract void AutoLeveling(int level);

        public int Hit()
        {
            return Level * new Random().Next(MinDamage, MaxDamage);
        }

        public bool IsDead(int hitValue)
        {
            Health -= hitValue;
            return Health - hitValue > 0;
        }

        public void Hitted(int damageValue)
        {
            if (IsDead(damageValue))
                Color = ConsoleColor.Gray;
            else
            {
                MaxDamage += Level * 3;
                MinDamage += Level;
            }
        }

        internal Enemy()
        {
            Health = 0;
            Position = (0, 0);
            Img = '\0';
            MaxDamage = 0;
            MinDamage = 0;
        }

        public Enemy((int x, int y) position, int level)
        {
            Position = position;
            AutoLeveling(level);
        }
    }
}
