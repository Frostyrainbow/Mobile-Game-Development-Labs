using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> enemyBulletPool;

    public int playerMaxBullets = 50;
    public int playerBulletCount = 0;
    public int playerActiveBullets;
    public int enemyMaxBullets = 50;
    public int enemyBulletCount = 0;
    public int enemyActiveBullets;

    private BulletFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        playerBulletPool = new Queue<GameObject>();
        enemyBulletPool = new Queue<GameObject>();
        factory = GameObject.FindObjectOfType<BulletFactory>();
        BuildBulletPools();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void BuildBulletPools()
    {
        for (int i = 0; i < playerMaxBullets; i++)
        {
            playerBulletPool.Enqueue(factory.createBullet(0));
        }

        playerBulletCount = playerBulletPool.Count;

        for (int i = 0; i < enemyMaxBullets; i++)
        {
            enemyBulletPool.Enqueue(factory.createBullet(1));
        }

        enemyBulletCount = enemyBulletPool.Count;
    }

    public GameObject GetPlayerBullet(Vector2 position)
    {
        

        GameObject bullet = null;
        if (playerBulletPool.Count < 1)
           {
               playerBulletPool.Enqueue(factory.createBullet(0));
           }
           bullet = playerBulletPool.Dequeue();
           playerBulletCount = playerBulletPool.Count;
           playerActiveBullets++;

        bullet.SetActive(true);
        bullet.transform.position = position;
        return bullet;
    }

    public GameObject GetEnemyBullet(Vector2 position)
    {


        GameObject bullet = null;
        if (enemyBulletPool.Count < 1)
        {
            enemyBulletPool.Enqueue(factory.createBullet(1));
        }
        bullet = enemyBulletPool.Dequeue();
        enemyBulletCount = enemyBulletPool.Count;
        enemyActiveBullets++;

        bullet.SetActive(true);
        bullet.transform.position = position;
        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        switch (type)
        {
            case BulletType.PLAYER:
                {
                    playerBulletPool.Enqueue(bullet);
                    playerBulletCount = playerBulletPool.Count;
                    playerActiveBullets--;

                }
                break;
            case BulletType.ENEMY:
                {
                    enemyBulletPool.Enqueue(bullet);
                    enemyBulletCount = playerBulletPool.Count;
                    enemyActiveBullets--;
                }
                break;
        }
        
    }
}
