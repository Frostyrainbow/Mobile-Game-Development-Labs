using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeathPlane : MonoBehaviour
{
    public Transform playerSpawnPoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Respawn(collision.gameObject);
        }
    }

    public void Respawn(GameObject gameObject)
    {
        gameObject.transform.position = playerSpawnPoint.position;
    }
}
