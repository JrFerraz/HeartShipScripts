using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactShotAmmo : MonoBehaviour
{
    public GameObject tiro;
    public GameObject explosion;

    bool ratata = false;
    private float bulletForce = 20f;

    private float controlShader = 0;
    private bool restartAnim = false;

    Transform[] dispersion;

    Transform rotator;
    private void Awake()
    {
        dispersion = GetComponentsInChildren<Transform>();
        rotator = transform.Find("Rotador");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            StatsPlayer.jugador.munImpacto += 20;
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
            Destroy(collision.gameObject);
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

    // Update is called once per frame
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
        if (controlShader <= 1)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", controlShader);
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            controlShader += 0.7f * Time.deltaTime;

            if (controlShader >= 0.9f)
            {
                restartAnim = true;
            }
        }
    }

    void ocultarMunicion()
    {
        if (restartAnim)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", controlShader);
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            controlShader -= 1.5f * Time.deltaTime;
            if (controlShader <= 0)
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
        rotator.Rotate(new Vector3(0f, 0f, 1000f) * Time.deltaTime);
    }
    void Shoot()
    {

        for (int i = 0; i < dispersion.Length; i++)
        {
            pegarTiro(i);
        }
    }

    void pegarTiro(int pos)
    {
        GameObject bullet = Instantiate(tiro, dispersion[pos].transform.position, dispersion[pos].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(dispersion[pos].transform.up * bulletForce, ForceMode2D.Impulse);
    }
}
