using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public Player player;
    public BarraDeVida Controller;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           
                Controller.Life = Controller.Life - 10;
            
        }

    }
}
