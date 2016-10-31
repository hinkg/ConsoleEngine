using System;

namespace ConsoleGame.Core.Objects 
{
    public class Text 
    {
        public string content;
        public ConsoleColor color;
        public Vector2 position;

        public Text(string content, ConsoleColor color, Vector2 position)
        {
            this.content = content;
            this.color = color;
            this.position = position;
        }
    }
}