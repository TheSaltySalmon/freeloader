using System;
namespace FreeLoader.GameLogic.Player
{
    public interface IShipMovement
    {
        bool CanShipMove { get; }
        void HandleMovement(float horizontalMovement, float verticalMovement);
        bool IsPlayerAcceleratingShip { get; }
        bool IsPlayerRotatingShipLeft { get; }
        bool IsPlayerRotatingShipRight { get; }
    }
}
