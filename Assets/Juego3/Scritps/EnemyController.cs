using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Player player;
    public BarraDeVida Controller;
    public float startX; 
    public float endX;    
    public float startY; 
    public float endY;
    public float moveSpeedX;
    public float moveSpeedY;
    private Rigidbody2D rb2d; 

    public int TipeEnemy;

    private bool movingXToEnd = true; 
    private bool movingYToEnd = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float movementX = moveSpeedX * Time.deltaTime;
       
        float movementY = moveSpeedY * Time.deltaTime;

     
        rb2d.velocity = new Vector2(movementX, movementY);


        

        if (movingXToEnd)
        {
            rb2d.velocity = new Vector2(moveSpeedX, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeedX, rb2d.velocity.y);
        }

        if (transform.position.x > endX)
        {
            movingXToEnd = false;
        }
        else if (transform.position.x < startX)
        {
            movingXToEnd = true;
        }

        if (movingYToEnd)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,moveSpeedY);
        }
        else
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,-moveSpeedY);
        }

        if (transform.position.y > endY)
        {
            movingYToEnd = false;
        }
        else if (transform.position.y < startY)
        {
            movingYToEnd = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.SetIsColEnemy(true);
            if (player.Colore != TipeEnemy)
            {
                Controller.Life = Controller.Life - 1;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.SetIsColEnemy(false);
        }
    }
}
