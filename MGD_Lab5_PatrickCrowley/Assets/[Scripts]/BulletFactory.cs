using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{

    //Bullet Prefab
    public Bullet[] typesOfBullets;
    public GameObject bulletPrefab;

    //Bullet Parent
    public Transform bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    private void Initalize()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("BulletPool").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject createBullet(int arrayValue)
    {
        GameObject createdBullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        createdBullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        createdBullet.GetComponent<BulletBehavior>().bulletObject = typesOfBullets[arrayValue];
        createdBullet.GetComponent<SpriteRenderer>().sprite = typesOfBullets[arrayValue].bulletSprite;
        createdBullet.SetActive(false);
        return createdBullet;

    }
}
