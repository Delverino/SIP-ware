using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Vector3 endPos;
    private bool moving = false;
    public float minOffset;
    public float maxOffset;

    // Start is called before the first frame update
    void Start()
    {
        // start with random model
        string cloudType = "voxcloud"+Random.Range(1,5);
        Mesh cloudMesh = Resources.Load<Mesh>("Voxels/"+cloudType);
        Material cloudMat = Resources.Load<Material>("Voxels/materials/mat_"+cloudType);
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = cloudMesh;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = cloudMat;

        // start with random offset
        endPos = transform.position;
        transform.Translate(Random.Range(minOffset,maxOffset)*(Random.Range(0,2)*2-1), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // lerp into place on command
        if (moving)
        {
            transform.Translate((endPos.x-transform.position.x)/8, 0, 0);
        }
    }

    // Call this via broadcast to make clouds move into place
    public void StartMoving()
    {
        moving = true;
    }
}
