using System;
namespace FreeLoader.GameLogic.Units
{
    public interface IFuel
    {
        void CombustFuel(float combustionAmmount);
        int CurrentFuel { get; set; }
        bool IsOutOfFuel { get; set; }
    }
}
