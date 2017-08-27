using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;

    public bool IsDamaged {
        get {
            return CurrentHealth < StartingHealth;
        }
    }

    public bool isAlive { 
        get {
            return CurrentHealth <= 0;
        }
    }

	// Use this for initialization
	void Start () {
        CurrentHealth = StartingHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
