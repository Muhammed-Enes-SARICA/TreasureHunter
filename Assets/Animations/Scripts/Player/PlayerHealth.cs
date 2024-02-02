using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float maxHealt;
    public float currentHealt;


    bool isInmune;
    public float immunityTime;

    Animator anim;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }

    }

    //imageler
    public Image healtBar;

    void Start()
    {   
        currentHealt = maxHealt;
        maxHealt = PlayerPrefs.GetFloat("MaxHealth", maxHealt);
        currentHealt = PlayerPrefs.GetFloat("CurrentHealth", currentHealt);
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (currentHealt > maxHealt)
        {
            currentHealt = maxHealt;
        }

        healtBar.fillAmount = currentHealt / maxHealt;
    }

    IEnumerator Immunity()
    {
        isInmune = true;
        yield return new WaitForSeconds(immunityTime);
        isInmune = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&&!isInmune)
        {
            currentHealt -= collision.GetComponent<EnemyStats>().damage;
            StartCoroutine(Immunity());
            anim.SetTrigger("Hit");

            if (currentHealt <= 0)
            {
                currentHealt = 0;
                Destroy(gameObject);
                SceneManager.LoadScene(6);
            }
        }

        if (collision.CompareTag("TriggerEnemyWeapon")&&isInmune)
        {
            currentHealt -= collision.GetComponent<EnemyStats>().damage;
            StartCoroutine(Immunity());
            anim.SetTrigger("Hit");
            if (currentHealt <= 0)
            {
                currentHealt = 0;
                Destroy(gameObject);
                SceneManager.LoadScene(6);
            }
        }
        
    }


    public void takeDamage(float damage)
    {
        currentHealt -= damage;
        if (currentHealt <= 0)
        {
            currentHealt = 0;
            Destroy(gameObject);
            SceneManager.LoadScene(6);
        }
    }
}
