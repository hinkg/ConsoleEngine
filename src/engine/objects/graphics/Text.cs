using System;
using ConsoleEngine.Core;

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

        public void Add(World world)
        {
            world.objects.Add(this);
        }

        public void Draw(World world)
        {
            int x = transform.position.x;
            int y = transform.position.y;

            foreach (char c in content)
            {
                if (c == '\n')
                {
                    y++;
                    x = transform.position.x;
                }
                else
                    world.SetTile(x++, y, c, color);
            }
        }
    }
}