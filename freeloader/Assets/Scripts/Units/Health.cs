﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;

    private Rigidbody2D _rigidBody;

    #region properties

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(CollisionHandler());
    }

    #region Private methods

    private IEnumerator CollisionHandler()
    {
        Vector3 initialVelocity, newVelocity;

        //get velocity
        initialVelocity = _rigidBody.velocity;

        //wait for new updates, by trial and error.
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        //get new velocity
        newVelocity = _rigidBody.velocity;

        //impulse = magnitude of change
        Vector3 result = initialVelocity - newVelocity;

        CurrentHealth -= (int)(result.magnitude * 20);
    }

    #endregion
}
