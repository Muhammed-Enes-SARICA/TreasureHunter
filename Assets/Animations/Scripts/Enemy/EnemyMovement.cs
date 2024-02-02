using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sp;

    //yürüme ve yön deðiþtirme algýlama
    public bool isStatic;
    public bool isWalker;
    
    public bool isWalkingRight;

    //Hareket Parametreleri
    public Transform WallCheck, GroundCheck, GapCheck;
    public bool wallDetected, groundDetected, gapDetected;
    public float detectionRadius;
    public LayerMask whatIsGround;

    //devriye atan enemy objelerinin kýsýtlanacaðý A ile B noktalarý arasý geçiþi
    public bool isPatroller;
    public bool wait;
    public float waitTime;
    bool isWaiting;
    public Transform pointA, pointB;
    private bool moveToA, moveToB;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        moveToA = true;

    }

    // Update is called once per frame
    void Update()
    {
        gapDetected = !Physics2D.OverlapCircle(GapCheck.position, detectionRadius, whatIsGround);
        wallDetected = Physics2D.OverlapCircle(WallCheck.position, detectionRadius, whatIsGround);
        groundDetected = Physics2D.OverlapCircle(GroundCheck.position, detectionRadius, whatIsGround);


        if((gapDetected || wallDetected) && groundDetected)
        {
                     Flip();
         
        }
    }
    private void FixedUpdate()
    {
        if (isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (isWalker)
        {
            anim.SetBool("Idle", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!isWalkingRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                
            } 
            else
                {
               
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                }
           
        }
        //A ve B noktlarýna göre enemy hareketi tanýmlar. devriye olayý
        if (isPatroller)
        {
            anim.SetBool("Idle", false);

            if (moveToA)
            {
                if (!isWaiting)
                {
                    rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Idle", false);
                }
                if (Vector2.Distance(transform.position, pointA.position)<0.2f)
                {
                    if (wait)
                    {
                        StartCoroutine(Waiting());
                    }
                    Flip();
                    moveToA = false;
                    moveToB = true;

                }
            }
            if (moveToB)
            {
                if (!isWaiting)
                {
                    rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Idle", false);
                }
                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {
                    if (wait)
                    {
                        StartCoroutine(Waiting());
                    }
                    Flip();
                    moveToA = true;
                    moveToB = false;

                }
            }
        }
    }

    IEnumerator Waiting()
    {
        anim.SetBool("Idle", true);
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        anim.SetBool("Idle", false);
        Flip();
    }
    public void Flip()
    {
        isWalkingRight = !isWalkingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
