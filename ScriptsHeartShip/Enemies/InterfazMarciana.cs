using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazMarciana : MonoBehaviour
{
    public Image vida;
    public Image armadura;
    public Image escudo;
    public Slider sliderVida;
    public Slider sliderArmadura;
    public Slider sliderEscudo;

    public float vidaF;
    private float vidaMax;
    public float escudoF;
    private float escudoMax;
    public float armaduraF;
    private float armaduraMax;

    public bool muerto = false;

    void Start()
    {
        cargarValores();
        interfazEnemigo();
    }

    void Update()
    {
        updateUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ammo2") && gameObject.layer == 8) GetComponent<BasicEnemy>().statsEnemigo.stunned = true;

        if (collision.gameObject.tag.Equals("Ammo2") && GetComponent<MarcianitoExplosivo>() != null) Invoke("explotarImpacto", 0.3f);
        if (collision.gameObject.layer == 14)
        {
            float[] barrasSalud = ammoDamages.standardAmmo(escudoF > 0, armaduraF > 0, 15f, vidaF, escudoF, armaduraF, collision.gameObject.tag);
            vidaF = barrasSalud[0];
            armaduraF = barrasSalud[1];
            escudoF = barrasSalud[2];

            if (vidaF <= 0 && !muerto) Morir();
        }
        if (collision.gameObject.layer == 15)
        {
            detenerStun();
        }

        dañar(collision.gameObject);
        Invoke("detenerStun",1f);
        Invoke("stopImpact",0.1f);
    }
    void explotarImpacto()
    {
        if (GetComponent<MarcianitoExplosivo>().numTransicion >= 23) { GenerateItems.marcianExplosion(transform, 2f); Destroy(gameObject); }
    }

    void detenerStun()
    {
        GetComponent<BasicEnemy>().statsEnemigo.stunned = false;
    }

    void stopImpact()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().Sleep();
    }

    void dañar(GameObject typeAmmo)
    {
        if (typeAmmo.tag.Equals("Pinchos") && typeAmmo.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
        {
            float[] barrasSalud = ammoDamages.standardAmmo(escudoF > 0, armaduraF > 0, 50f, vidaF, escudoF, armaduraF, typeAmmo.tag);
            vidaF = barrasSalud[0];
            armaduraF = barrasSalud[1];
            escudoF = barrasSalud[2];
        }
        else if (!typeAmmo.tag.Equals("Pinchos"))
        {
            float[] barrasSalud = ammoDamages.standardAmmo(escudoF > 0, armaduraF > 0, StatsPlayer.jugador.dañoEscogido, vidaF, escudoF, armaduraF, typeAmmo.tag);
            vidaF = barrasSalud[0];
            armaduraF = barrasSalud[1];
            escudoF = barrasSalud[2];
        }
        if (vidaF <= 0 && !muerto) Morir();
    }
    void updateUI()
    {
        sliderVida.value = vidaF / vidaMax;
        sliderArmadura.value = armaduraF / armaduraMax;
        sliderEscudo.value = escudoF / escudoMax;
    }
    void cargarValores()
    {
        vidaF = GetComponent<BasicEnemy>().statsEnemigo.vida;
        vidaMax = GetComponent<BasicEnemy>().statsEnemigo.vidaMax;
        escudoF = GetComponent<BasicEnemy>().statsEnemigo.escudo;
        escudoMax = GetComponent<BasicEnemy>().statsEnemigo.escudoMax;
        armaduraF = GetComponent<BasicEnemy>().statsEnemigo.armadura;
        armaduraMax = GetComponent<BasicEnemy>().statsEnemigo.armaduraMax;
    }
    void interfazEnemigo()
    {
        if (escudoF == 0 && armaduraF > 0)
        {
            sliderEscudo.gameObject.SetActive(false);
        }
        else if (escudoF > 0 && armaduraF == 0)
        {
            sliderEscudo.gameObject.transform.position = sliderArmadura.gameObject.transform.position;
            sliderArmadura.gameObject.SetActive(false);
        }
        else if (escudoF == 0 && armaduraF == 0)
        {
            sliderEscudo.gameObject.SetActive(false);
            sliderArmadura.gameObject.SetActive(false);
        }
    }

    public void Morir()
    {
        //lo mato con un bool primero por si hay dos colisiones distintas en el mismo frame
        muerto = true;
        GenerateItems.dropLoot(transform);
        GenerateItems.dropCoins(transform);
        Destroy(gameObject);
        StatsPlayer.jugador.numKills++;
        StatsPlayer.jugador.combo++;
        StatsPlayer.jugador.comboTime = Time.realtimeSinceStartup + 2.5f;
        CameraShake.Instance.ShakeCamera(20f, .1f);
        FindObjectOfType<audioManager>().Play("Boom");
    }
}
