using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercaderMunicion : MonoBehaviour
{
    public Transform[] dropItemsPos;
    private void Awake()
    {
        dropItemsPos = gameObject.GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        GenerateItems.dropMercaderMunicion(dropItemsPos);
    }
}
