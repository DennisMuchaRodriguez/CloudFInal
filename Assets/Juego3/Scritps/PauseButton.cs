using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI pauseButtonText; 
    public GameObject[] objectsToPause;
    public Contador Contador;

    private bool isPaused = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.V))
        {
           
            TogglePause();
        }
    }

    public void TogglePause()
    {
        
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

      
        for (int i = 0; i < objectsToPause.Length; i++)
        {
            objectsToPause[i].SetActive(!isPaused);
        }

        Contador.ToggleCounting(isPaused);
    }
}
