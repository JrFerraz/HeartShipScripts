using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disparar : MonoBehaviour
{
    //puntos de tiro
    public Transform firePoint;
    public Transform firePoint2;
    //tipoTiro
    public GameObject bulletPrefab;
    public GameObject impactShot;
    private GameObject tiroElegido;
    //velocidad tiro
    public float bulletForce = 80f;
    //Cooldown
    float start_refresh = 0;
    public Image laserCoolDownGet;
    public static Image laserCoolDown;

    public static bool sobrecalentado = false;
    bool canShoot = true;
    bool dispara = false;

    GameObject bullet;
    GameObject bullet2;


    int indexCD;
    private List<float> cooldowns;
    private void Awake()
    {
        laserCoolDown = laserCoolDownGet;
        cooldowns = new List<float>();
        cooldowns.Add(0);
        cooldowns.Add(0);
    }

    void Update()
    {
        fijarCD();
        if(!StatsPlayer.jugador.munEscogida.Equals("Laser")) gestionCoolDown();
    }
    void gestionCoolDown()
    {
        typeAmmo();
        //si se sobrecalienta o se queda sin municion...
        if (laserCoolDown.fillAmount >= 0.98)
        {
            sobrecalentado = true;
        }
        if (StatsPlayer.jugador.cantidadMunEscogida == 0)
        {
            canShoot = false;
        }
        if (StatsPlayer.jugador.cantidadMunEscogida > 0 && !sobrecalentado)
        {
            canShoot = true;
        }
        if (StatsPlayer.jugador.cantidadMunEscogida > 0 && sobrecalentado)
        {
            canShoot = false;
        }
        // si no puede disparar y el cooldown se acaba y tiene municion...
        if (!canShoot && laserCoolDown.fillAmount <= 0 && StatsPlayer.jugador.cantidadMunEscogida > 0)
        {
            canShoot = true;
            sobrecalentado = false;
        } // tiro
        else if (Input.GetMouseButtonDown(0) && canShoot && Time.realtimeSinceStartup >= cooldowns[indexCD])
        {
            //Shoot();
            dispara = true;
        }
        //se enfría con el tiempo si el cooldown es maximo se reducirá mas deprisa, pero mientras no llegue a cero no se puede disparar
        if (canShoot && Time.realtimeSinceStartup >= start_refresh)
        {
            enfriarCannon();
        } 
        if(!canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<audioManager>().Play("ReloadLaser");
            }
            enfriarCannonCalentado();
        }
    }

    private void FixedUpdate()
    {
        if (dispara) Shoot();
    }

    void fijarCD() 
    {
        if (StatsPlayer.jugador.munEscogida.Equals("LaserShot")) indexCD = 0;
        else if (StatsPlayer.jugador.munEscogida.Equals("ImpactShot")) indexCD = 1;
    }
    void Shoot()
    {
        CameraShake.Instance.ShakeCamera(1.3f, 0.04f);
        bullet = Instantiate(tiroElegido, firePoint.position, firePoint.rotation);
        bullet2 = Instantiate(tiroElegido, firePoint2.position, firePoint2.rotation);
        //aqui hacer una variable más en la que poner el tipo de impulso elegido
        bullet.GetComponent<Rigidbody2D>().AddForce((firePoint.up) * StatsPlayer.jugador.impulsoEscogido, ForceMode2D.Impulse);
        bullet2.GetComponent<Rigidbody2D>().AddForce(firePoint2.up * StatsPlayer.jugador.impulsoEscogido, ForceMode2D.Impulse);
        //gastarMun
        gastarMunicion();
        //InterfazLaser
        calentarCannon();
        //SonidoLaser
        //FindObjectOfType<audioManager>().Play("TiroLaser");
        dispararSonido();
        cooldowns[indexCD] = Time.realtimeSinceStartup + StatsPlayer.jugador.coolDownEscogido;
        dispara = false;
        start_refresh = Time.realtimeSinceStartup + 0.5f;
    }

    public static void calentarCannon()
    {
        laserCoolDown.fillAmount += (StatsPlayer.jugador.calentamientoEscogido / StatsPlayer.jugador.miCannon.calentamiento_max);

    }

    public static void enfriarCannon()
    {
        laserCoolDown.fillAmount -= (StatsPlayer.jugador.miCannon.disipador_no_sobrecalentado / StatsPlayer.jugador.miCannon.calentamiento_max) * Time.deltaTime;
        if (laserCoolDown.fillAmount <= 0) sobrecalentado = false;
    }

    public static void enfriarCannonCalentado()
    {
        laserCoolDown.fillAmount -= (StatsPlayer.jugador.miCannon.disipador_sobrecalentado / StatsPlayer.jugador.miCannon.calentamiento_max) * Time.deltaTime;
        if (laserCoolDown.fillAmount <= 0) sobrecalentado = false;
    }
    public static void calentarCannonPorUpdate()
    {
        laserCoolDown.fillAmount += (StatsPlayer.jugador.calentamientoEscogido / StatsPlayer.jugador.miCannon.calentamiento_max) * Time.deltaTime;
        if (laserCoolDown.fillAmount >= 0.98f) sobrecalentado = true;
    }

    void dispararSonido()
    {
        if (StatsPlayer.jugador.munEscogida.Equals("LaserShot")) FindObjectOfType<audioManager>().Play("TiroLaser");
        else if (StatsPlayer.jugador.munEscogida.Equals("ImpactShot")) FindObjectOfType<audioManager>().Play("ImpactoSound");
    }

    void gastarMunicion()
    {
        if (StatsPlayer.jugador.munEscogida.Equals("LaserShot")) StatsPlayer.jugador.laserAmmo--;
        else if (StatsPlayer.jugador.munEscogida.Equals("ImpactShot")) StatsPlayer.jugador.munImpacto--;
    }

    void typeAmmo()
    {
        if (StatsPlayer.jugador.munEscogida.Equals("LaserShot")) tiroElegido = bulletPrefab;
        else if (StatsPlayer.jugador.munEscogida.Equals("ImpactShot")) tiroElegido = impactShot;
    }
}
