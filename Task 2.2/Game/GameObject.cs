using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class GameObject
    {
        abstract public (int x, int y) Position { get; set; }
        abstract public ConsoleColor Color { get; set; }
        abstract public char Img { get; set; }

        abstract public new Type GetType();

    }
}
