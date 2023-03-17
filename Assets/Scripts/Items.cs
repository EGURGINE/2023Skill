using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private GameObject imageObj;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        imageObj = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        imageObj.transform.Rotate(0, 0, 3);
        rb.velocity = Vector3.down;
    }
}
