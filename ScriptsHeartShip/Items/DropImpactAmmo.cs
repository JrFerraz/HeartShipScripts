using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropImpactAmmo : MonoBehaviour
{
    public ImpactShot miImpacto;
    float parada = 0f;
    public bool manualGeneration = false;

    public float impulso;
    public float dmg;
    public float cd;
    private void Awake()
    {
        miImpacto = new ImpactShot();
        parada = transform.position.y;

        if (manualGeneration) miImpacto.setImpactAmmo(dmg,cd,impulso);
    }
    void Start()
    {
        colorRareza();
    }
    private void FixedUpdate()
    {
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
                if (StatsPlayer.jugador.miImpactShot.calidad < miImpacto.calidad) StatsPlayer.jugador.setImpactShot(miImpacto.warm_munImpacto, miImpacto.daño_munImpacto, miImpacto.impulso_munImpacto, miImpacto.cooldown_munImpacto);
                else Inventario.misImpactShots.Add(gameObject);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }

            if (gameObject.transform.parent != null && StatsPlayer.jugador.dinero >= gameObject.transform.parent.GetComponent<ComprarObjetos>().calidad)
            {
                StatsPlayer.jugador.setImpactShot(miImpacto.warm_munImpacto, miImpacto.daño_munImpacto, miImpacto.impulso_munImpacto, miImpacto.cooldown_munImpacto);
                Destroy(gameObject);
            }


        }
    }

    public void colorRareza()
    {
        if (miImpacto.calidad < 125) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else if (miImpacto.calidad >= 125 && miImpacto.calidad < 200) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (miImpacto.calidad >= 200 && miImpacto.calidad < 250) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }


    public class ImpactShot
    {
        public float impulso_munImpacto;
        public float warm_munImpacto;
        public float daño_munImpacto;
        public float cooldown_munImpacto;
        public float calidad;

        public ImpactShot()
        {
            calcularImpulso();
            calcularDaño();
            calcularCooldown();
            calcularWarm();
            calcularCalidad();
        }

        public void calcularCalidad()
        {
            calidad = impulso_munImpacto;
            calidad += daño_munImpacto * 10;
            calidad += 100 - (cooldown_munImpacto * 20);
        }

        void calcularImpulso()
        {
            float drop = Random.Range(0, 100);

            if (drop <= 20) impulso_munImpacto = Random.Range(60f, 100f);
            else if (drop <= 30) impulso_munImpacto = Random.Range(30f, 60f);
            else impulso_munImpacto = Random.Range(10f, 30f);
        }

        void calcularDaño()
        {
            float drop = Random.Range(0,100);

            if (impulso_munImpacto <= 30)
            {
                if (drop <= 30) daño_munImpacto = Random.Range(7f, 10f);
                else if (drop > 30 && drop <= 60) daño_munImpacto = Random.Range(4f, 7f);
                else daño_munImpacto = Random.Range(1f, 4f);
            }
            else if (impulso_munImpacto > 30 && impulso_munImpacto <= 60)
            {
                if (drop <= 20) daño_munImpacto = Random.Range(7f, 10f);
                else if (drop > 20 && drop <= 50) daño_munImpacto = Random.Range(4f, 7f);
                else daño_munImpacto = Random.Range(1f, 4f);
            }
            else
            {
                if (drop <= 15) daño_munImpacto = Random.Range(7f, 10f);
                else if (drop > 15 && drop <= 50) daño_munImpacto = Random.Range(4f, 7f);
                else daño_munImpacto = Random.Range(1f, 4f);
            }
        }
        
        void calcularCooldown()
        {
            float drop = Random.Range(0, 100);

            if (daño_munImpacto >= 7)
            {
                if (drop <= 10) cooldown_munImpacto = Random.Range(0.1f, 1.5f);
                else if (drop > 10 && drop <= 50) cooldown_munImpacto = Random.Range(1.5f, 3f);
                else cooldown_munImpacto = Random.Range(3f, 5f);
            }
            else if (daño_munImpacto < 7 && daño_munImpacto >= 4)
            {
                if (drop <= 20) cooldown_munImpacto = Random.Range(1f, 1.5f);
                else if (drop > 20 && drop <= 60) cooldown_munImpacto = Random.Range(1.5f, 3f);
                else cooldown_munImpacto = Random.Range(3f, 5f);
            }
            else
            {
                if (drop <= 30) cooldown_munImpacto = Random.Range(1f, 1.5f);
                else if (drop > 30 && drop <= 70) cooldown_munImpacto = Random.Range(1.5f, 3f);
                else cooldown_munImpacto = Random.Range(3f, 5f);
            }
        }

        void calcularWarm()
        {
            warm_munImpacto = (int)(((0.1 * daño_munImpacto) / 2) * impulso_munImpacto) * 1.5f;
        }

        public void setImpactAmmo(float dmg, float cd, float speed)
        {
            this.daño_munImpacto = dmg;
            this.cooldown_munImpacto = cd;
            this.impulso_munImpacto = speed;
            calcularWarm();
            calcularCalidad();
        }
    }
}
