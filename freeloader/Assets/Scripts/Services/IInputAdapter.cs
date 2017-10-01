using System;
namespace FreeLoader.Services
{
    public interface IInputAdapter
    {
        bool IsAccelerating { get; }
        bool IsDeAccelerating { get; }
        bool IsRotating { get; }
        bool IsRotatingLeft { get; }
        bool IsRotatingRight { get; }
        float HorizontalAxis { get; }
        float VerticalAxis { get; }
    }
}
