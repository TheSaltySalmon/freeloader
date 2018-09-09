using System;
using FreeLoader.Services;
namespace FreeLoader.GameLogic.Player
{
    public interface IPlayerActions
    {
        void HandleActions(IInputAdapter playerInput);
    }
}
