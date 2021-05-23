using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        collision.gameObject.GetComponent<Rigidbody2D>().Sleep();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        collision.gameObject.GetComponent<Rigidbody2D>().Sleep();
    }
}
