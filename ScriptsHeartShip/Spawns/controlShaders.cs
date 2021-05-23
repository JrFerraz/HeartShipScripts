using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlShaders : MonoBehaviour
{

    private GameObject player;
    private float intensidadAparicion;

    public static bool aparecido = false;
    public static bool ordenarDesaparicion = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        intensidadAparicion = 0;
        //Puntuaciones.limpiarPuntuaciones();
    }

    void Update()
    {
        aparecer();
        desaparecer();
    }

    void aparecer()
    {
        if (!aparecido)
        {
            player.GetComponent<Renderer>().material.SetFloat("_Fade", intensidadAparicion);
            player.GetComponent<Renderer>().material.SetColor("_Color", new Color(0f / 255f, 91 / 255f, 191 / 255f));
            intensidadAparicion += 0.7f * Time.deltaTime;
        }
        if(intensidadAparicion >= 1) aparecido = true;
    }

    void desaparecer()
    {
        if (aparecido && ordenarDesaparicion)
        {
            player.GetComponent<Renderer>().material.SetFloat("_Fade", intensidadAparicion);
            player.GetComponent<Renderer>().material.SetColor("_Color", new Color(0f / 255f, 91 / 255f, 191 / 255f));
            intensidadAparicion -= 0.7f * Time.deltaTime;
        }
        if (intensidadAparicion <= 0)
        {
            aparecido = false;
            ordenarDesaparicion = false;
        }
    }
}
