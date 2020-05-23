using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    LineRenderer line;

    Vector3[] points;

    Vector3 start;

    public float grain;
    public float length;

    public float min;
    public float max;

    public GameObject mouse;

    int numPoints;

    public int mult;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        start = Vector3.Lerp(Vector3.zero, mouse.transform.position, 0.2f);

        //length = 10 * Mathf.Abs(mouse.transform.position.x);

        numPoints = (int)(length / grain);
        points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float x = i * grain * mult;// * Mathf.Sign(mouse.transform.position.x);
            float y = Mathf.Sin(  (x * Mathf.Clamp(6 / Mathf.Abs(mouse.transform.position.x), min, max) ) )* mouse.transform.position.y;
            points[i] = new Vector3(x, y + start.y, 0);
        }

        line.positionCount = numPoints;
        line.SetPositions(points);
    }
}
