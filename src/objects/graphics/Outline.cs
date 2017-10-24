using System;
using ConsoleEngine;

namespace ConsoleEngine.Objects
{
    public class Outline : IGraphicsObject
    {
        public string content;
        public ConsoleColor color;
        public Vector2 size;
        public Vector2 thickness;
        public Transform transform;

        //Constructor
        public Outline(string content, ConsoleColor color, Vector2 position, Vector2 size, Vector2 thickness)
        {
            this.content = content;
            this.color = color;
            this.size = size;
            this.thickness = thickness;
            transform = new Transform(position);
        }

        public void Add(World world)
        {
            world.objects.Add(this);
        }

        public void Draw(World world)
        {
            Vector2 spaceStart = new Vector2(
                (transform.Position.X - size.X / 2) + thickness.X,
                (transform.Position.Y - size.Y / 2) + thickness.Y);

            Vector2 spaceEnd = new Vector2(
                (transform.Position.X + size.X / 2) - thickness.X,
                (transform.Position.Y + size.Y / 2) - thickness.Y);

            int charOffset = 0;

            for (int y = transform.Position.Y - (size.Y / 2); y < transform.Position.Y + (size.Y / 2); y++)
            {
                for (int x = transform.Position.X - (size.X / 2); x < transform.Position.X + (size.X / 2); x++)
                {
                    if (spaceStart.Y <= y && y < spaceEnd.Y)
                    {
                        if (spaceStart.X <= x && x < spaceEnd.X)
                        {
                            continue;
                        }
                        else if (x == spaceEnd.X)
                        {
                            charOffset = 0;
                        }
                    }

                    world.SetTile(x, y, content[charOffset++], color);

                    if (charOffset >= content.Length)
                    {
                        charOffset = 0;
                    }
                }

                charOffset = 0;
            }
        }
    }
}