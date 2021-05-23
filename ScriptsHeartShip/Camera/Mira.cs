using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
    public GameObject mira;
    public GameObject Player;
    private Vector3 target;
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        AIM();
    }
    void AIM()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        mira.transform.position = new Vector2(target.x, target.y);
    }
}
