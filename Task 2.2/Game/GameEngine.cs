using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Game
{
    class GameEngine
    {

        public enum LandScapeType
        {
            None,
            Small,
            Middle,
            Big
        }

        public enum EnemyAmount {
            None,
            Some,
            Many,
            WILFDOREST
        }

        public enum CollectablesAmount { 
            Some,
            Many,
            ChestOfTreasures
        };

        struct ColorAndImg
        {
            public ConsoleColor color;
            public char img;
            public ColorAndImg(ConsoleColor elemColor, char image)
            {
                img = image;
                color = elemColor;
            }
        }


        //Constant images
        readonly char[] enemies = { 'W', 'Z', 'B' };

        readonly char[] collectables = { '8', 'C', 'A' };

        readonly char[] grassElements = {',', '\'', ';', '\"', '^'};
        //


        ColorAndImg[,] gameField;

        ColorAndImg[,] landscapeShot;

        List<GameObject> gameobjects;

        (int height, int width) fieldProperties;

        User user;

        int collectablesCount;


        public void GameStart()
        {
            SpawnUser();
            Console.WindowWidth = fieldProperties.width * 2;
            Console.WindowHeight = fieldProperties.height * 2;
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    Console.ForegroundColor = gameField[i, j].color;
                    Console.Write(gameField[i, j].img);
                }
                Console.WriteLine();
            }
            while (true)
            {
                UpdateField();
                switch (ParseToKeys(Console.ReadKey().Key.ToString()))
                {
                    case Keys.Rigth:
                        MoveUser(1, 0);
                        break;
                    case Keys.Left:
                        MoveUser(-1, 0);
                        break;
                    case Keys.Up:
                        MoveUser(0, -1);
                        break;
                    case Keys.Down:
                        MoveUser(0, 1);
                        break;
                    case Keys.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        void UpdateField()
        {
            Console.Clear();
            for (int i = 0; i < fieldProperties.width; i++)
            {
                for (int j = 0; j < fieldProperties.height; j++)
                {
                    Console.ForegroundColor = landscapeShot[i, j].color;
                    Console.Write(landscapeShot[i, j].img);
                }
                Console.WriteLine();
            }
        }

        public void BuildField(int width, int height, params GameObject[] objects)
        {
            if(height < 10 && width < 10)
                throw new Exception("Слишком маленькое поле для игры!");
            

            gameField = new ColorAndImg[width, height];
            fieldProperties = (height, width);
            Random choseElement = new Random();
            collectablesCount = 0;
            gameobjects = new List<GameObject>();


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gameField[i, j] = new ColorAndImg(ConsoleColor.Green, grassElements[choseElement.Next(0, grassElements.Length)]);
                }
            }


            foreach (var item in objects)
            {
                switch (item)
                {
                    case Enemy e:
                        if(!AddObjectToField(e))
                            Console.WriteLine("ADDING ERROR. ENEMY DIDN'T ADDED TO FIELD.");
                        break;
                    case LandScapeElem l:
                        if(!AddObjectToField(l))
                            Console.WriteLine("ADDING ERROR. LANDSCAPEITEM DIDN'T ADDED TO FIELD.");
                        break;
                    case Collectables c:
                        if (!AddObjectToField(c))
                            Console.WriteLine("ADDING ERROR. COLLECTABLE DIDN'T ADDED TO FIELD.");
                        else
                            collectablesCount++;
                        break;
                }

                gameobjects.Add(item);
            }
        }

        public void CreateEnemies(EnemyAmount amount)
        {

            SpawnEnemies<Wolf>(new Wolf(), amount);

            SpawnEnemies<Bear>(new Bear(), amount);

            SpawnEnemies<Snake>(new Snake(), amount);

        }

        public void CreateItems(CollectablesAmount amount)
        {
            SpawnCollectables<Apple>(new Apple(), amount);

            SpawnCollectables<Beer>(new Beer(), amount);

            SpawnCollectables<Crisps>(new Crisps(), amount);
        }

        public void CreateLandScape(LandScapeType type)
        {
            switch (type)
            {
                case LandScapeType.Small:
                    AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width/4, fieldProperties.width / 4)), fieldProperties.width / 4, fieldProperties.width / 4), 
                        fieldProperties.width / 4, fieldProperties.width / 4);

                    AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                        fieldProperties.width / 6, fieldProperties.width / 6);

                    for (int i = 0; i < 1; i++)
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 5, FindCoordsToNewObj((1, fieldProperties.width/5))), 1, fieldProperties.width / 5);
                    
                    landscapeShot = gameField;
                    break;
                case LandScapeType.Middle:
                    if (fieldProperties.height > 20 && fieldProperties.width > 20)
                    {
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 3, fieldProperties.width / 3)), fieldProperties.width / 3, fieldProperties.width / 3), 
                            fieldProperties.width / 3, fieldProperties.width / 3);

                        for (int i = 0; i < 3; i++)
                            AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                                fieldProperties.width / 6, fieldProperties.width / 6);

                        for (int i = 0; i < 1; i++)
                        {
                            AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 5, FindCoordsToNewObj((1, fieldProperties.width / 5))), 1, fieldProperties.width / 5);

                            AddToField(Wall.CreateVerticalWall(fieldProperties.width / 5, FindCoordsToNewObj((fieldProperties.width / 5, 1))), fieldProperties.width / 5, 1);
                        }

                        for (int i = 0; i < 6; i++)
                        {
                        
                            AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        
                            AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        }

                        landscapeShot = gameField;
                    }
                    else
                    {
                        Console.WriteLine("Данный режим создания ландшафта недоступен для поля заданных размеров.");
                        Console.WriteLine("Создан ландшафт типа \"Small\" ");
                        goto case LandScapeType.Small;
                    }
                    break;
                case LandScapeType.Big:
                    if (fieldProperties.height > 30 && fieldProperties.height > 30)
                    {
                        for (int i = 0; i < 2; i++)
                            AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 4, fieldProperties.width / 4)), fieldProperties.width / 4, fieldProperties.width / 4), 
                                fieldProperties.width / 4, fieldProperties.width / 4);

                        for (int i = 0; i < 2; i++)
                            AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6),
                                fieldProperties.width / 6, fieldProperties.width / 6);

                        for (int i = 0; i < 5; i++)
                            AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 7, fieldProperties.width / 7)), fieldProperties.width / 7, fieldProperties.width / 7), 
                                fieldProperties.width / 7, fieldProperties.width / 7);

                        for (int i = 0; i < 1; i++)
                        {
                            AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 5, FindCoordsToNewObj((1, fieldProperties.width / 5))), 1, fieldProperties.width / 5);

                            AddToField(Wall.CreateVerticalWall(fieldProperties.width / 5, FindCoordsToNewObj((fieldProperties.width / 5, 1))), fieldProperties.width / 5, 1);
                        }

                        for (int i = 0; i < 14; i++)
                        {

                            AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);

                            AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        }

                        landscapeShot = gameField;
                    }
                    else
                    {
                        Console.WriteLine("Данный режим создания ландшафта недоступен для поля заданных размеров.");
                        if (fieldProperties.height > 20 && fieldProperties.width > 20)
                        {
                            Console.WriteLine("Создан ландшафт типа \"Middle\" ");
                            goto case LandScapeType.Middle;
                        }
                        else
                        {
                            Console.WriteLine("Создан ландшафт типа \"Small\" ");
                            goto case LandScapeType.Small;
                        }
                    }
                    break;
            }
        }

        public bool AddObjectToField(Enemy enemy)
        {
            if (enemy.Img != '\0')
            {
                if (enemy.Position.x < gameField.GetLength(1) && enemy.Position.y < gameField.GetLength(0))
                {
                    if (grassElements.Contains(gameField[enemy.Position.x, enemy.Position.y].img))
                    {
                        gameField[enemy.Position.x, enemy.Position.y] = new ColorAndImg(enemy.Color, enemy.Img);
                        return true;
                    }
                    else
                        return false;
                }
            }
                return false;
        }

        public bool AddObjectToField(Collectables collectable)
        {
            if (collectable.Img != '\0')
            {
                if (collectable.Position.x < gameField.GetLength(1) && collectable.Position.y < gameField.GetLength(0))
                {
                    if (grassElements.Contains(gameField[collectable.Position.x, collectable.Position.y].img))
                    {
                        gameField[collectable.Position.x, collectable.Position.y] = new ColorAndImg(collectable.Color, collectable.Img);
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public bool AddObjectToField(LandScapeElem landscapeItem)
        {
            if (landscapeItem.Img != '\0')
            {
                if (landscapeItem.Position.x + landscapeItem.Width < gameField.GetLength(1) && landscapeItem.Position.y + landscapeItem.Height < gameField.GetLength(0))
                {
                    for (int i = landscapeItem.Position.y; i < landscapeItem.Position.y + landscapeItem.Height; i++)
                        for (int j = landscapeItem.Position.x; j < landscapeItem.Position.x + landscapeItem.Width; j++)
                        {
                            if (!grassElements.Contains(gameField[i, j].img))
                            {
                                return false;
                            }
                        }
                    for (int i = landscapeItem.Position.y; i < landscapeItem.Position.y + landscapeItem.Height; i++)
                        for (int j = landscapeItem.Position.x; j < landscapeItem.Position.x + landscapeItem.Width; j++)
                            gameField[i, j] = new ColorAndImg(landscapeItem.Color, landscapeItem.Img);
                    return true;
                }
            }
            return false;
        }

        void SpawnUser()
        {
            user = new User(FindCoordsToNewObj((0, 1)));
            AddToField(user, 1, 1);
        }

        void SpawnItem(Collectables c, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                c.Position = FindCoordsToNewObj((1, 1));
                AddToField(c, 1, 1);
            }

            collectablesCount += amount;
        }

        private void SpawnCollectables<T>(T collectable, CollectablesAmount amount) where T: Collectables, new()
        {
            var curCollectable = new T();

            switch (collectable)
            {
                case Apple a:
                    curCollectable.Img = 'A';
                    curCollectable.Color = ConsoleColor.Red;
                    break;
                case Crisps c:
                    curCollectable.Img = 'C';
                    curCollectable.Color = ConsoleColor.Yellow;
                    break;
                case Beer b:
                    curCollectable.Img = '8';
                    curCollectable.Color = ConsoleColor.DarkYellow;
                    break;
                default:
                    break;
            }

            switch (amount)
            {
                case CollectablesAmount.Some:
                    SpawnItem(curCollectable, 2);
                    break;

                case CollectablesAmount.Many:
                    if (fieldProperties.height > 20 && fieldProperties.width > 20)
                    {
                        SpawnItem(curCollectable, 4);
                    }
                    else
                    {
                        Console.WriteLine("Данный режим спауна предметов недоступен для поля заданных размеров.");
                        Console.WriteLine("Размещено \"Some\" предметов");
                        goto case CollectablesAmount.Some;
                    }
                    break;

                case CollectablesAmount.ChestOfTreasures:
                    if (fieldProperties.height > 30 && fieldProperties.width > 30)
                    {
                        SpawnItem(curCollectable, 12);
                    }
                    else
                    {
                        Console.WriteLine("Данный режим спауна предметов недоступен для поля заданных размеров.");
                        if (fieldProperties.height > 20 && fieldProperties.width > 20)
                        {
                            Console.WriteLine("Размещено \"Many\" предметов");
                            goto case CollectablesAmount.Many;
                        }
                        else
                        {
                            Console.WriteLine("Размещено \"Some\" предметов");
                            goto case CollectablesAmount.Some;
                        }
                    }
                    break;
            }

        }

        private void SpawnEnemies<T>(T enemy, EnemyAmount amount) where T : Enemy, new()
        {
            var curEnemy  = new T();

            switch (enemy)
            {
                case Wolf w:
                    curEnemy.Img = 'W';
                    break;
                case Bear b:
                    curEnemy.Img = 'B';
                    break;
                case Snake S:
                    curEnemy.Img = 'S';
                    break;
                default:
                    break;
            }

            switch (amount)
            {
                
                case EnemyAmount.Some:
                    curEnemy.Position = FindCoordsToNewObj((1, 1));
                    curEnemy.AutoLeveling(1);
                    AddToField(curEnemy, 1, 1);
                    break;

                case EnemyAmount.Many:
                    if (fieldProperties.height > 20 && fieldProperties.width > 20)
                    {
                        curEnemy.Position = FindCoordsToNewObj((1, 1));
                        curEnemy.AutoLeveling(2);
                        AddToField(curEnemy, 1, 1);

                        curEnemy.Position = FindCoordsToNewObj((1, 1));
                        curEnemy.AutoLeveling(3);
                        AddToField(curEnemy, 1, 1);

                        curEnemy.Position = FindCoordsToNewObj((1, 1));
                        curEnemy.AutoLeveling(4);
                        AddToField(curEnemy, 1, 1);

                    }
                    else
                    {
                        Console.WriteLine("Данный режим спауна врагов недоступен для поля заданных размеров.");
                        Console.WriteLine("Размещено \"Some\" врагов");
                        goto case EnemyAmount.Some;
                    }
                    break;

                case EnemyAmount.WILFDOREST:
                    if (fieldProperties.height > 30 && fieldProperties.height > 30)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            curEnemy.Position = FindCoordsToNewObj((1, 1));
                            curEnemy.AutoLeveling(2);
                            AddToField(curEnemy, 1, 1);
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            curEnemy.Position = FindCoordsToNewObj((1, 1));
                            curEnemy.AutoLeveling(3);
                            AddToField(curEnemy, 1, 1);
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            curEnemy.Position = FindCoordsToNewObj((1, 1));
                            curEnemy.AutoLeveling(4);
                            AddToField(curEnemy, 1, 1);
                        }

                        curEnemy.Position = FindCoordsToNewObj((1, 1));
                        curEnemy.AutoLeveling(5);
                        AddToField(curEnemy, 1, 1);
                    }
                    else
                    {
                        Console.WriteLine("Данный режим спауна врагов недоступен для поля заданных размеров.");
                        if (fieldProperties.height > 20 && fieldProperties.width > 20)
                        {
                            Console.WriteLine("Размещено \"Many\" врагов");
                            goto case EnemyAmount.Many;
                        }
                        else
                        {
                            Console.WriteLine("Размещено \"Some\" врагов");
                            goto case EnemyAmount.Some;
                        }
                    }
                    break;

                case EnemyAmount.None:
                    break;
            }
            
        }

        private void AddToField(GameObject obj, int height, int width)
        {
            for (int i = obj.Position.y; i < obj.Position.y + height; i++)
                for (int j = obj.Position.x; j < obj.Position.x + width; j++)
                    gameField[i, j] = new ColorAndImg(obj.Color, obj.Img);
        }

        private (int x, int y) FindCoordsToNewObj((int heigth, int width) size)
        {
            (int x, int y) pos;
            Random r = new Random();
            bool nextRandom;
            while (true)
            {
                nextRandom = false;
                pos = (r.Next(gameField.GetLength(1)), r.Next(gameField.GetLength(0)));
                if (pos.x + size.width < gameField.GetLength(1) && pos.y + size.heigth < gameField.GetLength(0))
                    for (int i = pos.y; i < pos.y + size.heigth; i++)
                    {
                        for (int j = pos.x; j < pos.x + size.width; j++)
                        {
                            if (!grassElements.Contains(gameField[i, j].img))
                            {
                                nextRandom = true;
                                break;
                            }
                        }
                        if (nextRandom)
                            break;
                    }
                else
                    nextRandom = true;
                if (!nextRandom)
                    return pos;
            }
        }

        Keys ParseToKeys(string key)
        {
            switch (key)
            {
                case "RightArrow":
                    return Keys.Rigth;
                case "LeftArrow":
                    return Keys.Left;
                case "UpArrow":
                    return Keys.Up;
                case "DownArrow":
                    return Keys.Down;
                case "Escape":
                    return Keys.Escape;
                default:
                    return Keys.None;
            }
        }

        void MoveUser(int x, int y)
        {
            if (user.Position.x + x < fieldProperties.width 
                && user.Position.y + y < fieldProperties.height
                && user.Position.x + x > -1 && user.Position.y + y > -1)
            {
                if (enemies.Contains(gameField[user.Position.y + y, user.Position.x + x].img))
                {
                    Console.WriteLine("Oops, you died. Game Over. ");
                    return;
                }
                if (collectables.Contains(gameField[user.Position.y + y, user.Position.x + x].img))
                {
                    collectablesCount--;
                    gameField[user.Position.y + y, user.Position.x + x] = new ColorAndImg(ConsoleColor.Green, grassElements[new Random().Next(0, grassElements.Length)]);
                    user.Position = (user.Position.x + x, user.Position.y + y);
                }
                if (grassElements.Contains(gameField[user.Position.y + y, user.Position.x + x].img) || gameField[user.Position.y + y, user.Position.x + x].img == char.ConvertFromUtf32(15)[0])
                {
                    user.Position = (user.Position.x + x, user.Position.y + y);
                    gameField[user.Position.y + y, user.Position.x + x] = new ColorAndImg(ConsoleColor.Cyan, 'U');
                    gameField[user.Position.y + y, user.Position.x + x] = landscapeShot[user.Position.x, user.Position.y];
                }
            }
        }
        
    }
}
