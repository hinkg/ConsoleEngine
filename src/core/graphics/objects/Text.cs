using System;

namespace ConsoleEngine.Core.Graphics
{
    public class Text : IObject
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

        public void Add(GraphicsDevice graphics)
        {
            graphics.objects.Add(this);
        }

        public void Draw(GraphicsDevice graphics)
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
                    graphics.SetTile(x++, y, c, color);
            }
        }
    }
}