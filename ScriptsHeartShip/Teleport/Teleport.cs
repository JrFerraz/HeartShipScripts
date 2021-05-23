using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform salida;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            controlShaders.ordenarDesaparicion = true;
            Invoke("tepear", 1f);
           
        }
    }

    void tepear()
    {
        FindObjectOfType<audioManager>().Play("Aparecer");
        GameObject.FindGameObjectWithTag("Player").transform.position = salida.position;
    }
}
