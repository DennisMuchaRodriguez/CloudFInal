using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerJuego3 : MonoBehaviour
{
    public void CambiarEscena(string nombreDeEscena)
    {

        SceneManager.LoadScene(nombreDeEscena);
    }


    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
