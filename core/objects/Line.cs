using System;

namespace ConsoleGame.Core.Objects 
{
    public class Line 
    {
        public string content;
        public ConsoleColor color;
        public Vector2 position1;
        public Vector2 position2;

        public Line(string content, ConsoleColor color, Vector2 position1, Vector2 position2)
        {
            this.content = content;
            this.color = color;
            this.position1 = position1;
            this.position2 = position2;
        }
    }    
}