using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; 
    
    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    public GameObject deathEffect;
    
    private void Awake()
    {
        instance = this;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; // Activar conteo y llegue a cero.

            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }

    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--; // disminuir vidas
            PlayerController.instance.anim.SetTrigger("Hurt");

            AudioManager.instance.PlaySFX(9);
            
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
                
                AudioManager.instance.PlaySFX(8);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                
                
                
                PlayerController.instance.Knockback();
            }

            UIController.instance.UpdateHealthDisplay(); 
        }
        
    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();
    }
}
