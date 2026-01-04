using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    protected override void ActionOnRelease(Bullet obj)
    {
        obj.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        obj.gameObject.SetActive(false); 
    }
}
