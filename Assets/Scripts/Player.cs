using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Shooter))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private ScoreCounter _scoreCounter;
    private CollisionHandler _handler;
    private Shooter _shooter;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<CollisionHandler>();
        _mover = GetComponent<PlayerMover>();
        _shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _shooter.Shoot();
        }
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Enemy || interactable is Bullet)
        {
            GameOver?.Invoke();
        }

        else if(interactable is ScoreZone) 
        {
            _scoreCounter.Add();
        }
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }
}
