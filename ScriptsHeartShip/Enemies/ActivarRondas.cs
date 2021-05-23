using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarRondas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GetComponent<SpawnAlien>().activarSpawn();
            Debug.Log("Entro");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Salgo");
            GetComponent<SpawnAlien>().pararSpawn();
        }
    }
}
