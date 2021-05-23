using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public GameObject rielD;

    void frenar()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().Sleep();
    }

    public void detectarMuro()
    {

        RaycastHit2D hit = Physics2D.BoxCast(transform.position,transform.lossyScale, 13, rielD.transform.up);

        if (hit)
        {
            Debug.Log("SPAIN");
        }
        //try
        //{
        //    Vector2 vectorEnemigoJugador = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorEnemigoJugador, 1.5f);
        //    if (hit)
        //    {
        //        if (hit.transform.gameObject.layer == 15) bloqued = true;
        //    }
        //    else bloqued = false;
        //}
        //catch (System.Exception e) { };
    }
}
