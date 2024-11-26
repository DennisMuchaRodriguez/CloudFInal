using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public delegate void CoinTouchedEvent();
    public static event CoinTouchedEvent OnCoinTouched;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OnCoinTouched != null)
            {
                OnCoinTouched();
            }

            Destroy(gameObject);

        }

    }
}
