using System;

namespace ConsoleEngine.Core.Objects 
{
    public class Text 
    {
        public string content;
        public ConsoleColor color;
        public Transform transform;

        //Constructor
        public Text(string content, ConsoleColor color, Vector2 position)
        {
            this.content = content;
            this.color = color;
            transform = new Transform(position);
        }
    }
}