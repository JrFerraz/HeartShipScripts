using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitarFps : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
