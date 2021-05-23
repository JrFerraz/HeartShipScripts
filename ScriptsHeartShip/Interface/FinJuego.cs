using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinJuego : MonoBehaviour
{
    public TextMeshProUGUI PuntosText;
    public TextMeshProUGUI RachaText;
    public TextMeshProUGUI DineroText;
    public TextMeshProUGUI TotalText;


    private void Start()
    {
        int total = StatsPlayer.jugador.numKills + StatsPlayer.jugador.rachaMax * 2;
        PuntosText.text = "Puntos: " + StatsPlayer.jugador.numKills; 
        RachaText.text = "Mayor Racha: " + StatsPlayer.jugador.rachaMax;
        DineroText.text = "DineroMax: " + Puntuaciones.DineroMaximo;
        TotalText.text = "Total Puntos: " + Puntuaciones.total;
    }
}
