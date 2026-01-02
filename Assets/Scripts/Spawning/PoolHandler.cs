using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolHandler : MonoBehaviour 
{
    [SerializeField] private Transform _container;
    [SerializeField] private SpawnedObject _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<SpawnedObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<SpawnedObject>(
        createFunc: () => Create(_prefab),
        actionOnGet: (SpawnedObject) => ActionOnGet(SpawnedObject),
        actionOnRelease: (SpawnedObject) => SpawnedObject.gameObject.SetActive(false), 
        actionOnDestroy: (SpawnedObject) => OnObjectDestroy(SpawnedObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public SpawnedObject GetObject()
    {
        return _pool.Get();
    }

    public void ReleaseObject(SpawnedObject obj)
    {
        _pool.Release(obj);
    }

    public void Reset()
    {
        _pool.Clear();
    }

    private void ActionOnGet(SpawnedObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private SpawnedObject Create(SpawnedObject prefab)
    {
        return Instantiate(prefab);
    }

    private void OnObjectDestroy(SpawnedObject obj) 
    {
        Destroy(obj.gameObject);
    }
}
