using System;
using UnityEngine;

namespace FreeLoader.GameLogic.Projectiles
{
    public interface IProjectile
    {
        int Damage { get; }
        void HandleFixedUpdate();
        void HandleCollision(Collision2D collision);
        void HandleFire(Transform weaponTransform);
        void HandleUpdate();
    }
}