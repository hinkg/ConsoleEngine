using System;

namespace ConsoleGame.Core.Objects 
{
    public class Rectangle
    {
        public string content;
        public ConsoleColor color;
        public Vector2 position1;
        public Vector2 position2;

        public Rectangle(string content, ConsoleColor color, Vector2 position1, Vector2 position2)
        {
            this.content = content;
            this.color = color;
            this.position1 = position1;
            this.position2 = position2;
        }
    }
}