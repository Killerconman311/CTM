using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreKeep : MonoBehaviour
{
    public int score = 0;
    public Text scoreText; 

    public void IncreaseScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreText();
    }
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
