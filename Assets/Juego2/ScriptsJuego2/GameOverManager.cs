using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverManager : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "Final Score: " + PlayerPrefs.GetInt("FinalScore", 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Juego2"); // Reiniciar el juego
    }

    public void QuitGame()
    {
        Application.Quit(); // Salir del juego
    }
}
