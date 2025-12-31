using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    [SerializeField] private Transform _container;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private ScoreCounter _scoreCounter;

    private Queue<Enemy> _pool;

    public IEnumerable<Enemy> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<Enemy>();
    }

    public virtual Enemy GetObject()
    {
        if (_pool.Count == 0)
        {
            Enemy obj = Instantiate(_prefab);
            obj.Hitted += OnObjectHitted;
            obj.transform.parent = _container;

            return obj;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Enemy obj)
    {
        _pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();
    }

    private void OnObjectHitted(Enemy enemy)
    {
        _scoreCounter.Add();
        PutObject(enemy);
    }
}
