using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float elapsedTime = 0f;
    private bool isCounting = true;



    void Update()
    {
        
        elapsedTime += Time.deltaTime;

      
        ActualizarTextoContador();
    }

    void ActualizarTextoContador()
    {
       
        timerText.text = "Tiempo: " + ((int)elapsedTime).ToString() + "s";
    }
    public void ToggleCounting(bool isPaused)
    {
        isCounting = !isPaused;
    }

    public void CambiarEscenaGameOver()
    {
        PlayerPrefs.SetFloat("Tiempo", elapsedTime);
        SceneManager.LoadScene("GameOver");
    }
    public void CambiarEscenaYouwin()
    {

        PlayerPrefs.SetFloat("Tiempo", elapsedTime);
        SceneManager.LoadScene("YouWin");
    }
    public void CambiarEscenaYouwin2()
    {

        PlayerPrefs.SetFloat("Tiempo", elapsedTime);
        SceneManager.LoadScene("Finish");
    }
}
