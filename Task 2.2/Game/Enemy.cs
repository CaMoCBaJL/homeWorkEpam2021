using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class Enemy
    {
        public abstract void MoveRigth();
        public abstract void MoveLeft();
        public abstract void MoveUp();
        public abstract void MoveDown();
        public abstract char Image { get; set; }
        public abstract int Health { get; set; }
        public abstract int MaxDamage { get; set; }
        public abstract int Level { get; set; }
        public abstract ConsoleColor Color { get; set; }
        public abstract int Hit();
        public abstract void Hitted(int damageValue);
        public abstract bool IsDead(int hitValue);
        public abstract void AutoLeveling(int level);
    }
}
