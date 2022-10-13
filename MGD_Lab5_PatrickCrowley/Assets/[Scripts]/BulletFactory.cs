using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{

    //Bullet Prefab
    public GameObject bulletPrefab;

    //Sprite Textures
    public Sprite playerBulletSprite;
    public Sprite enemyBulletSprite;

    //Bullet Parent
    public Transform bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    private void Initalize()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprite Assets/Bullet");
        enemyBulletSprite = Resources.Load<Sprite>("Sprite Assets/EnemySmallBullet");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Player_Bullet");
        bulletParent = GameObject.Find("BulletPool").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject createBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);

        switch (type)
        {
            case BulletType.PLAYER:
                bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
                bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                bullet.GetComponent<BulletBehavior>().SetDirection(BulletDirection.UP);
                break;


            case BulletType.ENEMY:
                bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
                bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                bullet.GetComponent<BulletBehavior>().SetDirection(BulletDirection.DOWN);
                bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
        }

        bullet.SetActive(true);
        return bullet;

    }
}
