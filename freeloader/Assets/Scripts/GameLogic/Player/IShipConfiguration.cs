using System;
using UnityEngine;
using FreeLoader.GameLogic.Weapons;
namespace FreeLoader.GameLogic.Player
{
    public interface IShipConfiguration
    {
        IWeapon CurrentWeapon { get; }
    }
}
