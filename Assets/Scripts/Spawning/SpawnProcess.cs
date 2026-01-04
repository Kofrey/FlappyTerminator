using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EnemySpawner))]
[RequireComponent(typeof(BulletSpawner))]
public class SpawnProcess : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private float _delay = 4f;
    [SerializeField] private float _lowerBound = 2f;
    [SerializeField] private float _upperBound = -2f;
    [SerializeField] private Shooter _playerShooter;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

    private void OnEnable()
    {
        _playerShooter.Shooted += OnShooted;        
    }

    private void OnDisable() 
    {
        _playerShooter.Shooted -= OnShooted;
    }

    private void Start()
    {
        StartCoroutine(GenerateEnemies(_delay));
    }

    public void Reset()
    {
        _enemySpawner.Reset();
        _bulletSpawner.Reset();
        StopAllCoroutines();
        StartCoroutine(GenerateEnemies(_delay));
    }

    public void OnShooted(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = _bulletSpawner.GetObject(); 
        
        bullet.transform.position = position;
        bullet.SetVelocity(velocity); 
    }

    private IEnumerator GenerateEnemies(float time)
    {
        var wait = new WaitForSeconds(time);

        while (enabled) 
        {
            SpawnEnemy();
            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _enemySpawner.GetObject();
        enemy.GetComponent<Shooter>().Shooted += OnShooted;
        enemy.transform.position = GetEnemySpawnPoint();  
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        enemy.GetComponent<Shooter>().Shooted -= OnShooted;
    }

    private Vector3 GetEnemySpawnPoint()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
        return spawnPoint;
    }
}
