using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Game
{
    class GameEngine
    {
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

        ColorAndImg[,] gameField;
        char[] grassElements = {',', '\'', ';', '\"', '^'};
        List<GameObject> gameobjects;

        public void BuildField(int width, int height, params GameObject[] objects)
        {
            gameField = new ColorAndImg[width, height];
            Random choseElement = new Random();
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
                        break;
                }

                gameobjects.Add(item);
            }
        }

        public bool AddObjectToField(Collectables collectable)
        {
            if (collectable.Position.x < gameField.GetLength(0) && collectable.Position.y < gameField.GetLength(1))
            {
                if (grassElements.Contains(gameField[collectable.Position.x, collectable.Position.y].img))
                    return false;
                else
                {
                    gameField[collectable.Position.x, collectable.Position.y] = new ColorAndImg(collectable.Color, collectable.Img);
                }

            }
            return false;
        }

        public bool AddObjectToField(Enemy enemy)
        { 
            if (enemy.Position.x < gameField.GetLength(0) && enemy.Position.y < gameField.GetLength(1))
            {
                if (grassElements.Contains(gameField[enemy.Position.x, enemy.Position.y].img))
                    return false;
                else
                {
                    gameField[enemy.Position.x, enemy.Position.y] = new ColorAndImg(enemy.Color, enemy.Img);
                }

            }
            return false;
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
                    {
                        gameField[i, j] = new ColorAndImg(landscapeItem.Color, landscapeItem.Img);
                    }
                return true;
            }
            return false;
        }

        public void ShowField()
        {
            Console.WindowWidth = 60;
            Console.WindowHeight = 60;
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    Console.ForegroundColor = gameField[i, j].color;
                    Console.Write(gameField[i, j].img);
                }
                Console.WriteLine();
            }
            
        }
    }
}
