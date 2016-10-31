using System;
using ConsoleEngine.Core.Graphics;

namespace ConsoleEngine.Core.Interface
{
    public class Button : IInterface
    {
        public string content;
        public ConsoleColor selectedColor;
        public ConsoleColor unselectedColor;
        public Transform transform;

        public int index;
        public bool selected = false;

        //Constructor
        public Button(string content, ConsoleColor color1, ConsoleColor color2, Vector2 position, int index)
        {
            this.content = content;
            this.selectedColor = color1;
            this.unselectedColor = color2;
            this.index = index;
            transform = new Transform(position);
        }

        public override void Draw(GraphicsDevice graphics)
        {
            int x = transform.position.x;
            int y = transform.position.y;

            ConsoleColor color = unselectedColor;
            if(selected) color = selectedColor;

            foreach (char c in content)
            {
                if (c == '\n')
                {
                    y++;
                    x = transform.position.x;
                }
                else
                    graphics.DrawPixel(x++, y, c, color);
            }
        }

        public override void Select()
        {
            selected = true;
        }

        public override void Unselect()
        {
            selected = false;
        }

        public override void Add(InterfaceManager uinterface)
        {
            uinterface.buttons.Add(this);
        }
    }
}