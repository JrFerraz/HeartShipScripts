using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Verdin : MonoBehaviour
{
    //public statsMarcianito marcianito;

    public BasicEnemy verdin;

    //Movimiento
    public float lineOfSite;
    public float speed;
    Transform player;
    //Disparo
    public float shootingRange;
    public GameObject bulletParent;
    public GameObject bullet;
    public float fireRate = 1f;
    float nextFireTime;

    private bool bloqued;

    void Start()
    {
        bloqued = false;
        try
        {
            player = GameObject.Find("Player").transform;
        }
        catch (System.Exception e) { };

        //marcianito = new statsMarcianito();
        
        Invoke("darTiempo",1f);
    }

    void darTiempo()
    {
        GetComponent<BasicEnemy>().statsEnemigo.stunned = false;
    }
    void Update()
    {
        if (!GetComponent<BasicEnemy>().statsEnemigo.stunned)
        {
            MoveAndShoot();
        }
    }
    void MoveAndShoot()
    {
        detectarBloqueo();
        if (player != null && !bloqued)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time && StatsPlayer.jugador.vivo)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                FindObjectOfType<audioManager>().Play("AlienShot");
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
    void detectarBloqueo()
    {
        //aqui puede petar si player muere
        try
        {
            Vector2 vectorEnemigoJugador = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorEnemigoJugador, 1.5f);
            if (hit)
            {
                if (hit.transform.gameObject.layer == 15) bloqued = true;
            }
            else bloqued = false;
        }
        catch (System.Exception e) { };
    }
}
