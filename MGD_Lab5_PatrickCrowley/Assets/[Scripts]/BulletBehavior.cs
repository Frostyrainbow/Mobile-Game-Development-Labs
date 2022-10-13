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
    public float speed;
    public BulletDirection direction;
    public Vector3 velocity;
    public ScreenBounds bounds;
    public BulletManager bManager;

     public void SetDirection(BulletDirection direction)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up * speed;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right * speed;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down * speed;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left * speed;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bManager = FindObjectOfType<BulletManager>();

        SetDirection(BulletDirection.UP);
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
            bManager.ReturnBullet(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bManager.ReturnBullet(this.gameObject);
    }
}
