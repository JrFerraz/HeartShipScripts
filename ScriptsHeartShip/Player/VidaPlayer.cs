using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public Image vida;
    public Image armadura;
    public Image escudo;
    public GameObject barraEscudo;
    public GameObject barraArmadura;

    private void Start()
    {
        interfazJugador();
    }
    void Update()
    {
        interVida();
        interEscudo();
        interArmadura();
    }

    void interfazJugador()
    {
        if (StatsPlayer.jugador.escudo == 0 && StatsPlayer.jugador.armadura > 0)
        {
            barraEscudo.gameObject.SetActive(false);
        }
        else if (StatsPlayer.jugador.escudo > 0 && StatsPlayer.jugador.armadura == 0)
        {
            barraEscudo.gameObject.transform.position = barraArmadura.gameObject.transform.position;
            barraArmadura.gameObject.SetActive(false);
        }
        else if (StatsPlayer.jugador.escudo == 0 && StatsPlayer.jugador.armadura == 0)
        {
            barraEscudo.gameObject.SetActive(false);
            barraArmadura.gameObject.SetActive(false);
        }
    }

    void interArmadura()
    {
        if (StatsPlayer.jugador != null && StatsPlayer.jugador.vivo)
        {
            armadura.fillAmount = StatsPlayer.jugador.armadura / StatsPlayer.jugador.armaduraMax;
        }
    }
    void interVida()
    {
        if (StatsPlayer.jugador != null && StatsPlayer.jugador.vivo)
        {
            vida.fillAmount = StatsPlayer.jugador.vida / StatsPlayer.jugador.vidaMax;
        }
        else
        {
            vida.fillAmount = 0;
        }
    }

    void interEscudo()
    {
        if (StatsPlayer.jugador != null)
        {
            escudo.fillAmount = StatsPlayer.jugador.escudo / StatsPlayer.jugador.escudoMax;
        }
        else
        {
            escudo.fillAmount = 0;
        }
    }
}
