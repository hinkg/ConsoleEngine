using System;
using ConsoleEngine;

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

        public void Add(World world)
        {
            world.objects.Add(this);
        }

        public void Draw(World world)
        {
            int charOffset = 0;

            for (int y = transform.Position.Y; y < transform.Position.Y + size.Y; y++)
            {
                for (int x = transform.Position.X; x < transform.Position.X + size.X; x++)
                {
                    world.SetTile(x, y, content[charOffset++], color);

                    if (charOffset >= content.Length)
                    {
                        charOffset = 0;
                    }
                }
            }
        }
    }
}