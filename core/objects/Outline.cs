using System;

namespace ConsoleEngine.Core.Objects
{
    public class Outline
    {
        public string content;
        public ConsoleColor color;
        public Vector2 size;
        public Vector2 thickness;
        public Transform transform;

        //Constructor
        public Outline(string content, ConsoleColor color, Vector2 position, Vector2 size, Vector2 thickness)
        {
            this.content = content;
            this.color = color;
            this.size = size;
            this.thickness = thickness;
            transform = new Transform(position);

        }
    }
}