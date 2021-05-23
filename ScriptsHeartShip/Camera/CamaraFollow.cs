using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform player;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 temp = transform.position;
        if (player != null)
        {
            temp.x = player.transform.position.x;
            temp.y = player.transform.position.y;
            transform.position = temp;
        }
    }
}
