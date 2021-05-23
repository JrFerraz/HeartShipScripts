using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactShot : MonoBehaviour
{
    //public ParticleSystem impact;
    [SerializeField] GameObject impact = null;
    //public GameObject impact;
    GameObject[] tiros; 
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
        {
            CameraShake.Instance.ShakeCamera(10f, .1f);
            if(collision.gameObject.layer != 15) collision.gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 60f;
            if(collision.gameObject.layer == 17 || collision.gameObject.tag.Equals("Pinchos")) collision.gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
            Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<audioManager>().Play("ImpactShotSound");
        }

    }

    void Update()
    {
        Destroy(gameObject, 1f);
    }
}
