using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Item1,
    Item2,
    Item3,
    Item4,
    End
}
public class Items : MonoBehaviour
{
    private GameObject imageObj;
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void Start()
    {
        imageObj = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        imageObj.transform.Rotate(0, 5, 0);
        rb.velocity = Vector3.down;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            Destroy(gameObject);
        }
    }
}
