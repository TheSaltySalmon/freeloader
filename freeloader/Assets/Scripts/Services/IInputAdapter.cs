using System;
namespace FreeLoader.Services
{
    public interface IInputAdapter
    {
        // Movement
        bool IsAccelerating { get; }
        bool IsDeAccelerating { get; }
        bool IsRotating { get; }
        bool IsRotatingLeft { get; }
        bool IsRotatingRight { get; }
        float HorizontalAxis { get; }
        float VerticalAxis { get; }

        // Actions
        bool IsFiring { get; }
    }
}
