using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Shooter))]
public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private Shooter _shooter;
    [SerializeField] private float _shootRate = 0.3f;

    private Coroutine _coroutine;
    private CollisionHandler _handler;

    public event Action<Enemy> Hitted;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _handler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ShootCaller());
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _handler.CollisionDetected -= ProcessCollision;
    }

    private IEnumerator ShootCaller()
    {
        while(enabled)
        {
            _shooter.Shoot();
            yield return new WaitForSeconds(_shootRate);
        }
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet)
        {
            Hitted?.Invoke(this);
        }
    }
}
