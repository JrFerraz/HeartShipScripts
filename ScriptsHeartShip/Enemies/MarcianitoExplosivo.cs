using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcianitoExplosivo : MonoBehaviour
{
    Sprite[] sprites;
    //public statsMarcianitoExplosivo marcianito;

    public BasicEnemy mExplosivo;

    Transform player;

    public float lineOfSite;
    public float explosionRange;
    public float speed;
    public float tiempoTransicion;
    public float lapsoTransicion;
    public int numTransicion;
    public float tamaño;

    private float volumen;

    private bool bloqued;

    public Sound tick;

    public float speedTick;
    void darTiempo()
    {
       GetComponent<BasicEnemy>().statsEnemigo.stunned = false;
    }
    private void Awake()
    {
        if (buttonActions.silenciarTodo) volumen = 0;
        else volumen = 1;
        bloqued = false;
    }
    void Start()
    {
        try
        {
            player = GameObject.Find("Player").transform;
        }
        catch (System.Exception e) { };
        Invoke("darTiempo", 1f);
        //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("marcianitoExplosivo.marcianitoExplosivo_7");

        sprites = Resources.LoadAll<Sprite>("MarExplDef");

        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    void Update()
    {
        if (!GetComponent<BasicEnemy>().statsEnemigo.stunned) MoveAndExplote();
    }
    void MoveAndExplote()
    {
        detectarBloqueo();
        if (player != null)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer <= 40) controlarVolumen(distanceFromPlayer);
            else volumen = 0;
            if (distanceFromPlayer < lineOfSite && distanceFromPlayer > explosionRange)
            {
                if(!bloqued) transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                if(!InterfazGame.isPaused) calentarExplosion();
            }
            else if (distanceFromPlayer > lineOfSite)
            {
                if(!InterfazGame.isPaused) enfriarExplosion();
            }
            else if (distanceFromPlayer <= explosionRange && StatsPlayer.jugador.vivo)
            {
                if(!bloqued) transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                if(!InterfazGame.isPaused) calentarExplosion();
                if (numTransicion == 24)
                {
                    GenerateItems.marcianExplosion(transform, 1.5f);
                    Destroy(gameObject);
                }
            }
        }
    }
    void controlarVolumen(float distancia)
    {
        volumen = -(distancia * 0.04f) + 1;
    }

    void detectarBloqueo()
    {
        Vector2 vectorEnemigoJugador = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorEnemigoJugador, 1f);
        if (hit)
        {
            if (hit.transform.gameObject.layer == 15) bloqued = true;
        }
        else bloqued = false;
    }
    void calentarExplosion()
    {
        if(Time.realtimeSinceStartup >= tiempoTransicion)
        {
            if (numTransicion != 24)
            {
                numTransicion++;
                speedTick += 0.1f;
                tamaño += 0.03f;
                transform.localScale = new Vector3(tamaño, tamaño, 0);
            }
            GetComponent<SpriteRenderer>().sprite = sprites[numTransicion];
            tiempoTransicion = Time.realtimeSinceStartup + lapsoTransicion;
            FindObjectOfType<audioManager>().SetPitchPro(speedTick, volumen, tick);
        }
    }
    void enfriarExplosion()
    {
        if (Time.realtimeSinceStartup >= tiempoTransicion)
        {
            if (numTransicion != 0)
            {
                numTransicion--;
                speedTick -= 0.1f;
                tamaño -= 0.03f;
                transform.localScale = new Vector3(tamaño, tamaño, 0);
            }
            if (numTransicion != 0) FindObjectOfType<audioManager>().SetPitchPro(speedTick, volumen, tick);
            GetComponent<SpriteRenderer>().sprite = sprites[numTransicion];
            tiempoTransicion = Time.realtimeSinceStartup + lapsoTransicion;
        }
    }
}
