using UnityEngine;
using System;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private ScoreCounter _scoreCounter;

    public event Action<Enemy> EnemyDestroyed;

    protected override void OnObjectHitted(SpawnedObject obj)
    {
        _scoreCounter.Add();
        
        Pool.Release(obj as Enemy);
    } 

    protected override void OnObjectDestroy(Enemy obj)  
    {
        (obj as SpawnedObject).Hitted -= OnObjectHitted;
        EnemyDestroyed?.Invoke(obj);
        Destroy(obj.gameObject);
    }
}
