using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Bear : Enemy
    {
        public override char Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int MaxDamage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ConsoleColor Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AutoLeveling(int level)
        {
            throw new NotImplementedException();
        }

        public override int Hit()
        {
            throw new NotImplementedException();
        }

        public override void Hitted(int damageValue)
        {
            throw new NotImplementedException();
        }

        public override bool IsDead(int hitValue)
        {
            throw new NotImplementedException();
        }

        public override void MoveDown()
        {
            throw new NotImplementedException();
        }

        public override void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public override void MoveRigth()
        {
            throw new NotImplementedException();
        }

        public override void MoveUp()
        {
            throw new NotImplementedException();
        }
    }
}
