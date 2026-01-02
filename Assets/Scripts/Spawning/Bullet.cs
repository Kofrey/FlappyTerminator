using UnityEngine;

public class Bullet : SpawnedObject, IInteractable
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private CollisionHandler _handler;

    private Vector3 _direction;

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision; 
    }

    private void OnDisable()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _handler.CollisionDetected -= ProcessCollision;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void Fly()
    {
        _rigidbody.linearVelocity = _direction * _speed;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        ProcessCollision(null);      
    }
}
