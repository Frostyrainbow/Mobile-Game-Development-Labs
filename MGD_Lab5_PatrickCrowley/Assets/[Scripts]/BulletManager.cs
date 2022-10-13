using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public Queue<GameObject> bulletPool;
    public GameObject bulletPrefab;
    public int maxBullets = 50;
    public int bulletCount = 0;
    public Transform bulletParent;
    public int activeBullets;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>();
        BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullet.transform.SetParent(bulletParent);
        bulletPool.Enqueue(bullet);
    }

    void BuildBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            CreateBullet();
        }

        bulletCount = bulletPool.Count;
    }

    public GameObject GetBullet(Vector2 position, BulletDirection direction)
    {
        if (bulletPool.Count < 1)
        {
            CreateBullet();
        }

        var bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = position;
        bullet.GetComponent<BulletBehavior>().SetDirection(direction);
        activeBullets++;
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
        activeBullets--;
    }
}
