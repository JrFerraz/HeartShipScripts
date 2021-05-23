using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASERDrop : MonoBehaviour
{
    public LASER miLASER;
    float parada = 0f;
    public bool manualGeneration = false;

    public float warm_LASER;
    public float daño_LASER;
    public float consumoMun;
    private void Awake()
    {
        miLASER = new LASER();
        parada = transform.position.y;
        if (manualGeneration) miLASER.setLASER(warm_LASER, daño_LASER, consumoMun);
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
                if (StatsPlayer.jugador.miLASER.calidad < miLASER.calidad) StatsPlayer.jugador.setLASER(miLASER.warm_LASER, miLASER.daño_LASER, miLASER.consumoMun);
                else Inventario.misLaser.Add(gameObject);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }

            if (gameObject.transform.parent != null && StatsPlayer.jugador.dinero >= gameObject.transform.parent.GetComponent<ComprarObjetos>().calidad)
            {
                StatsPlayer.jugador.setLASER(miLASER.warm_LASER, miLASER.daño_LASER, miLASER.consumoMun);
                Destroy(gameObject);
            }
        }
    }

    public void colorRareza()
    {
        if (miLASER.calidad < 200) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else if (miLASER.calidad >= 200 && miLASER.calidad < 400) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (miLASER.calidad >= 400 && miLASER.calidad < 525) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public class LASER
    {
        public float warm_LASER;
        public float daño_LASER;
        public float consumoMun;
        public int calidad;

        public LASER()
        {
            calcularCalentamiento();
            calcularConsumo();
            calcularDmg();
            calcularCalidad();
        }

        public LASER(float warm, float dmg, float cons)
        {
            this.warm_LASER = warm;
            this.daño_LASER = dmg;
            this.consumoMun = cons;
            calcularCalidad();
        }

        void calcularCalentamiento()
        {
            float drop = Random.Range(0, 100);
            if (drop <= 20) warm_LASER = Random.Range(1f, 50f);
            else if (drop > 20 && drop <= 50) warm_LASER = Random.Range(51f, 100f);
            else  warm_LASER = Random.Range(100f, 200f);
        }

        void calcularConsumo()
        {
            float drop = Random.Range(0, 100);

            if (warm_LASER <= 50)
            {
                if (drop <= 10) consumoMun = Random.Range(30f, 80f);
                else if (drop > 10 && drop <= 50) consumoMun = Random.Range(81f, 160f);
                else consumoMun = Random.Range(161f, 230f);
            }
            else if (warm_LASER > 50 && warm_LASER < 100)
            {
                if (drop <= 25) consumoMun = Random.Range(30f, 80f);
                else if (drop > 25 && drop <= 60) consumoMun = Random.Range(81f, 160f);
                else consumoMun = Random.Range(161f, 230f);
            }
            else
            {
                if (drop <= 40) consumoMun = Random.Range(30f, 80f);
                else if (drop > 40 && drop <= 80) consumoMun = Random.Range(81f, 160f);
                else consumoMun = Random.Range(161f, 230f);
            }
        }

        void calcularDmg()
        {
            float drop = Random.Range(0, 100);

            if (consumoMun < 80)
            {
                if (drop <= 10) daño_LASER = Random.Range(1f, 2f);
                else if (drop > 10 && drop <= 50) daño_LASER = Random.Range(0.6f, 1f);
                else daño_LASER = Random.Range(0.1f, 0.5f);
            }
            if (consumoMun > 80 && consumoMun <= 160)
            {
                if (drop <= 25) daño_LASER = Random.Range(1f, 2f);
                else if (drop > 25 && drop <= 60) daño_LASER = Random.Range(0.6f, 1f);
                else daño_LASER = Random.Range(0.1f, 0.5f);
            }
            else
            {
                if (drop <= 40) daño_LASER = Random.Range(1f, 2f);
                else if (drop > 40 && drop <= 80) daño_LASER = Random.Range(0.6f, 1f);
                else daño_LASER = Random.Range(0.1f, 0.5f);
            }
        }

        public void calcularCalidad()
        {
            calidad = 200 - Mathf.RoundToInt(warm_LASER);
            calidad += 230 - Mathf.RoundToInt(consumoMun);
            calidad += Mathf.RoundToInt(daño_LASER * 100);
        }
        public void setLASER(float warm, float dmg, float cons)
        {
            this.warm_LASER = warm;
            this.daño_LASER = dmg;
            this.consumoMun = cons;
            calcularCalidad();
        }
    }
}
