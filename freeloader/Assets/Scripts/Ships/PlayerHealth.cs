using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;

    private bool _hasExploded = false;
    private Rigidbody2D rigidBody;
    private GameObject _burningEffect;

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
        CurrentHealth = StartingHealth;
        rigidBody = GetComponent<Rigidbody2D>();

        LoadPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        IfDeadHandler();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // On collision with something, start a coroutine
        StartCoroutine("OnImpulse");
    }

    #region Private methods

    private void IfDeadHandler()
    {
        if (!IsAlive && !_hasExploded)
        {
            ExplodeShip();
        }
    }

    private void ExplodeShip()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(0.58f, 0.10f, 0.10f);
        _burningEffect.SetActive(true);
        _hasExploded = true;

        Debug.Log("Exploded ship"); 
    }


    private void LoadPrefabs()
    {
        _burningEffect = SceneController.ObjectPool.GetSingle("ParticleSystems/BurningEffect");
        _burningEffect.transform.parent = transform;

        var newPosition = new Vector3(transform.position.x / 2, transform.position.y / 2, -5);
        _burningEffect.transform.position = newPosition;
    }

    private IEnumerator OnImpulse()
    {
        Vector3 initialVelocity, newVelocity;

        //get velocity
        initialVelocity = rigidBody.velocity;

        //wait for new updates, by trial and error.
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        //get new velocity
        newVelocity = rigidBody.velocity;

        //impulse = magnitude of change
        Vector3 result = initialVelocity - newVelocity;

        Debug.Log("Impulse taken: " + result.magnitude);

        CurrentHealth -= (int)(result.magnitude * 20);

        Debug.Log("Ship collision. Health = " + CurrentHealth + "| Alive = " + IsAlive);
    }

    #endregion
}
