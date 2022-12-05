using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Lives : MonoBehaviour
{
    [Header("Life Properties")]
    public int value;

    public GameObject[] lives;

    // Start is called before the first frame update
    void Start()
    {
        ResetLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLives()
    {
        value = 3;
        foreach  (GameObject life in lives)
        {
            life.SetActive(true);
        }
    }

    public void LoseALife()
    {
        value -= 1;
        if (value < 0)
        {
            value = 0;
        }
        lives[value].SetActive(false);
    }

    public void GainALife()
    {
        value += 1;
        lives[value].SetActive(true);
    }
}
