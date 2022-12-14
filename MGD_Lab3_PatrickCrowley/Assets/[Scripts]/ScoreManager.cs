using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreLabel;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreLabel();
    }

    public void UpdateScoreLabel()
    {
        scoreLabel.text = $"Score: {score}";
    }


}
