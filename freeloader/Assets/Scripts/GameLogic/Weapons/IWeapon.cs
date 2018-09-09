using System;
namespace FreeLoader.GameLogic.Weapons
{
    public interface IWeapon
    {
        int ReloadTime { get; }
        int Damage { get; }
        void Fire();
    }
}