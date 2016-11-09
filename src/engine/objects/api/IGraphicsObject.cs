using ConsoleEngine.Core;

namespace ConsoleEngine.Objects
{
    public interface IGraphicsObject
    {
        void Draw(GraphicsDevice graphics);

        void Add(GraphicsDevice graphics);
    }
}
