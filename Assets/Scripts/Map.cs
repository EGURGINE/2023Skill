using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private MeshRenderer m;
    private Vector2 vec;
    [SerializeField] private float spd;
    void Update()
    {
        vec.y += Time.deltaTime * spd;
        m.material.SetTextureOffset("_MainTex", vec);
    }
}
