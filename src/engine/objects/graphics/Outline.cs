using System;
using ConsoleEngine.Core;

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
                (transform.position.x - size.x / 2) + thickness.x,
                (transform.position.y - size.y / 2) + thickness.y);

            Vector2 spaceEnd = new Vector2(
                (transform.position.x + size.x / 2) - thickness.x,
                (transform.position.y + size.y / 2) - thickness.y);

            int charOffset = 0;

            for (int y = transform.position.y - (size.y / 2); y < transform.position.y + (size.y / 2); y++)
            {
                for (int x = transform.position.x - (size.x / 2); x < transform.position.x + (size.x / 2); x++)
                {
                    if (spaceStart.y <= y && y < spaceEnd.y)
                    {
                        if (spaceStart.x <= x && x < spaceEnd.x)
                            continue;
                        else if (x == spaceEnd.x)
                            charOffset = 0;
                    }

                    world.SetTile(x, y, content[charOffset++], color);

                    if (charOffset >= content.Length)
                        charOffset = 0;
                }

                charOffset = 0;
            }
        }
    }
}