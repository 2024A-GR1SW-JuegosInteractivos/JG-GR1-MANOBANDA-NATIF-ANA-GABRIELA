using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //Movimiento
    public float moveSpeed;

    //Salto 
    public float jumpForce;
    private bool canDoubleJump;
    public float bounceForce;

    //Componentes
    public Rigidbody2D theRB;

    // Animaciones
    public Animator anim;
    private SpriteRenderer theSR;
    

    //Detectar el piso
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;
    private bool isGrounded;

    public bool stopInput;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }
    
    // Tiempo 
    public float knockBackLength, knockBackForce;
    private float knockBackCounter;
    
    

    // Update is called once per frame   
    void Update()
    {
        if(!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
                    {
                        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
            
                        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround);
            
                        if (isGrounded)
                        {
                            canDoubleJump = true;
                        }
            
                        if (Input.GetButtonDown("Jump"))  
                        {
                            if (isGrounded)
                            {
                                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                                AudioManager.instance.PlaySFX(10);
                            }else
                            {
                                if (canDoubleJump)
                                {
                                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                                    //AudioManager.instance.PlaySFX(10);
                                    canDoubleJump = false;
                                }
                            }
                        }
            
                        if (theRB.velocity.x < 0)
                        {
                            theSR.flipX = true;
                        } else if (theRB.velocity.x > 0)
                        {
                            theSR.flipX = false;
                        }
                    }
                    else
                    {
                        knockBackCounter -= Time.deltaTime;
                        if (!theSR.flipX)
                        {
                            theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                        }
                        else
                        {
                            theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            
                        }
                    }

        }
        
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }


    public void Knockback()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }
}
