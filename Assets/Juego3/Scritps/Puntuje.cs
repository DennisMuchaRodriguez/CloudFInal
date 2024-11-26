using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Puntuje : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    void Start()
    {

        Coin.OnCoinTouched += IncreaseScore;
        UpdateScoreText();
    }

    void OnDestroy()
    {

        Coin.OnCoinTouched -= IncreaseScore;
    }

    void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

    }
}
