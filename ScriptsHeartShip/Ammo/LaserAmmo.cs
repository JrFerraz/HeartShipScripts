using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserAmmo : MonoBehaviour
{
    public GameObject tiro;
    public GameObject GO0;
    public Transform GO1;
    public Transform GO2;
    public GameObject explosion;
    float bulletForce = 20f;
    bool ratata = false;

    float test = 0;
    private bool restartAnim = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            StatsPlayer.jugador.laserAmmo += 20;
            Destroy(gameObject);
            FindObjectOfType<audioManager>().Play("GetAmmo");
        }
        //si disparas que explote   
        if (collision.gameObject.layer == 11 && !ratata)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
            ratata = true;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Invoke("bum", 0.2f);
            FindObjectOfType<audioManager>().Play("ExplodeAmmo");
        }

        if (collision.gameObject.layer == 14 && !ratata)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            ratata = true;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Invoke("bum", 0.2f);
            FindObjectOfType<audioManager>().Play("ExplodeAmmo");
        }
    }
    void bum()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        Rotar();
        resaltarLoot();
    }

    void resaltarLoot()
    {
        revelarMunicion();
        ocultarMunicion();
    }

    void revelarMunicion()
    {
        if (test <= 1)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", test);
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            test += 0.7f * Time.deltaTime;

            if (test >= 0.9f)
            {
                restartAnim = true;
            }
        }
    }

    void ocultarMunicion()
    {
        if (restartAnim)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", test);
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            test -= 1.5f * Time.deltaTime;
            if (test <= 0)
            {
                restartAnim = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (ratata)
        {
            Shoot();
        }
    }
    void Rotar()
    {
        GO0.transform.Rotate(new Vector3(0f, 0f, -2000f) * Time.deltaTime);
        GO1.transform.Rotate(new Vector3(0f, 0f, 2000f) * Time.deltaTime);
        GO2.transform.Rotate(new Vector3(0f, 0f, 1500f) * Time.deltaTime);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(tiro, GO0.transform.position, GO0.transform.rotation);
        GameObject bullet2 = Instantiate(tiro, GO1.transform.position, GO1.transform.rotation);
        GameObject bullet3 = Instantiate(tiro, GO2.transform.position, GO2.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rb1 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet3.GetComponent<Rigidbody2D>();
        rb.AddForce(GO0.transform.up * bulletForce, ForceMode2D.Impulse);
        rb1.AddForce(GO1.transform.up * bulletForce, ForceMode2D.Impulse);
        rb2.AddForce(GO2.transform.up * bulletForce, ForceMode2D.Impulse);
    }
}
