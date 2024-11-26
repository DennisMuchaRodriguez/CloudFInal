using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            // Movimiento hacia el jugador
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Rotación hacia el jugador
            RotateTowardsPlayer();
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Obtener el ángulo entre enemigo y jugador
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Aplicar la rotación en el eje Z
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManagerJuego2.instance.GameOver();
            Destroy(collision.gameObject); // Destruir jugador
        }
    }
}
