using ConsoleEngine;

namespace ConsoleEngine.Objects
{
    public interface IInterfaceObject
    {
        void Draw(World world);

        void Add(InterfaceManager uinterface);
    }
}