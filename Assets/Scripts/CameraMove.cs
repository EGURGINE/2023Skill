using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 setPos = new Vector3(0, 0, -10);
    [SerializeField] private GameObject player;
    [SerializeField] private float spd;

    void Update()
    {
        ObjMove();
    }

    private void ObjMove()
    {
        transform.position = Vector3.Lerp(transform.position , player.transform.position + setPos,Time.deltaTime * spd);
    }
}
