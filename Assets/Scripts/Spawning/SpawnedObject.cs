using UnityEngine;
using System;

public class SpawnedObject : MonoBehaviour
{
    public event Action<SpawnedObject> Hitted;

    protected virtual void ProcessCollision(IInteractable _)
    {
        Hitted?.Invoke(this);
    }
}
