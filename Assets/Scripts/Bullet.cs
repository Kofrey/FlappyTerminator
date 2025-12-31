using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Vector3 _direction;

    private void Start()
    {
        _rigidbody.linearVelocity = _direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy _))
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.TryGetComponent(out Bullet _))
            Destroy(this.gameObject);        
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
