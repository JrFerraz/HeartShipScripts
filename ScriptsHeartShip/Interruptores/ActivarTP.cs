using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarTP : MonoBehaviour
{
    public GameObject tp;
    bool oneSpawn = false;
    private void Update()
    {
        if (GetComponent<SpriteRenderer>().color == Color.green && !oneSpawn)
        {
            Instantiate(tp, new Vector3(transform.position.x + 3, transform.position.y + 14), Quaternion.identity);
            oneSpawn = true;
        }
    }
}
