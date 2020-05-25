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
    private uint activatedCount = 0;

    public Color completedLineColor;
    private Color initialLineColor;

    void Awake()
    {
        constellation = GetComponent<Constellation>();
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        line.SetPosition(line.positionCount - 1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        initialLineColor = line.startColor;
    }

    void FixedUpdate() {
        if (completed) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Star star = hit.transform.gameObject.GetComponent<Star>();
            if (star)
            {
                // For first star hovered, just add it as first point in the line
                if (!previous) 
                {
                    ActivateStar(star);
                }
                // For rest of stars, add point to line if star was not the previous star
                // Additionally, add edge to graph structure for checking completion
                else if(star != previous)
                {
                    constellation.AddEdge(previous, star);
                    ActivateStar(star);

                    // If this edge completes the constellation, give visual indication
                    if (constellation.IsComplete()) {
                        completed = true;
                        line.positionCount--;
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
        mousePos = transform.InverseTransformPoint(mousePos);
        SetLastLinePoint(mousePos);
    }

    private void ActivateStar(Star star)
    {
        SetLastLinePoint(star.transform.localPosition);
        line.positionCount++;

        star.Activate();
        activatedCount++;

        Color lineColor = Color.Lerp(initialLineColor, completedLineColor,
            (float)activatedCount / constellation.numberOfStars);
        line.startColor = lineColor;
        line.endColor = lineColor;
    }

    private void SetLastLinePoint(Vector3 point)
    {
        line.SetPosition(line.positionCount - 1, point);
    }
}
