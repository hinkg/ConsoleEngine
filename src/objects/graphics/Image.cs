using System;
using System.IO;
using ConsoleEngine;

namespace ConsoleEngine.Objects
{
    public class Image : IGraphicsObject
    {
        public string text;
        public Transform transform;

        public Image(string filepath, Vector2 position)
        {
            text = String.Join("\n", File.ReadAllLines(filepath));
            this.transform = new Transform(position);
        }

        public void Add(World world)
        {
            world.objects.Add(this);
        }

        public void Draw(World world)
        {
            int x = transform.Position.X;
            int y = transform.Position.Y;

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
                    x = transform.Position.X;
                }
                else if(setColor && c != '\\')
                {
                    switch(Char.ToLower(c))
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
                    world.SetTile(x, y, c, color);
                    x++;
                    setColor = false;
                }
            }
        }
    }
}