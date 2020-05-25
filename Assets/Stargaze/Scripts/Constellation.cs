using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Constellation : MonoBehaviour
{
    private Graph<Star> stars = new Graph<Star>(false, false);

    public Bounds starBounds;
    public uint numberOfStars;
    public GameObject starPrefab;

    void Awake()
    {
        Debug.Log(starBounds.extents);

        for (int ii = 0; ii < numberOfStars; ii++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(starBounds.min.x, starBounds.max.x),
                Random.Range(starBounds.min.y, starBounds.max.y),
                0.0f);

            GameObject instance = Instantiate(starPrefab, transform);
            instance.transform.localPosition = randomPoint;
        }

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
