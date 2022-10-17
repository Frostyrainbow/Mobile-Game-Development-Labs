using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Type", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public string bulletName;
    public Sprite bulletSprite;
    public BulletType type;
    public BulletDirection direction;
    public float bulletSpeed;
    public float maxXBounds;
    public float minXBounds;
    public float maxYBounds;
    public float minYBounds;

}
