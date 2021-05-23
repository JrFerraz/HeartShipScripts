using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class zoomCamera : MonoBehaviour
{
    // Start is called before the first frame update
    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.m_Lens.OrthographicSize <= 10)
        {
            cam.m_Lens.OrthographicSize += 0.1f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.m_Lens.OrthographicSize >= 4)
        {
            cam.m_Lens.OrthographicSize -= 0.1f;
        }
    }
}
