using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public GameObject onScreenControls;

    // Start is called before the first frame update
    void Awake()
    {
        onScreenControls = GameObject.Find("OnScreenControls");

        onScreenControls.SetActive(Application.isMobilePlatform);
        
    }

    private void Start()
    {
        FindObjectOfType<SoundManager>().PlayMusic(SoundEffects.MUSIC);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    FindObjectOfType<HealthBarController>().TakeDamage(10);
        //}
    }


}
