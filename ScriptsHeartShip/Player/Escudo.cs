using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{

    private float reloadTime = 0;

    void Update()
    {
        if (StatsPlayer.jugador.hit)
        {
            reloadTime = Time.realtimeSinceStartup + 5;
            StatsPlayer.jugador.hit = false;
        }

        if (reloadTime != 0 && reloadTime <= Time.realtimeSinceStartup && StatsPlayer.jugador.escudo <= StatsPlayer.jugador.escudoMax)
        {
            StatsPlayer.jugador.escudo += 1f * Time.deltaTime;
        }
        
    }
}
