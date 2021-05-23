using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousLaserAmmo : MonoBehaviour
{
    float test = 0;
    private bool restartAnim = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            StatsPlayer.jugador.mun_LASER += 100;
            Destroy(gameObject);
            FindObjectOfType<audioManager>().Play("GetAmmo");
        }
    }
    void revelarMunicion()
    {
        if (test <= 1)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", test);
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            test += 0.7f * Time.deltaTime;

            if (test >= 0.9f)
            {
                restartAnim = true;
            }
        }
    }

    void ocultarMunicion()
    {
        if (restartAnim)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", test);
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            test -= 1.5f * Time.deltaTime;
            if (test <= 0)
            {
                restartAnim = false;
            }
        }
    }
    void resaltarLoot()
    {
        revelarMunicion();
        ocultarMunicion();
    }

    private void Update()
    {
        resaltarLoot();
    }
}
