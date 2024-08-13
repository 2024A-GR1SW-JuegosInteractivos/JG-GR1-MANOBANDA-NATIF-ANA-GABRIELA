using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal, isMilk;

    private bool isCollected;

    public GameObject pickupEffect;
    
    public Text messageText; // Referencia al Text UI para mostrar mensajes.

    
    private string[] environmentalMessages = new string[] // Mensajes educativos
    {
        "Recuerda: ¡El agua limpia es esencial para la vida en nuestro planeta!",
        "¡Cada árbol que plantamos ayuda a combatir el cambio climático!",
        "Las energías renovables son la clave para un futuro sostenible.",
        "¡Reciclar es una forma fácil de cuidar nuestro medio ambiente!",
        "¡La biodiversidad es fundamental para la salud del planeta!"
    };
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;

                UIController.instance.UpdateGemCount();

                Instantiate(pickupEffect, transform.position, transform.rotation);
                
                AudioManager.instance.PlaySFX(6);
                
                
                isCollected = true;
                Destroy(gameObject);
                
                ShowEnvironmentalMessage(); // Mostrar mensaje educativo

            }

            if (isHeal)
            {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();

                    AudioManager.instance.PlaySFX(7);

                    isCollected = true;
                    Destroy(gameObject);
                }
            }
            
            if (isMilk)
            {
                LevelManager.instance.milksCollected++;

                UIController.instance.UpdateMilkCount();
                
                Instantiate(pickupEffect, transform.position, transform.rotation);

                AudioManager.instance.PlaySFX(6);
                

                
                isCollected = true;
                Destroy(gameObject);
                
                ShowEnvironmentalMessage(); // Mostrar mensaje educativo

            }
        }
    }
    
    
    private void ShowEnvironmentalMessage()
    {
        string message = environmentalMessages[UnityEngine.Random.Range(0, environmentalMessages.Length)];
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        // Aparece durante 10 segundos y luego desaparece gradualmente
        LeanTween.alphaText(messageText.rectTransform, 0, 7.0f).setOnComplete(() => {
            messageText.gameObject.SetActive(false);
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1); // Resetea la transparencia
        });
    }

    /*
    private void ShowEnvironmentalMessage()
    {
        Debug.Log("Intentando mostrar un mensaje educativo"); // Verifica si este mensaje se registra
        string message = environmentalMessages[UnityEngine.Random.Range(0, environmentalMessages.Length)];
        messageText.text = message;
        StartCoroutine(DisplayMessage());
    }

    private IEnumerator DisplayMessage()
    {
        Debug.Log("Mostrando mensaje en pantalla"); // Verifica que el mensaje está visible
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Espera 3 segundos
        messageText.gameObject.SetActive(false);
        Debug.Log("Mensaje ocultado después de 3 segundos"); // Verifica que el mensaje se ocultó
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
