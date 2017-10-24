using System;
using ConsoleEngine;

namespace ConsoleEngine.Objects
{
    public class Line : IGraphicsObject
    {
        public string content;
        public ConsoleColor color;
        public Vector2 size;
        public Transform transform;

        //Constructor
        public Line(string content, ConsoleColor color, Vector2 position, Vector2 size)
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
            int x1 = transform.Position.X;
            int y1 = transform.Position.Y;
            int x2 = transform.Position.X + size.X - 1;
            int y2 = transform.Position.Y + size.Y - 1;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            int charOffset = 0;

            while (true)
            {
                world.SetTile(x1, y1, content[charOffset++], color);

                if (charOffset >= content.Length)
                {
                    charOffset = 0;
                }

                if (x1 == x2 && y1 == y2)
                {
                    break;
                }

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
    }
}