using System.Collections.Generic;
using ConsoleEngine.Objects;

namespace ConsoleEngine
{
    public class InterfaceManager
    {
        public List<Button> buttons { get; private set; }

        public InterfaceManager()
        {
            buttons = new List<Button>();
        }

        public void Draw(World world)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Draw(world);
        }

        public void SelectButton(int index)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Unselect();
            }

            buttons.Find(x => x.index == index).Select();
        }
    }
}