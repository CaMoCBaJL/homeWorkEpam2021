using System;
using System.Collections.Generic;
using System.IO;
using static Game.GameEngine;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            GameEngine g = new GameEngine();

            g.AddObjectToField(new Wolf());
            
            g.BuildField(31, 31);

            g.CreateLandScape(LandScapeType.Big);

            g.CreateEnemies(EnemyAmount.WILFDOREST);

            g.CreateItems(CollectablesAmount.ChestOfTreasures);

            g.GameStart();
        }
    }
}
