using System;

namespace ConsoleEngine.Core.Objects
{
    public class Line
    {
        public string content;
        public ConsoleColor color;
        public Vector2 size;
        public Transform transform;

        //Constructor
        public Line(string content, ConsoleColor color, Vector2 position, Vector2 size)
        {
            this.content = content;
            this.color = color;
            this.size = size;
            transform = new Transform(position);
        }
    }
}