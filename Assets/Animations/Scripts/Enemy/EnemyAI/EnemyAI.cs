using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform target;

    [SerializeField]
    private float distance;
    private bool attackMode;
    public bool inRange;
    private bool cooling;
    private float intTimer;


    public Transform leftBound, rightBound;


    private Animator anim;
    public GameObject actionZone, TriggerZone;
    
    
    
    
    
    void Start()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (inRange)
        {
          EnemyBehaviour();
        }
        
        if(InsideOfBounds() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") )
        {
            SelectTarget();
        }
    }

    public void SelectTarget()
    {
        float distanceToLeft  = Vector2.Distance(transform.position, leftBound.position);
        float distanceToRight =Vector2.Distance(transform.position, rightBound.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftBound;
            
        }
        else
        {
            target = rightBound;
            
        }
      
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x >target.position.x) 
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject.transform;
            inRange = true;
            Flip();
        }
    }*/

    public void StopAttack()
    {
        attackMode = false;
        anim.SetBool("Attack", false);
        cooling = false;
    }


    public void TriggerCooling()
    {
        cooling = true;
    }

    public void CoolDown()
    {
        timer -= Time.deltaTime;
        if(timer <=0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;

        }
    }

    public void EnemyBehaviour()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        
        
        if (distance > attackDistance)
        {
            //Move();
            StopAttack();
        }
        else if (distance <= attackDistance && !cooling)
        {
            Attack();
        }

        if(cooling)
        {
            anim.SetBool("Attack", false);
            CoolDown();
        }
    }

    private bool InsideOfBounds()
    {
        return transform.position.x > leftBound.position.x && transform.position.x < rightBound.position.x;
    
    }
    public void Move()
    {
        anim.SetBool("CanWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition,moveSpeed*Time.deltaTime);
        }
    }
    public void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
    }
}
