using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public GameObject laserItem;
    public GameObject ammo;
    public GameObject ImpactAmmo;
    public GameObject deathAnim;
    public GameObject barrera;
    public GameObject cannon;
    public GameObject speedGen;
    public GameObject laser;
    public GameObject explosiveMarcian;
    public GameObject impactLoot;
    public GameObject coins;
    public GameObject LASERAmmo;

    public static GameObject x;

    public static GameObject realLaserItem;
    public static GameObject realAmmo;
    public static GameObject realImpactAmmo;
    public static GameObject realDeathAnim;
    public static GameObject realBarrera;
    public static GameObject realCannon;
    public static GameObject realSpeedGen;
    public static GameObject realExplosiveMarcian;
    public static GameObject realLaser;
    public static GameObject realImpactLoot;
    public static GameObject realCoins;
    public static GameObject realLASERAmmo;

    private void Awake()
    {
        x = null;
        realLASERAmmo = LASERAmmo;
        realCoins = coins;
        realLaserItem = laserItem;
        realImpactLoot = impactLoot;
        realAmmo = ammo;
        realDeathAnim = deathAnim;
        realBarrera = barrera;
        realCannon = cannon;
        realSpeedGen = speedGen;
        realImpactAmmo = ImpactAmmo;
        realExplosiveMarcian = explosiveMarcian;
        realLaser = laser;
    }

    public static void dropCoinsTrituradora(int calidad, Transform brokeScene)
    {
        x = Instantiate(realDeathAnim, brokeScene.position, Quaternion.identity);
        FindObjectOfType<audioManager>().Play("Boom");
        int dropMonedas = controlarPrecioVenta(calidad);
        int anguloRotacion;
        for (int i = 0; i < dropMonedas; i++)
        {
            anguloRotacion = Random.Range(-90, 90);
            brokeScene.transform.Rotate(0, 0, anguloRotacion);
            x = Instantiate(realCoins, brokeScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = brokeScene.up * 20f;
        }
    }

    public static int controlarPrecioVenta(int calidad)
    {
        float precio;

        if (calidad <= 100) precio = calidad / 10;
        else if (calidad <= 200) precio = calidad / 11;
        else if (calidad <= 300) precio = calidad / 22;
        else if (calidad <= 300) precio = calidad / 33;
        else precio = calidad / 44;

        return (int)precio;
    }

    public static void dropCoins(Transform deadScene)
    {
        int dropMonedas =(int)5 + (StatsPlayer.jugador.combo / 2);
        if(StatsPlayer.jugador.combo > 20) dropMonedas = (int)5 + (StatsPlayer.jugador.combo / 4);
        else if(StatsPlayer.jugador.combo > 40) dropMonedas = (int)5 + (StatsPlayer.jugador.combo / 8);
        int numMonedas = Random.Range(1, dropMonedas);
        int anguloRotacion = 0;

        for (int i = 0; i < numMonedas; i++)
        {
            anguloRotacion = Random.Range(-90, 90);
            deadScene.transform.Rotate(0,0,anguloRotacion);
            x = Instantiate(realCoins, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 20f;
        }
    }
    public static void dropMercaderPacifico(Transform[] itemsPos)
    {
        int drop = 0;

        for (int i = 1; i < itemsPos.Length; i++)
        {
            drop = Random.Range(0, 100);

            if (drop <= 33)
            {
                x = Instantiate(realSpeedGen, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = x.GetComponent<DropGeneradorVelocidad>().gVel.calidadGenerador;
            }
            else if (drop > 33 && drop <= 66)
            {
                x = Instantiate(realBarrera, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = x.GetComponent<DropBarrera>().barrera.calidadBarrera;
            }
            else
            {
                x = Instantiate(realCannon, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = x.GetComponent<Cannon>().miCañon.calidad;
            }

        }
    }

    public static void dropMercaderArmado(Transform[] itemsPos)
    {
        int drop = 0;

        for (int i = 1; i < itemsPos.Length; i++)
        {
            drop = Random.Range(0, 100);

            if (drop <= 33)
            {
                x = Instantiate(realLaser, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = x.GetComponent<LASERDrop>().miLASER.calidad;
            }
            else if (drop > 33 && drop <= 66)
            {
                x = Instantiate(realImpactLoot, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = x.GetComponent<DropImpactAmmo>().miImpacto.calidad;
            }
            else
            {
                x = Instantiate(realLaserItem, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = x.GetComponent<normalLaser>().cannon.calidad;
            }

        }
    }
    public static void resetMercaderMunicion(Transform[] itemPos)
    {
        for (int i = 1; i < itemPos.Length; i++)
        {
            Destroy(itemPos[i].GetChild(0).gameObject);
        }
    }

    public static void dropMercaderMunicion(Transform[] itemsPos)
    {
        int drop = 0;
        Vector2 setTamaño = new Vector2(1,1);

        for (int i = 1; i < itemsPos.Length; i++)
        {
            drop = Random.Range(0, 100);

            if (drop <= 33)
            {
                x = Instantiate(realAmmo, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = 10;
            }
            else if (drop > 33 && drop <= 66)
            {
                x = Instantiate(realImpactAmmo, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = 20;
            }
            else
            {
                x = Instantiate(realLASERAmmo, itemsPos[i].position, Quaternion.identity);
                x.transform.SetParent(itemsPos[i]);
                x.transform.parent.GetComponent<ComprarObjetos>().calidad = 30;
            }
            x.transform.localScale = setTamaño;
        }
    }

    public static void dropLoot(Transform deadScene)
    {
        Instantiate(realDeathAnim, deadScene.position, Quaternion.identity);
        int anguloRotacion = Random.Range(-25, 25);
        deadScene.transform.Rotate(0, 0, anguloRotacion);
        int drop = Random.Range(0, 100);
        if (drop <= 10)
        {
            x = Instantiate(realLaserItem, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 10f;
        }
        else if (drop > 10 && drop <= 20)
        {
            x = Instantiate(realBarrera, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 10f;
        }
        else if (drop > 20 && drop <= 30)
        {
            x = Instantiate(realCannon, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 10f;
        }
        else if (drop > 30 && drop < 40)
        {
            x = Instantiate(realSpeedGen, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 10f;
        }
        else if (drop > 40 && drop < 50)
        {
            Instantiate(realAmmo, deadScene.position, Quaternion.identity);
        }
        else if (drop > 60 && drop < 70)
        {
            x = Instantiate(realLaser, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 10f;
        }
        else if (drop > 70 && drop < 80)
        {
            x = Instantiate(realImpactLoot, deadScene.position, Quaternion.identity);
            x.GetComponent<Rigidbody2D>().velocity = deadScene.up * 10f;
        }
        //else Instantiate(realImpactAmmo, deadScene.position, Quaternion.identity);
    }

    public static void quitarGravedad(GameObject x, float parada)
    {
        if (x.transform.position.y <= parada - 1)
        {
            x.GetComponent<Rigidbody2D>().gravityScale = 0;
            x.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            x.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }

    public static void marcianExplosion(Transform deadScene, float radio)
    { 
        GameObject x = Instantiate(realExplosiveMarcian, deadScene.position, Quaternion.identity);
        x.GetComponent<CircleCollider2D>().radius = radio;
    }

    public static void soltarMunImpacto(GameObject munImpacto, Transform lootPlace)
    {
        int anguloRotacion = Random.Range(-45, 45);
        Transform y = lootPlace;
        y.transform.Rotate(0, 0, anguloRotacion);

        x = Instantiate(realImpactLoot, new Vector2(y.position.x, y.position.y+2), Quaternion.identity);

        x.GetComponent<DropImpactAmmo>().miImpacto.setImpactAmmo(munImpacto.GetComponent<DropImpactAmmo>().miImpacto.daño_munImpacto, munImpacto.GetComponent<DropImpactAmmo>().miImpacto.cooldown_munImpacto,
            munImpacto.GetComponent<DropImpactAmmo>().miImpacto.impulso_munImpacto);
        x.GetComponent<Rigidbody2D>().velocity = y.up * 10f;
    }

    public static void soltarBarreras(GameObject barrera, Transform lootPlace)
    {
        int anguloRotacion = Random.Range(-45, 45);
        Transform y = lootPlace;
        y.transform.Rotate(0, 0, anguloRotacion);

        x = Instantiate(realBarrera, new Vector2(y.position.x, y.position.y + 2), Quaternion.identity);

        x.GetComponent<DropBarrera>().barrera.setBarrera(barrera.GetComponent<DropBarrera>().barrera.cooldownBarrera, barrera.GetComponent<DropBarrera>().barrera.consumoBarrera, barrera.GetComponent<DropBarrera>().barrera.recuBarrera,
            barrera.GetComponent<DropBarrera>().barrera.barreraMaxima, barrera.GetComponent<DropBarrera>().barrera.dañoBarrera);

        x.GetComponent<Rigidbody2D>().velocity = y.up * 10f;
    }

    public static void soltarLaserShots(GameObject laserShot, Transform lootPlace)
    {
        int anguloRotacion = Random.Range(-45, 45);
        Transform y = lootPlace;
        y.transform.Rotate(0, 0, anguloRotacion);

        x = Instantiate(realLaserItem, new Vector2(y.position.x, y.position.y + 2), Quaternion.identity);

        x.GetComponent<normalLaser>().cannon.setCannon(laserShot.GetComponent<normalLaser>().cannon.cooldown, laserShot.GetComponent<normalLaser>().cannon.dmg, laserShot.GetComponent<normalLaser>().cannon.calentamiento,
            laserShot.GetComponent<normalLaser>().cannon.speed);

        x.GetComponent<Rigidbody2D>().velocity = y.up * 10f;
    }

    public static void soltarCañones(GameObject cannon, Transform lootPlace)
    {
        int anguloRotacion = Random.Range(-45, 45);
        Transform y = lootPlace;
        y.transform.Rotate(0, 0, anguloRotacion);

        x = Instantiate(realCannon, new Vector2(y.position.x, y.position.y + 2), Quaternion.identity);

        x.GetComponent<Cannon>().miCañon.setCannon(cannon.GetComponent<Cannon>().miCañon.calentamiento_max, cannon.GetComponent<Cannon>().miCañon.disipador_no_sobrecalentado, cannon.GetComponent<Cannon>().miCañon.disipador_sobrecalentado);

        x.GetComponent<Rigidbody2D>().velocity = y.up * 10f;
    }

    public static void soltarLASER(GameObject LASER, Transform lootPlace)
    {
        int anguloRotacion = Random.Range(-45, 45);
        Transform y = lootPlace;
        y.transform.Rotate(0, 0, anguloRotacion);

        x = Instantiate(realLaser, new Vector2(y.position.x, y.position.y + 2), Quaternion.identity);

        x.GetComponent<LASERDrop>().miLASER.setLASER(LASER.GetComponent<LASERDrop>().miLASER.warm_LASER, LASER.GetComponent<LASERDrop>().miLASER.daño_LASER, LASER.GetComponent<LASERDrop>().miLASER.consumoMun);

        x.GetComponent<Rigidbody2D>().velocity = y.up * 10f;
    }
    public static void soltarGenV(GameObject genV, Transform lootPlace)
    {
        int anguloRotacion = Random.Range(-45, 45);
        Transform y = lootPlace;
        y.transform.Rotate(0, 0, anguloRotacion);

        x = Instantiate(realSpeedGen, new Vector2(y.position.x, y.position.y + 2), Quaternion.identity);

        x.GetComponent<DropGeneradorVelocidad>().gVel.setGenerador(genV.GetComponent<DropGeneradorVelocidad>().gVel.velocidadNave, genV.GetComponent<DropGeneradorVelocidad>().gVel.cdDash, genV.GetComponent<DropGeneradorVelocidad>().gVel.impulsoDash);

        x.GetComponent<Rigidbody2D>().velocity = y.up * 10f;
    }
}
