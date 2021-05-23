using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComprarObjetos : MonoBehaviour
{
    public float calidad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (StatsPlayer.jugador.dinero >= (int)calidad)
            {
                StatsPlayer.jugador.dinero -= (int)calidad;
                Puntuaciones.DineroGastado += (int)calidad;
                gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        GetComponent<TextMeshPro>().text = (int)calidad + "";
    }
}
