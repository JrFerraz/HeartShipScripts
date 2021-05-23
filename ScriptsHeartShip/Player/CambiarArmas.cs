using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarArmas : MonoBehaviour
{
    void Update()
    {
        cambiarArma();
        cambiarDatosArma();
    }

    void cambiarArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) StatsPlayer.jugador.munEscogida = "LaserShot";
        else if (Input.GetKeyDown(KeyCode.Alpha2)) StatsPlayer.jugador.munEscogida = "ImpactShot";
        else if (Input.GetKeyDown(KeyCode.Alpha3)) StatsPlayer.jugador.munEscogida = "Laser";
    }

    void cambiarDatosArma()
    {
        if (StatsPlayer.jugador.munEscogida.Equals("LaserShot"))
        {
            StatsPlayer.jugador.cantidadMunEscogida = StatsPlayer.jugador.laserAmmo;
            StatsPlayer.jugador.calentamientoEscogido = StatsPlayer.jugador.miLaserNormal.calentamiento;
            StatsPlayer.jugador.coolDownEscogido = StatsPlayer.jugador.miLaserNormal.cooldown;
            StatsPlayer.jugador.dañoEscogido = StatsPlayer.jugador.miLaserNormal.dmg;
            StatsPlayer.jugador.impulsoEscogido = StatsPlayer.jugador.miLaserNormal.speed;
        }
        else if (StatsPlayer.jugador.munEscogida.Equals("ImpactShot"))
        {
            StatsPlayer.jugador.cantidadMunEscogida = StatsPlayer.jugador.munImpacto;
            StatsPlayer.jugador.calentamientoEscogido = StatsPlayer.jugador.miImpactShot.warm_munImpacto;
            StatsPlayer.jugador.coolDownEscogido = StatsPlayer.jugador.miImpactShot.cooldown_munImpacto;
            StatsPlayer.jugador.dañoEscogido = StatsPlayer.jugador.miImpactShot.daño_munImpacto;
            StatsPlayer.jugador.impulsoEscogido = StatsPlayer.jugador.miImpactShot.impulso_munImpacto;
        }
        else if (StatsPlayer.jugador.munEscogida.Equals("Laser"))
        {
            StatsPlayer.jugador.cantidadMunEscogida = StatsPlayer.jugador.mun_LASER;
            StatsPlayer.jugador.calentamientoEscogido = StatsPlayer.jugador.miLASER.warm_LASER;
            StatsPlayer.jugador.dañoEscogido = StatsPlayer.jugador.miLASER.daño_LASER;
        }
    }
}
