using ConsoleEngine.Core;

namespace ConsoleEngine.Objects
{
    public interface IInterfaceObject
    {
        void Draw(GraphicsDevice graphics);

        void Select();
        void Unselect();

        void Add(InterfaceManager uinterface);
    }
}