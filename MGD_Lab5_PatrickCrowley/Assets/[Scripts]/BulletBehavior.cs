using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundery horizontal;
    public Boundery vertical;
}

public class BulletBehavior : MonoBehaviour
{
    public Bullet bulletObject;
    public Vector3 velocity;
    public ScreenBounds bounds;
    public BulletManager bManager;

     public void SetDirection(BulletDirection direction)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up * bulletObject.bulletSpeed;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right * bulletObject.bulletSpeed;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down * bulletObject.bulletSpeed;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left * bulletObject.bulletSpeed;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetUp(bulletObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void CheckBounds()
    {
        if (transform.position.x > bounds.horizontal.max || transform.position.x < bounds.horizontal.min || transform.position.y > bounds.vertical.max || transform.position.y < bounds.vertical.min)
        {
            bManager.ReturnBullet(this.gameObject, bulletObject.type);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bManager.ReturnBullet(this.gameObject, bulletObject.type);
    }

    public void SetUp(Bullet bullet)
    {
        bManager = FindObjectOfType<BulletManager>();

        SetDirection(bullet.direction);
        bounds.horizontal.min = bullet.minXBounds;
        bounds.horizontal.max = bullet.maxXBounds;
        bounds.vertical.min = bullet.minYBounds;
        bounds.vertical.max = bullet.maxYBounds;
    }
}
