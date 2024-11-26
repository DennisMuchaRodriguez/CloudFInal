using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _comprigidbody2D;
    private SpriteRenderer _compspriteRenderer;
    public float speedX;
    public float JumpingPower;
    public int maxJumps = 2;
    private int jumpsRemaining;
    public float raycastDistance = 1f;
     public BarraDeVida LifeController;
    private bool isCollidingEnemy = false;
    public Contador contador;
    public float _xMovement;
  
    public bool _jump;
    public LayerMask collisionMask;
    public int Colore;
   


    private void Awake()
    {
        _comprigidbody2D = GetComponent<Rigidbody2D>();
        _compspriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps; 
    }

    void Update()
    {

      
        
        _comprigidbody2D.velocity = new Vector2(_xMovement * speedX, _comprigidbody2D.velocity.y);


        if (Colore == 1)
        {
            _compspriteRenderer.color = Color.green;
        }
        else if (Colore == 2)
        {
            _compspriteRenderer.color = Color.red;
        }
        else if (Colore == 3)
        {
            _compspriteRenderer.color = Color.blue;
        }
        else if (Colore == 4)
        {
            _compspriteRenderer.color = Color.yellow;
        }
        else if (Colore == 5)
        {
            _compspriteRenderer.color = Color.cyan;
        }




        LifeController.UpdateLife();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Portal")
        {
            contador.CambiarEscenaYouwin();
        }
        if (other.gameObject.tag == "Portal1")
        {
            contador.CambiarEscenaYouwin2();
        }
    }
    public void SetIsColEnemy(bool value)
    {
        isCollidingEnemy = value;
    }
    public bool GetIsCollidingWithEnemy()
    {
        return isCollidingEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        _xMovement = context.ReadValue<float>();
       
    }
    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed && jumpsRemaining > 0)
        {

            _comprigidbody2D.velocity = new Vector2(_comprigidbody2D.velocity.x, JumpingPower);
            jumpsRemaining = jumpsRemaining - 1;


            if (jumpsRemaining == 0)
            {
                _comprigidbody2D.velocity = new Vector2(_comprigidbody2D.velocity.x, JumpingPower);
            }
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, collisionMask);
        if (hit.collider != null)
        { 
            jumpsRemaining = maxJumps;
        }
    }
    public void CambioColor(InputAction.CallbackContext context)
    {
        if (context.performed && Colore < 5)
        {
            Colore = Colore + 1;
        }

    }
    public void CambioColorNeg(InputAction.CallbackContext context)
    {
        if (context.performed && Colore > 0)
        {
            Colore = Colore - 1;
        }

    }
}

