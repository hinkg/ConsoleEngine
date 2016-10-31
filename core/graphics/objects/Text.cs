using System;

namespace ConsoleEngine.Core.Graphics
{
    public class Text : IObject
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

        public void Draw(GraphicsDevice graphics)
        {
            int charOffset = 0;

            for (int x = transform.position.x; x < transform.position.x + content.Length; x++)
            {
                graphics.DrawPixel(x, transform.position.y, content[charOffset++], color);
            }
        }
    }
}