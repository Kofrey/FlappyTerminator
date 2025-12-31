using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private float _offset = 1f; 

    private bool _isReady = true;

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
            Bullet bullet = Instantiate(_prefab); 
            bullet.transform.position = transform.position + transform.right * _offset;
            bullet.SetDirection(transform.right);
        }
    }

    private IEnumerator TrackCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _isReady = true;
    }
}
