using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int _currentHealth;
    private Rigidbody2D _rigidBody;

    public int MaxHealth = 100; 
    public int StartingHealth = 100;

    #region properties

    public int CurrentHealth {
        get {
            return _currentHealth;
        }
        set {
            _currentHealth = (value > MaxHealth ? MaxHealth : value);
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

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        CurrentHealth = StartingHealth;

        TriggerHealthGainedEvent(StartingHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(CollisionHandler(collision));
    }

    #region Private methods

    private IEnumerator CollisionHandler(Collision2D collision)
    {
        //Vector3 initialVelocity, newVelocity;

        ////get velocity
        //initialVelocity = _rigidBody.velocity;

        ////wait for new updates, by trial and error.
        //yield return null;
        //yield return null;
        //yield return null;
        //yield return null;
        //yield return null;

        ////get new velocity
        //newVelocity = _rigidBody.velocity;

        ////impulse = magnitude of change
        //Vector3 result = initialVelocity - newVelocity;
        var result = collision.relativeVelocity.magnitude;

        var healthLost = (int)(result * result * 1.5);

        CurrentHealth -= healthLost;

        TriggerHealthLostEvent(healthLost);
        return null;
    }

    private void TriggerHealthGainedEvent(int healthGained)
    {
        Scene.Events.TriggerEvent(
            AvailableEvents.PLAYER_LOST_HEALTH,
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
        Scene.Events.TriggerEvent(
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
