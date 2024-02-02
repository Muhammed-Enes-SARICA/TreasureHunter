using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    private bool inRange;
    private Animator anim;

    private EnemyAI enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemy.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy.TriggerZone.SetActive(true);
            enemy.inRange=false;
            enemy.SelectTarget();
        }
    }
}
