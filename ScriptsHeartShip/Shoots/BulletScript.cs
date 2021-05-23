using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        try
        {
            target = GameObject.Find("Player");
            Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
            bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
            Destroy(this.gameObject, 5);
        }
        catch (System.Exception e)
        {

        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
