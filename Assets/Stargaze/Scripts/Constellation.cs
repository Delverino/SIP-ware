using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Constellation : MonoBehaviour
{
    private Graph<Star> stars = new Graph<Star>(false, false);

    public Bounds bounds;

    void Awake()
    {
        bounds = GetComponent<BoxCollider>().bounds;

        foreach (Star star in GetComponentsInChildren<Star>())
        {
            Node<Star> node = stars.AddNode(star);
            star.index = node.Index;
        }
    }

    public List<Star> GetStars()
    {
        return stars.GetData();
    }

    public void AddEdge(Star from, Star to)
    {
        stars.AddEdge(stars.Nodes[from.index], stars.Nodes[to.index]);
    }

    public bool IsComplete()
    {
        return stars.IsConnected();
    }
}
