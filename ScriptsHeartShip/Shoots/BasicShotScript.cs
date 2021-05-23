using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShotScript : MonoBehaviour
{
    public GameObject impact;

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        //Random.ColorHSV()
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
            Instantiate(impact, transform.position, Quaternion.identity);
        }
        
    }

    void Update()
    {
        //Se elimina la bala de la escena en 4s
        Destroy(gameObject, StatsPlayer.jugador.vuelo_mun1);
    }
}
