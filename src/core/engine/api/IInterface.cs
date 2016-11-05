using ConsoleEngine.Core.Interface;

namespace ConsoleEngine.Core
{
    public abstract class IInterface
    {
        public abstract void Draw(Graphics.GraphicsDevice graphics);

        public abstract void Select();
        public abstract void Unselect();

        public abstract void Add(InterfaceManager uinterface);
    }
}