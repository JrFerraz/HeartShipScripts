using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBarrera : MonoBehaviour
{
    public Barrera barrera;
    float parada = 0f;
    public bool manualGeneration = false;

    public float cd;
    public float cons;
    public float rec;
    public float barMax;
    public float dmg;

    private void Awake()
    {
        barrera = new Barrera();
        parada = transform.position.y;
        if (manualGeneration) barrera.setBarrera(cd, cons, rec, barMax, dmg);
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

    public void colorRareza()
    {
        if (barrera.calidadBarrera < 150) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else if (barrera.calidadBarrera >= 150 && barrera.calidadBarrera < 300) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (barrera.calidadBarrera >= 300 && barrera.calidadBarrera < 400) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if(gameObject.transform.parent == null)
            {
                if (StatsPlayer.jugador.miBarrera.calidadBarrera < barrera.calidadBarrera) StatsPlayer.jugador.setBarrer(barrera.cooldownBarrera, barrera.barreraMaxima, barrera.recuBarrera, barrera.consumoBarrera, barrera.dañoBarrera);
                else Inventario.misBarreras.Add(gameObject);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }

            if(gameObject.transform.parent != null && StatsPlayer.jugador.dinero >= gameObject.transform.parent.GetComponent<ComprarObjetos>().calidad)
            {
                StatsPlayer.jugador.setBarrer(barrera.cooldownBarrera, barrera.barreraMaxima, barrera.recuBarrera, barrera.consumoBarrera, barrera.dañoBarrera);
                Destroy(gameObject);
            }
        }
    }

    public class Barrera
    {
        public float cooldownBarrera;
        public float consumoBarrera;
        public float recuBarrera;
        public float barreraMaxima;
        public float dañoBarrera;
        public float calidadBarrera;

        public Barrera()
        {
            crearBarrera();
        }

        public void crearBarrera()
        {
            dropSencillo();
            calidadSencilla();
        }

        public void dropSencillo()
        {
            if (Random.value <= 0.7) cooldownBarrera = Random.Range(3f, 10f);
            else cooldownBarrera = Random.Range(0.1f, 3f);

            if (Random.value <= 0.6) consumoBarrera = Random.Range(5f, 10f);
            else consumoBarrera = Random.Range(1f, 5f);

            if (Random.value <= 0.6) recuBarrera = Random.Range(1f, 6f);
            else recuBarrera = Random.Range(7f, 10f);

            calidadMaxBarrera();

            if(Random.value <= 0.7) dañoBarrera = Random.Range(15f, 25f);
            else dañoBarrera = Random.Range(0f, 14f);
        }

        public void calidadSencilla()
        {
            calidadBarrera = barreraMaxima;
            calidadBarrera += -((dañoBarrera * 4) - 100);
            Mathf.Round(calidadBarrera);
            calidadBarrera += -((cooldownBarrera * 10) - 100);
            calidadBarrera += -((consumoBarrera * 10) - 100);
            calidadBarrera += recuBarrera * 10;
        }

        public void calidadRecuperarBarrera()
        {
            if (calidadBarrera <= 75)
            {
                if (Random.value <= 0.3) recuBarrera = Random.Range(7f, 10f);
                else if (Random.value <= 0.5) recuBarrera = Random.Range(3f, 6f);
                else recuBarrera = Random.Range(1f, 3f);
            }
            else if (calidadBarrera > 75 && calidadBarrera <= 150)
            {
                if (Random.value <= 0.2) recuBarrera = Random.Range(1f, 3f);
                else if (Random.value <= 0.3) recuBarrera = Random.Range(7f, 10f);
                else recuBarrera = Random.Range(3f, 6f);
            }
            else if (calidadBarrera > 150 && calidadBarrera <= 240)
            {
                if (Random.value <= 0.3) recuBarrera = Random.Range(3f, 6f);
                else if (Random.value <= 0.7) recuBarrera = Random.Range(1f, 3f);
                else recuBarrera = Random.Range(7f, 10f);
            }
            else
            {
                if (Random.value <= 0.1) recuBarrera = Random.Range(7f, 10f);
                else if (Random.value <= 0.7) recuBarrera = Random.Range(1f, 3f);
                else recuBarrera = Random.Range(3f, 6f);
            }

            calidadBarrera += recuBarrera * 10;

            Debug.Log(calidadBarrera + "hasta recuperar barrera");
        }

        public void calidadConsumoBarrera()
        {
            if (calidadBarrera <= 130)
            {
                if (Random.value <= 0.3) consumoBarrera = Random.Range(1f, 3f);
                else if (Random.value <= 0.5) consumoBarrera = Random.Range(3f, 6f);
                else consumoBarrera = Random.Range(7f, 10f);
            }
            else if (calidadBarrera > 130 && calidadBarrera <= 230)
            {
                if (Random.value <= 0.2) consumoBarrera = Random.Range(1f, 3f);
                else if (Random.value <= 0.3) consumoBarrera = Random.Range(3f, 6f);
                else consumoBarrera = Random.Range(7f, 10f);
            }
            else if (calidadBarrera > 230 && calidadBarrera <= 270)
            {
                if (Random.value <= 0.3) consumoBarrera = Random.Range(3f, 6f);
                else if (Random.value <= 0.2) consumoBarrera = Random.Range(1f, 3f);
                else consumoBarrera = Random.Range(7f, 10f);
            }
            else
            {
                if (Random.value <= 0.1) consumoBarrera = Random.Range(1f, 3f);
                else if (Random.value <= 0.7) consumoBarrera = Random.Range(7f, 10f);
                else consumoBarrera = Random.Range(3f, 6f);
            }

            calidadBarrera += -((consumoBarrera * 10) - 100);
            Debug.Log(calidadBarrera + "hasta consumo barrera");
        }

        public void calidadCooldownBarrera()
        {
            if (calidadBarrera <= 65)
            {
                if (Random.value <= 0.3) cooldownBarrera = Random.Range(0.1f, 3f);
                else if (Random.value <= 0.5) cooldownBarrera = Random.Range(3f, 6f);
                else cooldownBarrera = Random.Range(4f, 10f);
            }
            else if (calidadBarrera > 65 && calidadBarrera <= 115)
            {
                if (Random.value <= 0.2) cooldownBarrera = Random.Range(0.1f, 3f);
                else if (Random.value <= 0.3) cooldownBarrera = Random.Range(3f, 6f);
                else cooldownBarrera = Random.Range(4f, 10f);
            }
            else if (calidadBarrera > 115 && calidadBarrera <= 180)
            {
                if (Random.value <= 0.3) cooldownBarrera = Random.Range(3f, 6f);
                else if (Random.value <= 0.2) cooldownBarrera = Random.Range(0.1f, 3f);
                else cooldownBarrera = Random.Range(4f, 10f);
            }
            else
            {
                if (Random.value <= 0.3) cooldownBarrera = Random.Range(0.1f, 3f);
                else if (Random.value <= 0.7) cooldownBarrera = Random.Range(4f, 10f);
                else cooldownBarrera = Random.Range(3f, 6f);
            }

            calidadBarrera += -((cooldownBarrera * 10) - 100);
            Debug.Log(calidadBarrera + "hasta cooldown barrera");
        }

        public void calidadMaxBarrera()
        {
            float x = Random.Range(0f, 100f);

            if (x <= 50) barreraMaxima = Random.Range(10f, 30f);
            else if (x > 50 && x <= 80) barreraMaxima = Random.Range(31f, 80f);
            else barreraMaxima = Random.Range(81f, 100f);

            //calidadBarrera = barreraMaxima;

            //Debug.Log(calidadBarrera + "hasta max barrera");
        }

        public void calidadDmgBarrera()
        {
            if (calidadBarrera <= 25)
            {
                if (Random.value <= 0.3) dañoBarrera = Random.Range(0.1f, 4f);
                else if (Random.value <= 0.5) dañoBarrera = Random.Range(5f, 14f);
                else dañoBarrera = Random.Range(15f, 25f);
            }
            else if (calidadBarrera > 25 && calidadBarrera <= 50)
            {
                if (Random.value <= 0.2) dañoBarrera = Random.Range(0.1f, 4f);
                else if (Random.value <= 0.3) dañoBarrera = Random.Range(15f, 25f);
                else dañoBarrera = Random.Range(5f, 14f);
            }
            else if (calidadBarrera > 50 && calidadBarrera <= 80)
            {
                if (Random.value <= 0.1) dañoBarrera = Random.Range(0.1f, 4f);
                else if (Random.value <= 0.5) dañoBarrera = Random.Range(15f, 25f);
                else dañoBarrera = Random.Range(5f, 14f);
            }
            else 
            {
                if (Random.value <= 0.3) dañoBarrera = Random.Range(0.1f, 4f);
                else if (Random.value <= 0.7) dañoBarrera = Random.Range(15f, 25f);
                else dañoBarrera = Random.Range(5f, 14f);
            }

            calidadBarrera += -((dañoBarrera * 4) - 100);
            Debug.Log(calidadBarrera + "hasta dmg barrera");
        }

        public void setBarrera(float cd, float cons, float recu, float barMax, float dmg)
        {
            this.cooldownBarrera = cd;
            this.consumoBarrera = cons;
            this.recuBarrera = recu;
            this.barreraMaxima = barMax;
            this.dañoBarrera = dmg;

            calidadSencilla();
        }
    }
}
