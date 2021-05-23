using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarTP : MonoBehaviour
{
    // Start is called before the first frame update

    float test = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        revelarMunicion();
    }

    void revelarMunicion()
    {
        if (test <= 1)
        {
            GetComponent<Renderer>().material.SetFloat("_Fade", test);
            GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            test += 0.7f * Time.deltaTime;
        }
    }
}
