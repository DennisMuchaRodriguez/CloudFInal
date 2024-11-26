using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento hacia adelante y atrás
        movement = transform.up * Input.GetAxisRaw("Vertical") * moveSpeed;

        // Girar hacia el mouse
        RotateTowardsMouse();

        // Disparar
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Mover al jugador hacia adelante y atrás
        rb.MovePosition(rb.position + new Vector2(movement.x, movement.y) * Time.fixedDeltaTime);
    }

    void RotateTowardsMouse()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        // Calcular el ángulo hacia el mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
