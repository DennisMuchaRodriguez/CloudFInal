using UnityEngine;
using TMPro;

public class MostrarTiempo : MonoBehaviour
{
    public TextMeshProUGUI tiempoText;


    void Start()
    {
        MostrarTiempoG();
    }

    void MostrarTiempoG()
    {
        float tiempoGuardado = PlayerPrefs.GetFloat("Tiempo");

        if (tiempoGuardado != 0f)
        {
            int tiempoEntero = (int)tiempoGuardado;
            tiempoText.text = "Tiempo Guardado: " + tiempoEntero.ToString() + "s";
        }
        else
        {
            tiempoText.text = "No hay tiempo guardado.";
        }
    }
}
