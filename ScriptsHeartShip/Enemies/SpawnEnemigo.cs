using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigo : MonoBehaviour
{
    private float intensidadAparicion = 0;
    private GameObject Interfaz;

    private void Awake()
    {
        Interfaz = transform.Find("CanvasEnemigo").gameObject;
        Invoke("invocarCanvas",0.8f);
    }
    void Update()
    {
        tiempoSpawn(intensidadAparicion);
    }
    void tiempoSpawn(float t)
    {
        if (t <= 1)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", intensidadAparicion);
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            intensidadAparicion += 1f * Time.deltaTime;
        }
    }

    void invocarCanvas()
    {
        Interfaz.SetActive(true);
    }
}
