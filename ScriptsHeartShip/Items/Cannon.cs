using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public CannonHeartship miCañon;
    float parada = 0f;
    public bool manualGeneration = false;

    bool añadido = false;

    public float calMax;
    public float disSobreCal;
    public float disNoSobreCal;

    private void Awake()
    {
        miCañon = new CannonHeartship();
        parada = transform.position.y;
        if (manualGeneration) miCañon.setCannon(calMax, disNoSobreCal, disSobreCal);
    }
    void Start()
    {
        colorRareza();
    }
    private void FixedUpdate()
    {
        //quitarGravedad();
        GenerateItems.quitarGravedad(gameObject, parada);
    }
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, 300f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (gameObject.transform.parent == null)
            {
                if (StatsPlayer.jugador.miCannon.calidad <= miCañon.calidad) StatsPlayer.jugador.setCannon(miCañon.calentamiento_max, miCañon.disipador_no_sobrecalentado, miCañon.disipador_sobrecalentado);
                else if (!añadido)
                {
                    Inventario.misCañones.Add(gameObject);
                    añadido = true;
                }
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            if (gameObject.transform.parent != null && StatsPlayer.jugador.dinero >= gameObject.transform.parent.GetComponent<ComprarObjetos>().calidad)
            {
                StatsPlayer.jugador.setCannon(miCañon.calentamiento_max, miCañon.disipador_no_sobrecalentado, miCañon.disipador_sobrecalentado);
                Destroy(gameObject);
            }
        }
    }

    public void colorRareza()
    {
        if (miCañon.calidad < 100) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else if (miCañon.calidad >= 100 && miCañon.calidad < 225) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (miCañon.calidad >= 225 && miCañon.calidad < 350) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public class CannonHeartship
    {
        public float calentamiento_max;
        public float disipador_sobrecalentado;
        public float disipador_no_sobrecalentado;
        public float calidad;

        public CannonHeartship()
        {
            crearCannon();
        }

        public void crearCannon()
        {
            calidadCalentamientoMax();
            calidadSobrecalentado();
            calidadNoSobrecalentado();
            calcularCalidad();
        }

        public void calcularCalidad()
        {
            calidad = disipador_sobrecalentado + disipador_no_sobrecalentado + calentamiento_max;
        }

        public void calidadCalentamientoMax()
        {

            if (Random.value <= 0.3)
            {
                if (Random.value <= 0.3) calentamiento_max = Random.Range(110f, 200f);
                else if (Random.value <= 0.8) calentamiento_max = Random.Range(50f, 110f);
                else calentamiento_max = Random.Range(10f, 49f);
            }
            else
            {
                if (Random.value <= 0.1) calentamiento_max = Random.Range(110f, 200f);
                else if (Random.value <= 0.5) calentamiento_max = Random.Range(50f, 110f);
                else calentamiento_max = Random.Range(10f, 49f);
            }
        }

        public void calidadSobrecalentado()
        {
            float min = (calentamiento_max * 0.1f);
            float max = calentamiento_max;
            float drop = Random.Range(0, 100);

            if (drop <= 20) disipador_sobrecalentado = Random.Range((max * 0.75f), max);
            if (drop > 20 && drop <= 50) disipador_sobrecalentado = Random.Range((min * 5), (max * 0.75f));
            if (drop > 50) disipador_sobrecalentado = Random.Range(min, (min * 5));
        }

        public void calidadNoSobrecalentado()
        {
            float min = (calentamiento_max * 0.1f);
            float max = calentamiento_max;
            float drop = Random.Range(0, 100);

            if (drop <= 30) disipador_no_sobrecalentado = Random.Range((max * 0.75f), max);
            if (drop > 30 && drop <= 60) disipador_no_sobrecalentado = Random.Range((min * 5), (max * 0.75f));
            if (drop > 60) disipador_no_sobrecalentado = Random.Range(min, (min * 5));
        }

        public void setCannon(float calMax, float DSS, float DSC)
        {
            this.calentamiento_max = calMax;
            this.disipador_no_sobrecalentado = DSS;
            this.disipador_sobrecalentado = DSC;
            calcularCalidad();
        }
    }
}
