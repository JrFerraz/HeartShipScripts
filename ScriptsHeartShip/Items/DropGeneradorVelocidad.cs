using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGeneradorVelocidad : MonoBehaviour
{
    public GeneradorVelocidad gVel;
    float parada = 0f;
    public bool manualGeneration = false;

    public float v;
    public float cd;
    public float imp;
    private void Awake()
    {
        gVel = new GeneradorVelocidad();
        parada = transform.position.y;
        if (manualGeneration) gVel.setGenerador(v,cd,imp);

    }

    private void FixedUpdate()
    {
        GenerateItems.quitarGravedad(gameObject, parada);
    }

    private void Start()
    {
        colorRareza();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f, 300f) * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            if (gameObject.transform.parent == null)
            {
                if (StatsPlayer.jugador.miGeneradorVelocidad.calidadGenerador < gVel.calidadGenerador) StatsPlayer.jugador.setSpeedGenerator(gVel.impulsoDash, gVel.cdDash, gVel.velocidadNave);
                else Inventario.misGenV.Add(gameObject);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            if (gameObject.transform.parent != null && StatsPlayer.jugador.dinero >= gameObject.transform.parent.GetComponent<ComprarObjetos>().calidad)
            {
                StatsPlayer.jugador.setSpeedGenerator(gVel.impulsoDash, gVel.cdDash, gVel.velocidadNave);
                Destroy(gameObject);
            }


        }
    }
    public void colorRareza()
    {
        if (gVel.calidadGenerador < 15) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else if (gVel.calidadGenerador >= 15 && gVel.calidadGenerador < 30) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (gVel.calidadGenerador >= 30 && gVel.calidadGenerador < 40) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public class GeneradorVelocidad
    {
        public float velocidadNave;
        public float cdDash;
        public float impulsoDash;
        public float calidadGenerador;

        public GeneradorVelocidad()
        {
            dropVelocidad();
            dropCooldownDash();
            dropDashImpulso();
            calcularCalidad();
        }

        void dropVelocidad()
        {
            if (Random.value <= 0.1f) velocidadNave = Random.Range(16,20);
            else if (Random.value <= 0.3f) velocidadNave = Random.Range(10,15);
            else velocidadNave = Random.Range(5,9);
        }

        void dropCooldownDash()
        {
            if (Random.value <= 0.1f) cdDash = Random.Range(0.2f, 1);
            else if (Random.value <= 0.3f) cdDash = Random.Range(1.1f, 2);
            else cdDash = Random.Range(2.1f, 5);
        }

        void dropDashImpulso()
        {
            if (Random.value <= 0.1f) impulsoDash = Random.Range(8, 10);
            else if (Random.value <= 0.3f) impulsoDash = Random.Range(5, 7);
            else impulsoDash = Random.Range(1, 4);
        }

        public void calcularCalidad()
        {
            calidadGenerador = velocidadNave;
            calidadGenerador += -((cdDash * 3) - 15);
            calidadGenerador += impulsoDash * 1.5f;
        }

        public void setGenerador(float vel, float cdDash, float impDash)
        {
            this.velocidadNave = vel;
            this.cdDash = cdDash;
            this.impulsoDash = impDash;
            calcularCalidad();
        }
    }
}
