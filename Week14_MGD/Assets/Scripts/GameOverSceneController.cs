using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().PlayMusic(SoundEffects.GAMEOVER);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
