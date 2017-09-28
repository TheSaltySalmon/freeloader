using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum EffectType
{
    Gained,
    Lost
}

namespace FreeLoader.EventDataModels
{

    public class Health
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int HealthAmount { get; set; }
        public EffectType Effect { get; set; }
    }

    public class Fuel
    {
        public int MaxFuel { get; set; }
        public int CurrentFuel { get; set; }
        public int FuelAmount { get; set; }
        public EffectType Effect { get; set; }
    }
}
