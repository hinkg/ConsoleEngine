using System;
using System.IO;
using ConsoleEngine.Core;

namespace ConsoleEngine.Objects
{
    public class Image : IGraphicsObject
    {
        public string text;
        public Transform transform;

        public Image(string filepath, Vector2 position)
        {
            text = string.Join("\n", File.ReadAllLines(filepath));
            this.transform = new Transform(position);
        }

        public void Add(GraphicsDevice graphics)
        {
            graphics.objects.Add(this);
        }

        public void Draw(GraphicsDevice graphics)
        {
            int x = transform.position.x;
            int y = transform.position.y;

            bool setColor = false;
            ConsoleColor color = ConsoleColor.White;

            foreach (char c in text)
            {
                if (c == '\\' && !setColor)
                {
                    setColor = true;
                }
                else if (c == '\n')
                {
                    y++;
                    x = transform.position.x;
                }
                else if(setColor && c != '\\')
                {
                    switch(char.ToLower(c))
                    {
                        case '0': color = ConsoleColor.Black; break;
                        case '1': color = ConsoleColor.DarkBlue; break;
                        case '2': color = ConsoleColor.DarkGreen; break;
                        case '3': color = ConsoleColor.DarkCyan; break;
                        case '4': color = ConsoleColor.DarkRed; break;
                        case '5': color = ConsoleColor.DarkMagenta; break;
                        case '6': color = ConsoleColor.DarkYellow; break;
                        case '7': color = ConsoleColor.Gray; break;
                        case '8': color = ConsoleColor.DarkGray; break;
                        case '9': color = ConsoleColor.Blue; break;
                        case 'a': color = ConsoleColor.Green; break;
                        case 'b': color = ConsoleColor.Cyan; break;
                        case 'c': color = ConsoleColor.Red; break;
                        case 'd': color = ConsoleColor.Magenta; break;
                        case 'e': color = ConsoleColor.Yellow; break;
                        case 'f': color = ConsoleColor.White; break;
                    }
                    setColor = false;
                }
                else
                {
                    graphics.SetTile(x, y, c, color);
                    x++;
                    setColor = false;
                }
            }
        }
    }
}