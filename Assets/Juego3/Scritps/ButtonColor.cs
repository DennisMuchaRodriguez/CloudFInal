using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColor : MonoBehaviour
{
    public Player personaje;
    public int ColorName;
    
    public void CambioColor()
    {
        if (personaje.GetIsCollidingWithEnemy()==false)
        {
            personaje.Colore = ColorName;
        }
        
    }

}
