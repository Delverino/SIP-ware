using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Constellation))]
[RequireComponent(typeof(LineRenderer))]
public class ConstellationDrawer : MonoBehaviour
{
    private Star previous = null;
    private Constellation constellation = null;
    private LineRenderer line = null;

    void Awake()
    {
        constellation = GetComponent<Constellation>();
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        line.SetPosition(line.positionCount - 1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Star star = hit.transform.gameObject.GetComponent<Star>();
            if (star)
            {
                // For first star hovered, just add it as first point in the line
                if (!previous) 
                {
                    SetLastLinePoint(star.transform.position);
                    line.positionCount += 1;
                }
                // For rest of stars, add point to line if star was not the previous star
                // Additionally, add edge to graph structure for checking completion
                else
                {
                    if (star != previous) 
                    {
                        constellation.AddEdge(previous, star);
                        SetLastLinePoint(star.transform.position);
                        line.positionCount += 1;

                        // If this edge completes the constellation, give visual indication
                        if (constellation.IsComplete()) {
                            line.startColor = Color.blue;
                            line.endColor = Color.blue;
                        }
                    }
                }
                star.Activate();
                previous = star;
            }
        }

        // Draw line to mouse position, so it looks like user is holding a thread connecting the stars
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        SetLastLinePoint(mousePos);
    }

    private void SetLastLinePoint(Vector3 point)
    {
        line.SetPosition(line.positionCount - 1, point);
    }
}
