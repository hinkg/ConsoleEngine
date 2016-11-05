using System;

namespace ConsoleEngine.Core.Graphics
{
    public class Rectangle : IObject
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

        public void Draw(GraphicsDevice graphics)
        {
            int charOffset = 0;

            for (int y = transform.position.y - (size.y / 2); y < transform.position.y + (size.y / 2); y++)
            {
                for (int x = transform.position.x - (size.x / 2); x < transform.position.x + (size.x / 2); x++)
                {
                    graphics.DrawPixel(x, y, content[charOffset++], color);

                    if (charOffset >= content.Length)
                        charOffset = 0;
                }
            }
        }
    }
}