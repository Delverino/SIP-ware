using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Constellation constellation;
    public int index { get; set; } = -1;

    void Awake()
    {
        constellation = GetComponentInParent<Constellation>();
    }

    public void Activate()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh)
        {
            mesh.material.color = Color.blue;
        }
    }
}
