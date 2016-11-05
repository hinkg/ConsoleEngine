using System;

namespace ConsoleEngine.Core.Graphics
{
    public class Line : IObject
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

        public void Draw(GraphicsDevice graphics)
        {
            int x1 = transform.position.x;
            int y1 = transform.position.y;
            int x2 = transform.position.x + size.x - 1;
            int y2 = transform.position.y + size.y - 1;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            int charOffset = 0;

            while (true)
            {
                graphics.DrawPixel(x1, y1, content[charOffset++], color);

                if (charOffset >= content.Length)
                    charOffset = 0;

                if (x1 == x2 && y1 == y2)
                    break;

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