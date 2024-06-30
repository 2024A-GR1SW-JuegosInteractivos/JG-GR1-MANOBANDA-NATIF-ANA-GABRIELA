using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarritoScript : MonoBehaviour
{
    [SerializeField] float steerSpeed  = 200f;
    [SerializeField] float moveSpeed  = 15f;

    [SerializeField] float velocidadRapido  = 20f;
    [SerializeField] float velocidadLento  = 5f;


    // Start is called before the first frame update

    void Start()
    {
        // cambiar la rotación
        //transform.Rotate(0,0,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        // cantidad de velocidad de giro & 
        // cantidad de velocidad de movimiento
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;


        // cambiar la posición
        transform.Rotate(0, 0 , -1*steerAmount);
        transform.Translate(0, moveAmount ,0);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rapido")
        {
            Debug.Log("Ir Rápido");
            moveSpeed = velocidadRapido;
        } 
        if (other.tag == "Lento")
        {
            Debug.Log("Ir Lento");
            moveSpeed = velocidadLento;
        }
    }
}
