using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMap : MonoBehaviour
{
    [SerializeField] private Vector3 startY;
    [SerializeField] private float spd;

    private void Update()
    {
        transform.Translate(Vector3.down * spd * Time.deltaTime);
        if (transform.position.y <= -24.3) transform.position = startY;
    }

}
