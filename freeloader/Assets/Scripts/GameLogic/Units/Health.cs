using FreeLoader.Components;
using FreeLoader.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FreeLoader.GameLogic.Units
{
    public class Health : IHealth
    {
        public int MaxHealth = 100; 
        public int StartingHealth = 100;
        public float LostHealthCollisionFactor = 1.5f;

        private int _currentHealth;

        #region properties

        public int CurrentHealth {
            get {
                return _currentHealth;
            }
            set {
                _currentHealth = (value > MaxHealth ? MaxHealth : value);

                if (_currentHealth <= 0)
                {
                    TriggerDiedEvent();
                }
            }
        }

        public bool IsDamaged
        {
            get
            {
                return CurrentHealth < StartingHealth;
            }
        }

        public bool IsAlive
        {
            get
            {
                return CurrentHealth > 0;
            }
            set
            {
                CurrentHealth = value == true ? CurrentHealth : 0;
            }
        }

        #endregion

        public Health()
        {
            CurrentHealth = StartingHealth;

            TriggerHealthGainedEvent(StartingHealth);
        }

        public void HandleCollisionHealthLoss(float collisionVelocityMagnitude)
        {
            var healthLost = (int)(collisionVelocityMagnitude * collisionVelocityMagnitude * LostHealthCollisionFactor);

            CurrentHealth -= healthLost;

            TriggerHealthLostEvent(healthLost);
        }


        #region Private methods


        private void TriggerDiedEvent()
        {
            Game.Services.EventManager.TriggerEvent(
                AvailableEvents.PLAYER_DIED,
                null
            );
        }

        private void TriggerHealthGainedEvent(int healthGained)
        {
            Game.Services.EventManager.TriggerEvent(
                AvailableEvents.PLAYER_GAINED_HEALTH,
                new EventDataModels.Health
                {
                    MaxHealth = MaxHealth,
                    CurrentHealth = CurrentHealth,
                    HealthAmount = healthGained,
                    Effect = EffectType.Gained
                }
            );
        }

        private void TriggerHealthLostEvent(int healthLost)
        {
            Game.Services.EventManager.TriggerEvent(
                AvailableEvents.PLAYER_LOST_HEALTH,
                new EventDataModels.Health
                {
                    MaxHealth = MaxHealth,
                    CurrentHealth = CurrentHealth,
                    HealthAmount = healthLost,
                    Effect = EffectType.Lost
                }
            );
        }

        #endregion
    }
}
