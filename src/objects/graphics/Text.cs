using System;
using ConsoleEngine;

namespace ConsoleEngine.Objects
{
    public class Text : IGraphicsObject
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

        public void Draw(World world)
        {
            int x = transform.Position.X;
            int y = transform.Position.Y;

            foreach (char c in content)
            {
                if (c == '\n')
                {
                    y++;
                    x = transform.Position.X;
                }
                else
                {
                    world.SetTile(x++, y, c, color);
                }
            }
        }
    }
}