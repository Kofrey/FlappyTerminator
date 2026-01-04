using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private PlayerInput _input;
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
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _input.InputPerformed += OnInputPerformed;
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _input.InputPerformed -= OnInputPerformed;
        _handler.CollisionDetected -= ProcessCollision;
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }

    private void OnInputPerformed(KeyCode code)
    {
        switch (code)
        {
            case PlayerInput.ShootKey:
                _shooter.Shoot();
                break;

            case PlayerInput.LeapKey:
                _mover.Leap();
                break;
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
}
