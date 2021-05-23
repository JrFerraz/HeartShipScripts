using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAlien : MonoBehaviour
{
    public GameObject marcianito;
    public GameObject tp;
    public GameObject marcianoExplosivo;
    public GameObject player;
    Vector3 posicionSpawn;

    float speedSpawn = 4f;
    float requisitoLevel = 3;
    bool nextLevel = false;
    void Start()
    {
        
    }

    public void activarSpawn()
    {
        InvokeRepeating("marcianitosSpawn", 1f, speedSpawn);
    }

    void Update()
    {
        if (speedSpawn < 0.5)
        {
            speedSpawn = 0.5f;
        }
        if (StatsPlayer.jugador.numKills == requisitoLevel && !nextLevel)
        {
            speedSpawn -= 0.4f;
            CancelInvoke();
            InvokeRepeating("marcianitosSpawn",0f,speedSpawn);
            nextLevel = true;
        }
        if (StatsPlayer.jugador.numKills > requisitoLevel)
        {
            nextLevel = false;
            requisitoLevel += 4;
        }
        if (!StatsPlayer.jugador.vivo)
        {
            CancelInvoke();
        }
    }
    public void marcianitosSpawn()
    {
        int drop = Random.Range(0,101);
        if (drop <= 10)
        {
            posicionSpawn = new Vector3(Random.Range(player.transform.position.x - 10, player.transform.position.x + 10), Random.Range(player.transform.position.y - 10, player.transform.position.y + 10), 0);
            Instantiate(marcianoExplosivo, posicionSpawn, Quaternion.identity);
        }
        else if (drop <= 98)
        {
            posicionSpawn = new Vector3(Random.Range(player.transform.position.x - 10, player.transform.position.x + 10), Random.Range(player.transform.position.y - 10, player.transform.position.y + 10), 0);
            Instantiate(marcianito, posicionSpawn, Quaternion.identity);
        }
        else
        {
            posicionSpawn = new Vector3(Random.Range(player.transform.position.x - 10, player.transform.position.x + 10), Random.Range(player.transform.position.y - 10, player.transform.position.y + 10), 0);
            Instantiate(tp, posicionSpawn, Quaternion.identity);
        }
        
    }

    public void pararSpawn()
    {
        CancelInvoke("marcianitosSpawn");
    }
}
