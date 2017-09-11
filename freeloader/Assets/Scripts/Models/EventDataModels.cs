using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum EffectType
{
    Gained,
    Lost
}

namespace EventDataModels
{

    public class Health
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int HealthAmount { get; set; }
        public EffectType Effect { get; set; }
    }
}
