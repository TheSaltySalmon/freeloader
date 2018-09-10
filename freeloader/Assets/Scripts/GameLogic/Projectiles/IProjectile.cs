using System;
namespace FreeLoader.GameLogic.Projectiles
{
    public interface IProjectile
    {
        int Damage { get; }
        void HandleFixedUpdate();
    }

}