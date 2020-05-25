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
    private bool completed = false;

    public Color completedLineColor;

    void Awake()
    {
        constellation = GetComponent<Constellation>();
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        line.SetPosition(line.positionCount - 1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void FixedUpdate() {
        if (completed) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.direction *= 100f;
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f)) {
            Star star = hit.transform.gameObject.GetComponent<Star>();
            if (star)
            {
                // For first star hovered, just add it as first point in the line
                if (!previous) 
                {
                    SetLastLinePoint(star.transform.localPosition);
                    line.positionCount += 1;
                    star.Activate();
                }
                // For rest of stars, add point to line if star was not the previous star
                // Additionally, add edge to graph structure for checking completion
                else if(star != previous)
                {
                    constellation.AddEdge(previous, star);
                    SetLastLinePoint(star.transform.localPosition);
                    line.positionCount += 1;
                    star.Activate();

                    // If this edge completes the constellation, give visual indication
                    if (constellation.IsComplete()) {
                        completed = true;
                        line.positionCount -= 1;
                        line.startColor = completedLineColor;
                        line.endColor = completedLineColor;
                        return;
                    }
                }
                previous = star;
            }
        }

        // Draw line to mouse position, so it looks like user is holding a thread connecting the stars
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.localPosition.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawLine(Camera.main.transform.position, mousePos, Color.cyan);
        mousePos = transform.InverseTransformPoint(mousePos);
        Debug.Log($"{Input.mousePosition}, {mousePos}");
        SetLastLinePoint(mousePos);
    }

    private void SetLastLinePoint(Vector3 point)
    {
        line.SetPosition(line.positionCount - 1, point);
    }
}
