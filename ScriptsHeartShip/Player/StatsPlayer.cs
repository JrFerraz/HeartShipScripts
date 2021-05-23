using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPlayer : MonoBehaviour
{
    public static statsPlayer jugador;
    public GameObject deathGIF;
    public TextMeshProUGUI numAmmo;
    public TextMeshProUGUI controlTiempo;
    public TextMeshProUGUI comboCountText;

    public Image coinImage;
    public TextMeshProUGUI CoinCountText;

    public float vidaM = 10;
    public float escudoM = 10;
    public float armaduraM = 20;

    public float impulso1 = 10;
    public float daño1 = 10;
    public float cooldown1 = 20;
    public int calentamiento = 10;

    public float barreraMax;
    public float dañoBarrera;
    public float barreraCooldown;
    public float barreraRecuperar;
    public float barreraConsumo;

    public float calMax;
    public float disSCal;
    public float disCCal;

    public float dashCD;
    public float impulsoDash;
    public float vel;

    public float DMGimpacto;
    public float CDimpacto;
    public float WARMGimpacto;
    public float Vueloimpacto;

    public Canvas gameOver;
    float coolDownPararTiempo = 0;

    void Start()
    {
        jugador = new statsPlayer(vidaM, escudoM, armaduraM);
        jugador.setNormalCannon(impulso1, daño1, cooldown1, calentamiento);
        jugador.setBarrer(barreraCooldown, barreraMax, barreraRecuperar, barreraConsumo, dañoBarrera);
        jugador.setImpactShot(WARMGimpacto, DMGimpacto, Vueloimpacto, CDimpacto);
        jugador.setLASER(2f, 3f, 30f);
        jugador.setSpeedGenerator(impulsoDash, dashCD, vel);
        jugador.setCannon(calMax, disSCal, disCCal);
        FindObjectOfType<audioManager>().Play("GameTheme");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si es disparo enemigo llamo a hacerDaño
        if (collision.gameObject.layer == 9)
        {
            recibirDaño(ammoDamages.standardAmmo(jugador.escudo > 0, jugador.armadura > 0, 3f, jugador.vida, jugador.escudo, jugador.armadura, collision.gameObject.tag));
            FindObjectOfType<audioManager>().Play("ImpactHS");
        }
        if (collision.gameObject.layer == 14)
        {
            recibirDaño(ammoDamages.standardAmmo(jugador.escudo > 0, jugador.armadura > 0, 15f, jugador.vida, jugador.escudo, jugador.armadura, collision.gameObject.tag));
        }

        if (collision.gameObject.layer == 2)
        {
            getMoney();
            Destroy(collision.gameObject);
        }
    }

    void getMoney()
    {
        FindObjectOfType<audioManager>().Play("CoinSound");
        CoinCountText.gameObject.SetActive(true);
        coinImage.gameObject.SetActive(true);
        jugador.dinero++;
        Puntuaciones.DineroMaximo++;
        Invoke("desaparecerDineroInterface", 2f);
    }

    void desaparecerDineroInterface()
    {
        CoinCountText.gameObject.SetActive(false);
        coinImage.gameObject.SetActive(false);
    }

    void recibirDaño(float [] salud)
    {
        jugador.hit = true;
        jugador.vida = salud[0];
        jugador.armadura = salud[1];
        jugador.escudo = salud[2];
    }

    void morir()
    {
        if (jugador.vida <= 0)
        {
            Puntuaciones.puntos = jugador.numKills;
            Puntuaciones.mayorRacha = jugador.rachaMax;
            Puntuaciones.total = (jugador.numKills + jugador.rachaMax * 2);
            Puntuaciones.listarPuntuaciones((jugador.numKills + jugador.rachaMax * 2));
            if (!buttonActions.silenciarTodo)
            {
                FindObjectOfType<audioManager>().allVolModified(0.3f);
                FindObjectOfType<audioManager>().Play("Boom");
                FindObjectOfType<audioManager>().Play("GameOver");
                FindObjectOfType<audioManager>().slowSounding();
            }
            gameOver.gameObject.SetActive(true);
            jugador.vivo = false;
            Instantiate(deathGIF,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        engancharCombo();
        numAmmo.text = "Munición: " + StatsPlayer.jugador.cantidadMunEscogida;
        CoinCountText.text = jugador.dinero + "";
        morir();
        if(!InterfazGame.isPaused) pararTiempo();
        canvasTicTac();

        if (Input.GetKeyDown(KeyCode.M)) soltarLoot();
    }

    void soltarLoot()
    {
        Inventario.soltarItems(transform);
    }

    public class statsPlayer
    {
        public int dinero;
        //Salud
        public float vida;
        public float escudo;
        public float armadura;
        //SaludMax
        public float escudoMax;
        public float armaduraMax;
        public float vidaMax;
        //Rachas
        public int numKills;
        public int rachaMax;
        public int combo;
        public float comboTime;
        //MunLaser
        public int laserAmmo;
        public float vuelo_mun1;
        public bool vivo;
        public bool hit;
        //TipoMun
        public string munEscogida;
        public float cantidadMunEscogida;
        public float impulsoEscogido;
        public float dañoEscogido;
        public float coolDownEscogido;
        public float calentamientoEscogido;
        //ImpactMun
        public float munImpacto;
        //LASER 
        public float mun_LASER;

        public normalLaser.normalCannon miLaserNormal;
        public DropBarrera.Barrera miBarrera;
        public LASERDrop.LASER miLASER;
        public Cannon.CannonHeartship miCannon;
        public DropImpactAmmo.ImpactShot miImpactShot;
        public DropGeneradorVelocidad.GeneradorVelocidad miGeneradorVelocidad;

        public statsPlayer(float v, float e, float a)
        {
            dinero = 0;

            vidaMax = v;
            escudoMax = e;
            armaduraMax = a;

            escudo = escudoMax;
            vida = vidaMax;
            armadura = armaduraMax;

            laserAmmo = 20;
            numKills = 0;
            rachaMax = 0;
            vuelo_mun1 = 0.7f;
            mun_LASER = 10000;

            miLaserNormal = new normalLaser.normalCannon();
            miBarrera = new DropBarrera.Barrera();
            miLASER = new LASERDrop.LASER();
            miCannon = new Cannon.CannonHeartship();
            miImpactShot = new DropImpactAmmo.ImpactShot();
            miGeneradorVelocidad = new DropGeneradorVelocidad.GeneradorVelocidad();
            munImpacto = 20;

            escogerMun();

            vivo = true;
            hit = false;
        }

        void escogerMun()
        {
            munEscogida = "LaserShot";
            calentamientoEscogido = miLaserNormal.calentamiento;
            coolDownEscogido = miLaserNormal.cooldown;
            dañoEscogido = miLaserNormal.dmg;
            impulsoEscogido = miLaserNormal.speed;
            cantidadMunEscogida = laserAmmo;
        }

        public void setSpeedGenerator(float newDashAmount, float newCD, float newSpeed)
        {
            miGeneradorVelocidad.impulsoDash = newDashAmount;
            miGeneradorVelocidad.velocidadNave = newSpeed;
            miGeneradorVelocidad.cdDash = newCD;
            miGeneradorVelocidad.calcularCalidad();
        }

        public void setNormalCannon(float newImpulso, float newDmg, float newCooldown, int newCal)
        {
            miLaserNormal.speed = newImpulso;
            miLaserNormal.dmg = newDmg;
            miLaserNormal.cooldown = newCooldown;
            miLaserNormal.calentamiento = newCal;
            miLaserNormal.calcularCalidad();
        }

        public void setBarrer(float cd, float maxB, float velRec, float velCons, float dmg)
        {
            miBarrera.barreraMaxima = maxB;
            miBarrera.dañoBarrera = dmg;
            miBarrera.cooldownBarrera = cd;
            miBarrera.recuBarrera = velRec;
            miBarrera.consumoBarrera = velCons;
            miBarrera.calidadSencilla();
        }
        public void setLASER(float warm, float dmg, float d)
        {
            miLASER.daño_LASER = dmg;
            miLASER.consumoMun = d;
            miLASER.warm_LASER = warm;
            miLASER.calcularCalidad();
        }

        public void setImpactShot(float warm, float d, float imp, float cd)
        {
            miImpactShot.warm_munImpacto = warm;
            miImpactShot.daño_munImpacto = d;
            miImpactShot.impulso_munImpacto = imp;
            miImpactShot.cooldown_munImpacto = cd;
            miImpactShot.calcularCalidad();
        }
         
        public void setCannon(float max, float DnoS, float DsiS)
        {
            miCannon.calentamiento_max = max;
            miCannon.disipador_no_sobrecalentado = DnoS;
            miCannon.disipador_sobrecalentado = DsiS;
            miCannon.calcularCalidad();
        }
    }
    void canvasTicTac()
    {
        if (coolDownPararTiempo - Time.time > 0)
        {
            controlTiempo.text = "Slow: Recargando...";
        }
        else
        {
            controlTiempo.text = "Slow: ¡Listo!";
        }
        
    }

    void pararTiempo()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= coolDownPararTiempo)
        {
            FindObjectOfType<audioManager>().Play("SlowMotion");
            Time.timeScale = 0.7f;
            FindObjectOfType<audioManager>().slowSounding();
            FindObjectOfType<audioManager>().Play("TicTac");
            Invoke("flashTime",2f);
            coolDownPararTiempo = Time.time + 15f;
        }
    }
    void flashTime()
    {
        Time.timeScale = 1f;
        FindObjectOfType<audioManager>().Stop("TicTac");
        FindObjectOfType<audioManager>().normalSounding();
        FindObjectOfType<audioManager>().Play("SlowMotion");
    }
    void engancharCombo()
    {
        if (jugador.combo > jugador.rachaMax)
        {
            jugador.rachaMax = jugador.combo;
        }
        if (Time.realtimeSinceStartup >= jugador.comboTime)
        {
            jugador.combo = 0;
            comboCountText.gameObject.SetActive(false);
        }
        if (jugador.combo >= 2)
        {
            comboCountText.gameObject.SetActive(true);
        }
        if (jugador.combo % 2 == 0)
        {
            comboCountText.GetComponent<Animator>().SetTrigger("Kill");
            comboCountText.GetComponent<Animator>().ResetTrigger("OtherKill");
        }
        else
        {
            comboCountText.GetComponent<Animator>().SetTrigger("OtherKill");
            comboCountText.GetComponent<Animator>().ResetTrigger("Kill");
        }
        comboCountText.text = "Racha: ¡" + jugador.combo + "!";
    }
}
