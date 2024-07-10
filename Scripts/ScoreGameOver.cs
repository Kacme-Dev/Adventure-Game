using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGameOver : MonoBehaviour
{
    int totalScore;
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        scoreText.text = totalScore.ToString();
    }
}
