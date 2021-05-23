using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrera : MonoBehaviour
{
    public Image barreraBarra;
    float regenerarBarrera = 0f;
    public static bool castBarrera = true;
    bool soundIsPlaying = false;
    bool soundBrokenIsPlaying = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            FindObjectOfType<audioManager>().Play("HitBarrera");
            //barreraBarra.fillAmount -= StatsPlayer.jugador.dmgBarrera;
            barreraBarra.fillAmount -= (StatsPlayer.jugador.miBarrera.dañoBarrera / StatsPlayer.jugador.miBarrera.barreraMaxima);
            
            if(barreraBarra.fillAmount <= 0) regenerarBarrera = Time.realtimeSinceStartup + StatsPlayer.jugador.miBarrera.cooldownBarrera;
        }
    }


    private void Update()
    {
        if (barreraBarra.fillAmount <= 0)
        {
            if (!soundBrokenIsPlaying)
            {
                FindObjectOfType<audioManager>().Play("BreakBarrera");
                FindObjectOfType<audioManager>().Stop("BarreraSound");
                soundBrokenIsPlaying = true;
            }
            StartCoroutine("breakBarrer");
        }
        if (regenerarBarrera <= Time.realtimeSinceStartup && !gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            barreraBarra.fillAmount += StatsPlayer.jugador.miBarrera.recuBarrera * Time.deltaTime / StatsPlayer.jugador.miBarrera.barreraMaxima;
        }
        if (!castBarrera && barreraBarra.fillAmount >= 1)
        {
            castBarrera = true;
            gameObject.GetComponent<Animator>().SetBool("BrokenBarrer", false);
            soundBrokenIsPlaying = false;
        }
        if (gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            barreraBarra.fillAmount -= StatsPlayer.jugador.miBarrera.consumoBarrera * Time.deltaTime / StatsPlayer.jugador.miBarrera.barreraMaxima;
            if (!soundIsPlaying)
            {
                FindObjectOfType<audioManager>().Play("BarreraSound");
                soundIsPlaying = true;
            }
        }
        else
        {
            FindObjectOfType<audioManager>().Stop("BarreraSound");
            soundIsPlaying = false;
        }
    }
    IEnumerator breakBarrer()
    {
        gameObject.GetComponent<Animator>().SetBool("BrokenBarrer", true);
        yield return new WaitForSeconds(0.8f);
        castBarrera = false;
    }
}
