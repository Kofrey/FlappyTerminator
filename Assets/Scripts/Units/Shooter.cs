using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _cooldown = 1.8f;
    [SerializeField] private float _offset = 1f; 
    [SerializeField] private float _bulletSpeed;

    private bool _isReady = true;
    private WaitForSeconds _waitTime;

    public event Action<Vector3, Vector3> Shooted;

    private void Awake() 
    {
        _waitTime = new WaitForSeconds(_cooldown); 
    }

    private void OnEnable()
    {
        _isReady = true;
    }

    public void Shoot()
    {
        if(_isReady)
        {
            _isReady = false;
            StartCoroutine(TrackCooldown());
            Vector3 position = transform.position + transform.right * _offset;

            Shooted?.Invoke(position, transform.right * _bulletSpeed);
        }
    }

    private IEnumerator TrackCooldown()
    {
        yield return _waitTime;
        _isReady = true;
    }
}
