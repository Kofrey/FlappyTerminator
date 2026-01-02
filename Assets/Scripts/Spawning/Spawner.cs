using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private PoolHandler _enemyPool;
    [SerializeField] private PoolHandler _bulletPool;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GenerateEnemies());
    }

    public void Reset()
    {
        _enemyPool.Reset();
        _bulletPool.Reset();
    }

    public void SpawnBullet(Vector3 position, Vector3 direction, float speed)
    {
        Bullet bullet = _bulletPool.GetObject() as Bullet; 
        bullet.Hitted += OnObjectHitted;
        bullet.transform.position = position;
        bullet.SetDirection(direction);
        bullet.SetSpeed(speed);  
        bullet.Fly();     
    }

    private IEnumerator GenerateEnemies()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled) 
        {
            SpawnEnemy();
            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = _enemyPool.GetObject() as Enemy;
        enemy.Hitted += OnObjectHitted;
        enemy.transform.position = spawnPoint;      
    }

    private void OnObjectDeactivated(SpawnedObject obj)
    {
        obj.Hitted -= OnObjectHitted;

        if(obj is Enemy)
        {
            _enemyPool.ReleaseObject(obj);
        }
        else if (obj is Bullet)
        {
            _bulletPool.ReleaseObject(obj);
        }
    }

    private void OnObjectHitted(SpawnedObject obj)
    {
        if(obj is Enemy)
            _scoreCounter.Add();

        OnObjectDeactivated(obj);
    }
}
