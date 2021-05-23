using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emboscada : MonoBehaviour
{
    public GameObject marcianito;
    Transform [] posicionesEmboscada;


    private bool haEntrado = false;
    private void Awake()
    {
        posicionesEmboscada = gameObject.GetComponentsInChildren<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !haEntrado)
        {
            //Invoke("generarMarcianos", 1f);
            generarMarcianos();
            haEntrado = true;
        }
    }

    void generarMarcianos()
    {
        for (int i = 1; i < posicionesEmboscada.Length; i++)
        {
            Instantiate(marcianito, posicionesEmboscada[i].position, Quaternion.identity);
        }
    }
}
