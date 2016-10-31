using System;
using ConsoleEngine.Core;

namespace ConsoleEngine.Core.Interface
{
    public class Button
    {
        public string content;
        public ConsoleColor color;
        public Transform transform;

        //Constructor
        public Button(string content, ConsoleColor color, Vector2 position)
        {
            this.content = content;
            this.color = color;
            transform = new Transform(position);
        }
    }
}