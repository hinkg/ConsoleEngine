using System;
using System.IO;

namespace ConsoleEngine.Core.Graphics
{
    public class Image : IObject
    {
        public string[] lines;
        public Transform transform;

        public Image(string filepath, Vector2 position)
        {
            lines = File.ReadAllLines(filepath);
            this.transform = new Transform(position);
        }

        public void Add(GraphicsDevice graphics)
        {
            graphics.objects.Add(this);
        }

        public void Draw(GraphicsDevice graphics)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    graphics.DrawPixel(transform.position.x + x, transform.position.y + y, lines[y].ToCharArray()[x], ConsoleColor.White);
                }
            }
        }
    }
}