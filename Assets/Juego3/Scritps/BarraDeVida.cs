using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarraDeVida : MonoBehaviour
{
    public int Life;
    public GameObject[] Lifes;
    public Contador contador;

    void Start()
    {
        ActivateLives();
        Corazon.OnHealthTouched += HandleHealthTouched;
    }

    void HandleHealthTouched()
    {

        Life++;
        UpdateLife();
    }

    public void UpdateLife()
    {
        for (int i = 0; i < Lifes.Length; i++)
        {
            Lifes[i].SetActive(i < Life);
        }

        if (Life < 1)
        {
            contador.CambiarEscenaGameOver();
        }
    }

    void ActivateLives()
    {
        for (int i = 0; i < Lifes.Length; i++)
        {
            Lifes[i].SetActive(true);
        }
    }

    void OnDestroy()
    {
        Corazon.OnHealthTouched -= HandleHealthTouched;
    }
}
