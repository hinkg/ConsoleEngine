using ConsoleEngine.Core.Graphics;
using System;

namespace ConsoleEngine.Core.Interface
{
    public class Button : IObject
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

        public void Draw(GraphicsDevice graphics)
        {
            throw new NotImplementedException();
        }
    }
}