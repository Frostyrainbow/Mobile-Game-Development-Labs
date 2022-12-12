using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour, Action
{
    [Header("Ranged Attack Properties")]
    [Range(1, 100)]
    public int fireDelay = 30;
    public Transform bulletSpawn;

    public GameObject bulletPrefab;
    public Transform bulletParent;

    private bool hasLOS;
    private PlayerDection playerDection;
    private SoundManager soundManager;

    private void Awake()
    {
        playerDection = transform.parent.GetComponentInChildren<PlayerDection>();
        soundManager = FindObjectOfType<SoundManager>();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        hasLOS = playerDection.LOS;
    }

    void FixedUpdate()
    {
        if (hasLOS && Time.frameCount % fireDelay == 0)
        {
            Execute();
        }
    }

    public void Execute()
    {
        var bullet = BulletManager.Instance().GetBullet(bulletSpawn.position);
        
    }
}
