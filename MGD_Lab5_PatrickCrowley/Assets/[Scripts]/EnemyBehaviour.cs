using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Boundery xBoundery;
    public Boundery yBoundery;
    public Boundery xSpeed;
    public Boundery ySpeed;
    public float horizontalSpeed;

    public Transform bulletSpawnPoint;
    // public GameObject bulletPrefab;
    public float ROF = 0.2f;
    //public Transform bulletPool;

    public BulletManager bManager;

    // Start is called before the first frame update
    void Start()
    {
        bManager = FindObjectOfType<BulletManager>();
        var randomXPosition = Random.Range(xBoundery.min, xBoundery.max);
        var randomYPosition = Random.Range(yBoundery.min, yBoundery.max);
        xSpeed.min = Random.Range(0.5f, 2.0f);
        xSpeed.max = Random.Range(2.0f, 6.0f);
        horizontalSpeed = Random.Range(xSpeed.min, xSpeed.max);
        transform.position = new Vector3(randomXPosition, randomYPosition, 0.0f);
        InvokeRepeating("FireBullets", 0.0f, ROF);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalLength = xBoundery.max - xBoundery.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLength) - xBoundery.max, transform.position.y, transform.position.z);
    }

    void FireBullets()
    {
        var bullet = bManager.GetEnemyBullet(bulletSpawnPoint.position);
    }


}
