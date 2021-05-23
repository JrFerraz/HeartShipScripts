using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trituradora : MonoBehaviour
{
    public bool pinchoDerecho = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("LOOT"))
        {
            GenerateItems.dropCoinsTrituradora(encontrarCalidad(collision.gameObject), collision.transform);

            Destroy(collision.gameObject);
        }

        if (collision.tag.Equals("trituradora"))
        {
            if (pinchoDerecho) pinchoDerecho = false;
            else pinchoDerecho = true;
        }
    }

    int encontrarCalidad(GameObject loot)
    {
        int calidad = 0;

        if (loot.GetComponent<normalLaser>() != null) calidad = (int)loot.GetComponent<normalLaser>().cannon.calidad;

        else if (loot.GetComponent<Cannon>() != null) calidad = (int)loot.GetComponent<Cannon>().miCañon.calidad;

        else if (loot.GetComponent<DropBarrera>() != null) calidad = (int)loot.GetComponent<DropBarrera>().barrera.calidadBarrera;

        else if (loot.GetComponent<LASERDrop>() != null) calidad = (int)loot.GetComponent<LASERDrop>().miLASER.calidad;

        else if (loot.GetComponent<DropImpactAmmo>() != null) calidad = (int)loot.GetComponent<DropImpactAmmo>().miImpacto.calidad;

        else if (loot.GetComponent<DropGeneradorVelocidad>() != null) calidad = (int)loot.GetComponent<DropGeneradorVelocidad>().gVel.calidadGenerador;

        return calidad;
    }

    private void Update()
    {
        if(!pinchoDerecho) transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

        else transform.position += new Vector3(-2 * Time.deltaTime, 0, 0);
    }
}
