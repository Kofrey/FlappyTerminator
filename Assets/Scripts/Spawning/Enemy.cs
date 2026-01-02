using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Shooter))]
public class Enemy : SpawnedObject, IInteractable
{
    [SerializeField] private Shooter _shooter;
    [SerializeField] private float _shootRate = 0.3f;

    private Coroutine _coroutine;
    private CollisionHandler _handler;

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
        var wait = new WaitForSeconds(_shootRate);

        while(enabled)
        {
            _shooter.Shoot();
            yield return wait;
        }
    }
}
