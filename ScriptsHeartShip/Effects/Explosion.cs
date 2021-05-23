using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool cargado = false;

    private void Start()
    {
        if (gameObject.tag.Equals("explosion")) Destroy(gameObject, 0.3f);
        else
        {
            CameraShake.Instance.ShakeCamera(20f, .1f);
            FindObjectOfType<audioManager>().Play("ExplMarciano");
            Destroy(gameObject, 0.6f);
        }
    }
}
