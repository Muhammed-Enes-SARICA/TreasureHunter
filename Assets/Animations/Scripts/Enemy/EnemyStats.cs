using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio
    ;
public class EnemyStats : MonoBehaviour
{

    

    //float
    public float maxHealt;
    private float currentHealt;
    public float knockBackForceX, knockBackForceY;
    public float damage;
    public float expToGive;
    //bool
    public AudioSource hitAS, deadAS;



    //gameobject
    public Transform player;
    Rigidbody2D rb;
    public GameObject Death_effect;
    private Animator animator;
    public GameObject[] lootItems;

    public static EnemyStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealt = maxHealt;
        rb = GetComponent<Rigidbody2D>();
      

    }

    // Update is called once per frame
    void Update()
    {

    }
    //TakeDamage Fonksiyonu: Enemy Hasar alýðýnda, Current Health Playerin Hasarýna göre azalýr. Boar_Hit animasyonu çalýþýr. Ve hasar alýnan yönün zýttýna Enemy KnockBack iþlemini yapar.
    public void TakeDamage(float damage)
    {

        currentHealt -= damage;
        AudioManager.instance.PlayAudio(hitAS);
        animator.SetTrigger("hitEffect");
       
        //karakter Enemy'nin saðýndaysa sola, Solundaysa saða ötelenir.
        if(player.position.x < transform.position.x)
        {
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }

        if (currentHealt <= 0)
        {

            currentHealt = 0;
            Instantiate(Death_effect, transform.position, transform.rotation);

            int lootChance = Random.Range(0,101);

            if ( lootChance<=40)
            {
                Instantiate(lootItems[0],transform.position,Quaternion.identity);
            }
           else if (lootChance > 40 && lootChance <= 70)
            {
                Instantiate(lootItems[1], transform.position, Quaternion.identity);
            }
            else if (lootChance >70 && lootChance <= 90)
            {
                Instantiate(lootItems[2], transform.position, Quaternion.identity);
            }
            else if (lootChance > 90 && lootChance < 101)
            {
                Instantiate(lootItems[3], transform.position, Quaternion.identity);
            }



            Destroy(gameObject);
            Experience.instance.expMod(expToGive);
            AudioManager.instance.PlayAudio(deadAS);
        }
    }
}
