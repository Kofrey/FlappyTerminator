using UnityEngine;

public class Bullet : SpawnedObject, IInteractable
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private CollisionHandler _handler;

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision; 
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.linearVelocity = velocity;
    }
}
