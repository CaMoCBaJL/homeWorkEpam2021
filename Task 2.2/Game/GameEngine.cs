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

        char[] enemies = { 'W', 'Z', 'B' };
        char[] collectables = { '8', 'C', 'A' };

        ColorAndImg[,] gameField;
        ColorAndImg[,] landscape;
        char[] grassElements = {',', '\'', ';', '\"', '^'};
        List<GameObject> gameobjects;
        (int height, int width) fieldProperties;
        User user;
        int collectablesCount;

        public void BuildField(int width, int height, params GameObject[] objects)
        {
            if(height < 10 && width < 10)
            {
                Console.WriteLine("Слишком маленькое поле!!!!");
                return;
            }
            gameField = new ColorAndImg[width, height];
            fieldProperties = (height, width);
            Random choseElement = new Random();
            collectablesCount = 0;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    gameField[i, j] = new ColorAndImg(ConsoleColor.Green,grassElements[choseElement.Next(0,grassElements.Length)]);
                }
            gameobjects = new List<GameObject>();
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

        public bool AddObjectToField(Collectables collectable)
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
            return false;
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

        void SpawnUser()
        {
            user = new User(FindCoordsToNewObj((0, 1)));
            AddToField(user, 1, 1);
        }

        public void GenerateItems(CollectablesAmount amount)
        {
            switch (amount)
            {
                case CollectablesAmount.Some:
                    AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                    AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                    AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                    AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                    AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                    AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                    collectablesCount = 6;
                    break;
                case CollectablesAmount.Many:
                    if (fieldProperties.height > 20 && fieldProperties.width > 20)
                    {
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        collectablesCount = 12;
                    }
                    else
                    {
                        Console.WriteLine("Данный режим спауна предметов недоступен для поля заданных размеров.");
                        Console.WriteLine("Размещено \"Some\" предметов");
                        goto case CollectablesAmount.Some;
                    }
                    break;
                case CollectablesAmount.ChestOfTreasures:
                    if (fieldProperties.height > 30 && fieldProperties.height > 30)
                    {
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Apple(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Crisps(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        AddToField(new Beer(FindCoordsToNewObj((1, 1))), 1, 1);
                        collectablesCount = 36;
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

        public void SpawnEnemies(EnemyAmount amount)
        {
            switch (amount)
            {
                case EnemyAmount.Some:
                    AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 1), 1, 1);
                    AddToField(new Bear(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                    AddToField(new Snake(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                    break;
                case EnemyAmount.Many:
                    if (fieldProperties.height > 20 && fieldProperties.width > 20)
                    {
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 4), 1, 1);
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
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Wolf(FindCoordsToNewObj((1, 1)), 5), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Bear(FindCoordsToNewObj((1, 1)), 5), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 3), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 2), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 4), 1, 1);
                        AddToField(new Snake(FindCoordsToNewObj((1, 1)), 5), 1, 1);
                        break;
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
            }
        }

        public void GenerateLandScape(LandScapeType type)
        {
            switch (type)
            {
                case LandScapeType.Small:
                    AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width/4, fieldProperties.width / 4)), fieldProperties.width / 4, fieldProperties.width / 4), 
                        fieldProperties.width / 4, fieldProperties.width / 4);
                    AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                        fieldProperties.width / 6, fieldProperties.width / 6);
                    AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 5, FindCoordsToNewObj((1, fieldProperties.width/5))), 1, fieldProperties.width / 5);
                    AddToField(Wall.CreateVerticalWall(fieldProperties.width / 5, FindCoordsToNewObj((fieldProperties.width / 5, 1))), fieldProperties.width / 5, 1);
                    landscape = gameField;
                    break;
                case LandScapeType.Middle:
                    if (fieldProperties.height > 20 && fieldProperties.width > 20)
                    {
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 3, fieldProperties.width / 3)), fieldProperties.width / 3, fieldProperties.width / 3), 
                            fieldProperties.width / 3, fieldProperties.width / 3);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                            fieldProperties.width / 6, fieldProperties.width / 6);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                            fieldProperties.width / 6, fieldProperties.width / 6);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                            fieldProperties.width / 6, fieldProperties.width / 6);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 5, FindCoordsToNewObj((1, fieldProperties.width / 5))), 1, fieldProperties.width / 5);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 5, FindCoordsToNewObj((fieldProperties.width / 5, 1))), fieldProperties.width / 5, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        landscape = gameField;
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
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 4, fieldProperties.width / 4)), fieldProperties.width / 4, fieldProperties.width / 4), 
                            fieldProperties.width / 4, fieldProperties.width / 4);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 4, fieldProperties.width / 4)), fieldProperties.width / 4, fieldProperties.width / 4), 
                            fieldProperties.width / 4, fieldProperties.width / 4);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                            fieldProperties.width / 6, fieldProperties.width / 6);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 6, fieldProperties.width / 6)), fieldProperties.width / 6, fieldProperties.width / 6), 
                            fieldProperties.width / 6, fieldProperties.width / 6);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 7, fieldProperties.width / 7)), fieldProperties.width / 7, fieldProperties.width / 7), 
                            fieldProperties.width / 7, fieldProperties.width / 7);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 7, fieldProperties.width / 7)), fieldProperties.width / 7, fieldProperties.width / 7), 
                            fieldProperties.width / 7, fieldProperties.width / 7);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 7, fieldProperties.width / 7)), fieldProperties.width / 7, fieldProperties.width / 7), 
                            fieldProperties.width / 7, fieldProperties.width / 7);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 7, fieldProperties.width / 7)), fieldProperties.width / 7, fieldProperties.width / 7), 
                            fieldProperties.width / 7, fieldProperties.width / 7);
                        AddToField(new Swamp(FindCoordsToNewObj((fieldProperties.width / 7, fieldProperties.width / 7)), fieldProperties.width / 7, fieldProperties.width / 7), 
                            fieldProperties.width / 7, fieldProperties.width / 7);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 5, FindCoordsToNewObj((1, fieldProperties.width / 5))), 1, fieldProperties.width / 5);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 5, FindCoordsToNewObj((fieldProperties.width / 5, 1))), fieldProperties.width / 5, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        AddToField(Wall.CreateHorizontalWall(fieldProperties.width / 8, FindCoordsToNewObj((1, fieldProperties.width / 8))), 1, fieldProperties.width / 8);
                        AddToField(Wall.CreateVerticalWall(fieldProperties.width / 8, FindCoordsToNewObj((fieldProperties.width / 8, 1))), fieldProperties.width / 8, 1);
                        landscape = gameField;
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
            return false;
        }

        private void AddToField(GameObject obj, int height, int width)
        {
            for (int i = obj.Position.y; i < obj.Position.y + height; i++)
                for (int j = obj.Position.x; j < obj.Position.x + width; j++)
                    gameField[i, j] = new ColorAndImg(obj.Color, obj.Img);
        }

        public bool AddObjectToField(LandScapeElem landscapeItem)
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
            return false;
        }

        public static Keys ParseToKeys(string key)
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

        void UpdateField()
        {
            Console.Clear();
            for (int i = 0; i < fieldProperties.width; i++)
            {
                for (int j = 0; j < fieldProperties.height; j++)
                {
                    Console.ForegroundColor = gameField[i, j].color;
                    Console.Write(gameField[i, j].img);
                }
                Console.WriteLine();
            }
        }

        ColorAndImg ScanNearestObjects()
        {
            List<char> elementsAroundUser = new List<char>();
            (int x, int y) startPosition;
            (int x, int y) finishPosition;
            if (user.Position.x - 1 > -1)
            {
                if (user.Position.y - 1 > -1)
                {
                    if (user.Position.x + 2 < fieldProperties.width)
                    {
                        if (user.Position.y + 2 < fieldProperties.height)
                        {
                            startPosition = (user.Position.x - 1, user.Position.y - 1);
                            finishPosition = (user.Position.x + 2, user.Position.y + 2);
                        }
                        else
                        {
                            startPosition = (user.Position.x - 1, user.Position.y - 1);
                            finishPosition = (user.Position.x + 2, fieldProperties.height);
                        }
                    }
                    else
                    {
                        if (user.Position.y + 2 < fieldProperties.height)
                        {
                            startPosition = (user.Position.x - 1, user.Position.y - 1);
                            finishPosition = (fieldProperties.width, user.Position.y + 2);
                        }
                        else
                        {
                            startPosition = (user.Position.x - 1, user.Position.y - 1);
                            finishPosition = (fieldProperties.width, fieldProperties.height);
                        }
                    }
                }
                else
                {
                    if (user.Position.x + 2 < fieldProperties.width)
                    {
                        startPosition = (user.Position.x - 1, 0);
                        finishPosition = (user.Position.x + 2, 2);
                    }
                    else
                    {
                        startPosition = (user.Position.x - 1, 0);
                        finishPosition = (fieldProperties.width, 2);
                    }
                }
            }
            else
            {
                startPosition = (0, 0);
                finishPosition = (2, 2);
            }

            for (int i = startPosition.x; i < finishPosition.x; i++)
            {
                for (int j = startPosition.y; j < startPosition.y; j++)
                    elementsAroundUser.Add(gameField[i, j].img);
            }

            int maxV = 0;
            char img = '\0';
            foreach (var item in elementsAroundUser)
            {
                if (elementsAroundUser.Count(c => c == item) > maxV)
                {
                    maxV = elementsAroundUser.Count(c => c == item);
                    img = item;
                }
                elementsAroundUser.RemoveAll(c => c == item);
            }

            if (grassElements.Contains(img))
                return new ColorAndImg(ConsoleColor.Green, grassElements[new Random().Next(0, grassElements.Length)]);
            else
                return new ColorAndImg(ConsoleColor.Gray, char.ConvertFromUtf32(15)[0]);
        }

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
                        if (user.Position.x + 1 < fieldProperties.width)
                        {
                            if (enemies.Contains(gameField[user.Position.y, user.Position.x + 1].img))
                            {
                                Console.WriteLine("Oops, you died. Game Over. ");
                                return;
                            }
                            if (collectables.Contains(gameField[user.Position.y, user.Position.x + 1].img))
                            {
                                collectablesCount--;
                                gameField[user.Position.y, user.Position.x + 1] = new ColorAndImg(ConsoleColor.Green, grassElements[new Random().Next(0, grassElements.Length)]);
                                user.Move(Keys.Rigth);
                            }
                            if (grassElements.Contains(gameField[user.Position.y, user.Position.x + 1].img) || gameField[user.Position.y, user.Position.x + 1].img == char.ConvertFromUtf32(15)[0])
                            {
                                user.Move(Keys.Rigth);
                                gameField[user.Position.y, user.Position.x] = new ColorAndImg(ConsoleColor.Cyan, 'U');
                                gameField[user.Position.y, user.Position.x - 1] = landscape[user.Position.x, user.Position.y];
                            }
                        }
                        break;
                    case Keys.Left:
                        if (user.Position.x - 1 > -1)
                        {
                            if (enemies.Contains(gameField[user.Position.y, user.Position.x - 1].img))
                            {
                                Console.WriteLine("Oops, you died. Game Over. ");
                                return;
                            }
                            if (collectables.Contains(gameField[user.Position.y, user.Position.x - 1].img))
                            {
                                collectablesCount--;
                                gameField[user.Position.y, user.Position.x - 1] = new ColorAndImg(ConsoleColor.Green, grassElements[new Random().Next(0, grassElements.Length)]);
                                user.Move(Keys.Left);
                            }
                            if (grassElements.Contains(gameField[user.Position.y, user.Position.x - 1].img) || gameField[user.Position.y, user.Position.x - 1].img == char.ConvertFromUtf32(15)[0])
                            {
                                user.Move(Keys.Left);
                                gameField[user.Position.y, user.Position.x] = new ColorAndImg(ConsoleColor.Cyan, 'U');
                                gameField[user.Position.y, user.Position.x + 1] = landscape[user.Position.x, user.Position.y];
                            }
                        }
                        break;

                    case Keys.Up:
                        if (user.Position.y - 1 > -1)
                        {
                            if (enemies.Contains(gameField[user.Position.y - 1, user.Position.x].img))
                            {
                                Console.WriteLine("Oops, you died. Game Over. ");
                                return;
                            }   
                            if (collectables.Contains(gameField[user.Position.y - 1, user.Position.x].img))
                            {
                                collectablesCount--;
                                gameField[user.Position.y - 1, user.Position.x] = new ColorAndImg(ConsoleColor.Green, 'U');
                                user.Move(Keys.Up);
                            }
                            if (grassElements.Contains(gameField[user.Position.y - 1, user.Position.x].img) || gameField[user.Position.y - 1, user.Position.x].img == char.ConvertFromUtf32(15)[0])
                            {
                                user.Move(Keys.Up);
                                gameField[user.Position.y, user.Position.x] = new ColorAndImg(ConsoleColor.Cyan, 'U');
                                gameField[user.Position.y + 1, user.Position.x] = landscape[user.Position.x, user.Position.y];
                            }
                        }
                        break;
                    case Keys.Down:
                        if (user.Position.y + 1 < fieldProperties.height)
                        {
                            if (enemies.Contains(gameField[user.Position.y + 1, user.Position.x].img))
                            {
                                Console.WriteLine("Oops, you died. Game Over. ");
                                return;
                            }
                            if (collectables.Contains(gameField[user.Position.y + 1, user.Position.x].img))
                            {
                                collectablesCount--;
                                gameField[user.Position.y + 1, user.Position.x] = new ColorAndImg(ConsoleColor.Green, grassElements[new Random().Next(0, grassElements.Length)]);
                                user.Move(Keys.Down);
                            }
                            if (grassElements.Contains(gameField[user.Position.y + 1, user.Position.x].img) || gameField[user.Position.y + 1, user.Position.x].img == char.ConvertFromUtf32(15)[0])
                            {
                                user.Move(Keys.Down);
                                gameField[user.Position.y, user.Position.x] = new ColorAndImg(ConsoleColor.Cyan, 'U');
                                gameField[user.Position.y - 1, user.Position.x] = landscape[user.Position.x, user.Position.y];
                            }
                        }
                        break;
                    case Keys.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
