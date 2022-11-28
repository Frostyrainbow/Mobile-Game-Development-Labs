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
            var player = collision.gameObject.GetComponent<PlayerControler>();
            player.life.LoseALife();
            player.health.ResetHealth();

            if (player.life.value > 0)
            {
                Respawn(collision.gameObject);

                FindObjectOfType<SoundManager>().PlaySoundFX(SoundEffects.DEATH, Channel.PLAYER_DEATH_FX);
            }
            
        }
    }

    public void Respawn(GameObject gameObject)
    {
        gameObject.transform.position = playerSpawnPoint.position;
    }
}
