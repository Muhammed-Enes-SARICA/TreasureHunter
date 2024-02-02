using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private float movementDirection;
    public float speed;
    public float jumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack = 0;

    public AudioSource swordAs;


    private bool isGrounded;
    private bool isFacingRight = true;

    Rigidbody2D rb;
    Animator anim;

    WeaponStats weaponStats;

    public Transform attackPoint;
    public float attackDistance;
    public LayerMask enemyLayer;

    public GameObject GroundCheck;
    public LayerMask groundlayer;
    public float damage;

    public GameObject ninjaStar;
    public Transform firePoint;

    public GameObject inventory;
    bool inventoryIsActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        weaponStats = GetComponent<WeaponStats>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        Jump();
        ChechSurface();
        CheckAnimations();
        AttackInput();
        Shoot();

        if(Input.GetKeyDown(KeyCode.I)&& !inventoryIsActive)
        {
            inventory.SetActive(true);
            inventoryIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.I)&&inventoryIsActive)
        {
            inventory.SetActive(false);
            inventoryIsActive = false;
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (StarBank.instance.BankStar > 0) {
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                StarBank.instance.BankStar -= 1;

                PlayerPrefs.SetInt("StarAmount", StarBank.instance.BankStar);
            }
        }
            
    }
    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
        anim.SetFloat("runSpeed", Mathf.Abs(movementDirection * speed));
    }

    void CheckRotation()
    {
        if (isFacingRight && movementDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementDirection > 0)
        {
            Flip();
        } 
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    void ChechSurface()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.transform.position, groundCheckRadius, groundlayer);
    }


    void Jump()
    {
        if (isGrounded) { 
            if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        }
        
    }

    public void Attack()
    {
        anim.SetTrigger("AttackBasic");
        AudioManager.instance.PlayAudio(swordAs);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackDistance,enemyLayer);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(weaponStats.DamageInput());
        }
       
    }
    public void AttackInput()
    {
        if(Time.time> nextAttack) {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }

}
