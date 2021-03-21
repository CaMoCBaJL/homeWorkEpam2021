using System;
using System.Collections.Generic;
using System.IO;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            GameEngine g = new GameEngine();
            
            g.BuildField(31, 31);
            g.GenerateLandScape(GameEngine.LandScapeType.Big);
            g.SpawnEnemies(GameEngine.EnemyAmount.WILFDOREST);
            g.GenerateItems(GameEngine.CollectablesAmount.ChestOfTreasures);
            g.GameStart();
        }
    }
}
