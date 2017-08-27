using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;

    private bool hasExploded = false;
    private Rigidbody2D rigidBody;

    #region properties

    public bool IsDamaged {
        get {
            return CurrentHealth < StartingHealth;
        }
    }

    public bool IsAlive { 
        get {
            return CurrentHealth > 0;
        }
        set
        {
            CurrentHealth = value == true ? CurrentHealth : 0;
        }
    }

    #endregion

	// Use this for initialization
	void Start ()
    {
        CurrentHealth = StartingHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {	
        if (!IsAlive && !hasExploded)
        {
            ExplodeShip();
        }
    }

    private void ExplodeShip()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ship collision. Health = " + CurrentHealth + "| Alive = " + IsAlive);

        // On collision with something, start a coroutine
        StartCoroutine("OnImpulse");
    }

    private IEnumerator OnImpulse(){
    
        Vector3 initialVelocity, newVelocity;
        int minForceToBreak = 1;

        //get velocity
        initialVelocity = rigidBody.velocity;

        //wait for new updates, by trial and error, 3 frames seems to get me the correct effect.
        yield return null;
        yield return null;
        yield return null;

        //get new velocity
        newVelocity = rigidBody.velocity;

        //impulse = magnitude of change
        Vector3 result = initialVelocity - newVelocity;

        Debug.Log ("Impulse taken: " + result.magnitude);

        if (result.magnitude > minForceToBreak)
        {
            //Destroy(gameObject);
            IsAlive = false;
        }

        /*
           * Let me explain. Impulse force on an object is determined by the instantaneous change in velocity. 
           * which in this case i have estimated by measuring change in velocity over 3 frames as what i have done. 
           * the reason for 3 frames is by trial and error as it seems to net me the best stable result in my case. 
           * As sometimes when determined over 1 frame, it kinda misses the computation.
        */
    }
}
