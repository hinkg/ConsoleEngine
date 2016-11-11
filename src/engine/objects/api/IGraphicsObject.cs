using ConsoleEngine.Core;

namespace ConsoleEngine.Objects
{
    public interface IGraphicsObject
    {
        void Draw(World world);

        void Add(World world);
    }
}
