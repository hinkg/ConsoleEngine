using System;

namespace ConsoleEngine.Core.Objects
{
    public class Rectangle
    {
        public string content;
        public ConsoleColor color;
        public Vector2 size;
        public Transform transform;

        //Constructor
        public Rectangle(string content, ConsoleColor color, Vector2 size, Vector2 position)
        {
            this.content = content;
            this.color = color;
            this.size = size;
            transform = new Transform(position);
        }
    }
}