using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStats : MonoBehaviour
{
    
    float damagePower;
    float totalDamagePower;
    public float weaponDamage;

    public GameObject damageText;
    PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
        damagePower = player.damage;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float DamageInput()
    {
        totalDamagePower = damagePower + weaponDamage;
        float finalAttack = Mathf.Round(Random.Range(totalDamagePower - 7, totalDamagePower + 5));

        GameObject textDam=Instantiate(damageText,new Vector2(transform.position.x+1, transform.position.y+1),Quaternion.identity);
        textDam.GetComponent<TextMeshPro>().SetText(finalAttack.ToString());

        if (finalAttack > totalDamagePower+1)
        {
            textDam.GetComponent<TextMeshPro>().SetText("CRITICAL!\n" + finalAttack.ToString());
            finalAttack *= 2;
        }
        return finalAttack;
    }
}
