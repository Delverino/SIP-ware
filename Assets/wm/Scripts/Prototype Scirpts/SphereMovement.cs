using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public int zVel = 8;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, zVel * Time.deltaTime,0);
    }
}
