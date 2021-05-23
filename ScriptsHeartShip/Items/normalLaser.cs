using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalLaser : MonoBehaviour
{
    public normalCannon cannon;
    float parada = 0f;
    public bool manualGeneration = false;

    public float cd;
    public float d;
    public float speed;
    public int warm;


    private void Awake()
    {
        cannon = new normalCannon();
        parada = transform.position.y;
        if (manualGeneration) cannon.setCannon(cd, d, warm, speed);
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
                if (StatsPlayer.jugador.miLaserNormal.calidad < cannon.calidad) StatsPlayer.jugador.setNormalCannon(cannon.speed, cannon.dmg, cannon.cooldown, cannon.calentamiento);
                else Inventario.misLaserShot.Add(gameObject);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            if (gameObject.transform.parent != null && StatsPlayer.jugador.dinero >= gameObject.transform.parent.GetComponent<ComprarObjetos>().calidad)
            {
                StatsPlayer.jugador.setNormalCannon(cannon.speed, cannon.dmg, cannon.cooldown, cannon.calentamiento);
                Destroy(gameObject);
            }


        }

    }
    public void colorRareza()
    {
        if (cannon.calidad < 15) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else if (cannon.calidad >= 15 && cannon.calidad < 30) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (cannon.calidad >= 30 && cannon.calidad < 40) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public class normalCannon
    {
        public float cooldown;
        public float dmg;
        public float speed;
        public int calentamiento;
        public float calidad;

        public normalCannon()
        {
            calidad = 0;
            createNormalCannonConCalidad();
        }

        public void createNormalCannonConCalidad()
        {
            calidadCooldown();
            calidadDaño();
            calidadImpulso();
            calcularCalentamiento();
        }
        //será [(0.1 * daño) / 2] * speed
        public void calcularCalentamiento()
        {
            calentamiento = (int)(((0.1 * dmg) / 2) * speed);

            //minimo 0.5 y maximo
        }

        public void calidadImpulso()
        {
            if (calidad <= 10)
            {
                if (Random.value <= 0.3) speed = Random.Range(41f, 80f);
                else if (Random.value <= 0.2) speed = Random.Range(10f, 40f);
                else speed = Random.Range(81f, 100f);
            }
            else if (calidad > 10 && calidad <= 20)
            {
                if (Random.value <= 0.5) speed = Random.Range(41f, 80f);
                else if (Random.value <= 0.3) speed = Random.Range(81f, 100f);
                else speed = Random.Range(10f, 40f);
            }
            else
            {
                if (Random.value <= 0.5) speed = Random.Range(10f, 40f);
                else if (Random.value <= 0.2) speed = Random.Range(81f, 100f);
                else speed = Random.Range(41f, 80f);
            }
            calidad += (speed / 10) * 1.5f;
            calidad = Mathf.Round(calidad);
        }

        //15 num magico
        public void calidadDaño()
        {
            if (calidad <= 5)
            {
                if (Random.value <= 0.1) dmg = Random.Range(11f, 15f);
                else if (Random.value <= 0.8) dmg = Random.Range(1f, 5f);
                else dmg = Random.Range(6f, 10f);
            }
            else if (calidad > 5 && calidad <= 10)
            {
                if (Random.value <= 0.5) dmg = Random.Range(6f, 10f);
                else if (Random.value <= 0.3) dmg = Random.Range(11f, 15f);
                else dmg = Random.Range(1f, 5f);
            }
            else
            {
                if (Random.value <= 0.5) dmg = Random.Range(1f, 5f);
                else if (Random.value <= 0.2) dmg = Random.Range(11f, 15f);
                else dmg = Random.Range(6f, 10f);
            }
            calidad += dmg;
        }

        public void calidadCooldown()
        {
            cooldown = Random.Range(0f, 1.5f);
            calidad = (cooldown * 10f) - 15;
            calidad = -calidad;
        }

        public void calcularCalidad()
        {
            calidad = (cooldown * 10f) - 15;
            calidad = -calidad;

            calidad += dmg;

            calidad += (speed / 10) * 1.5f;
            calidad = Mathf.Round(calidad);

        }

        public void setCannon(float cd, float d, int warm, float s)
        {
            this.cooldown = cd;
            this.dmg = d;
            this.calentamiento = warm;
            this.speed = s;

            calcularCalidad();
        }
    }
}
