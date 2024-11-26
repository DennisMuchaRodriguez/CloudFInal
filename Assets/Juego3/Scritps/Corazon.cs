using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
    public delegate void HealthTouchedEvent();
    public static event HealthTouchedEvent OnHealthTouched;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OnHealthTouched != null)
                OnHealthTouched();

            Destroy(this.gameObject);
        }

    }
}
