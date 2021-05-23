using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAtracction : MonoBehaviour
{
    private GameObject player;
    float parada = 0f;
    float speed = 0f;
    float tiempoParaIman;

    bool atraccionParcialB;
    private void Awake()
    {
        parada = transform.position.y;
        try
        {
            player = GameObject.Find("Player");
        }
        catch (System.Exception e) { }
    }

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, 300f) * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        if (atraccionParcialB) atraccionParcial();
        GenerateItems.quitarGravedad(gameObject, parada);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            atraccionParcialB = true;
        }
    }
    void atraccionParcial()
    {
        speed += 0.2f * Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, player.transform.position, speed);
    }

    void atraccionDefinitiva()
    {
        Vector2.MoveTowards(transform.position, player.transform.position,10);
    }
}
