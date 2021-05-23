using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static List<GameObject> misCañones;
    public static List<GameObject> misBarreras;
    public static List<GameObject> misImpactShots;
    public static List<GameObject> misLaserShot;
    public static List<GameObject> misLaser;
    public static List<GameObject> misGenV;

    public static GameObject generator;

    private void Awake()
    {
        generator = null;
        misCañones = new List<GameObject>();
        misBarreras = new List<GameObject>();
        misGenV = new List<GameObject>();
        misImpactShots = new List<GameObject>();
        misLaser = new List<GameObject>();
        misLaserShot = new List<GameObject>();
    }

    public static void soltarItems(Transform playerPos)
    {
        soltarEscudos(playerPos);
        soltarBarreras(playerPos);
        soltarLaserShots(playerPos);
        soltarCañones(playerPos);
        soltarLASER(playerPos);
        soltarGenerador(playerPos);
    }

    public static void soltarEscudos(Transform playerPos)
    {
        foreach (GameObject item in misImpactShots)
        {
            GenerateItems.soltarMunImpacto(item, playerPos);
        }
        misImpactShots.Clear();
    }
    public static void soltarBarreras(Transform playerPos)
    {
        foreach (GameObject item in misBarreras)
        {
            GenerateItems.soltarBarreras(item, playerPos);
        }
        misBarreras.Clear();
    }

    public static void soltarLaserShots(Transform playerPos)
    {
        foreach (GameObject item in misLaserShot)
        {
            GenerateItems.soltarLaserShots(item, playerPos);
        }
        misLaserShot.Clear();
    }

    public static void soltarCañones(Transform playerPos)
    {
        foreach (GameObject item in misCañones)
        {
            GenerateItems.soltarCañones(item, playerPos);
        }
        misCañones.Clear();
    }
    public static void soltarLASER(Transform playerPos)
    {
        foreach (GameObject item in misLaser)
        {
            GenerateItems.soltarLASER(item, playerPos);
        }
        misLaser.Clear();
    }
    public static void soltarGenerador(Transform playerPos)
    {
        foreach (GameObject item in misGenV)
        {
            GenerateItems.soltarGenV(item, playerPos);
        }
        misGenV.Clear();
    }
    private void Update()
    {

    }

}
