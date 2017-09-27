using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.Player;

public class PlayerComponent : MonoBehaviour, IComponent
{
    private PlayerController _player;

    public PlayerController Player
    {
        get
        {
            return _player;
        }
    }

    public void Awake()
    {
        _player = new GameLogic.Player.PlayerController(gameObject, this);
    }
    void FixedUpdate()
    {
        _player.HandleFixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _player.HandleCollision(collision);
    }

}