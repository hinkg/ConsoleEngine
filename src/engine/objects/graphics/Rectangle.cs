using System;
using ConsoleEngine.Core;

namespace ConsoleEngine.Objects
{
    public class Rectangle : IGraphicsObject
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

        public void Add(GraphicsDevice graphics)
        {
            graphics.objects.Add(this);
        }

        public void Draw(GraphicsDevice graphics)
        {
            int charOffset = 0;

            for (int y = transform.position.y; y < transform.position.y + size.y; y++)
            {
                for (int x = transform.position.x; x < transform.position.x + size.x; x++)
                {
                    graphics.SetTile(x, y, content[charOffset++], color);

                    if (charOffset >= content.Length)
                        charOffset = 0;
                }
            }
        }
    }
}