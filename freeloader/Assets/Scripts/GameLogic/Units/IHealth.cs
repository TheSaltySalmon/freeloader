using System;
namespace FreeLoader.GameLogic.Units
{
    public interface IHealth
    {
        int CurrentHealth { get; set; }
        void HandleCollisionHealthLoss(float collisionVelocityMagnitude);
        bool IsAlive { get; set; }
        bool IsDamaged { get; }
    }
}
