using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    bool hovering = false;

    public float downScalar;
    public float response;

    Vector3 initScale;
    Vector3 downScale;
    Vector3 targetScale;

    public List<AudioClip> pops;
    public AudioSource popper;

    public GameObject particles;
    int clicks;

    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        targetScale = initScale;
        downScale = initScale * downScalar;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            targetScale = initScale;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, response);
    }

    private void OnMouseDown()
    {
        transform.localScale = initScale;
        popper.clip = pops[Random.Range(0, pops.Count)];
        GameObject.Instantiate(particles, transform.position, Quaternion.identity);
        if (!popper.isPlaying)
        {
            popper.Play();
        }
    }

    private void OnMouseDrag()
    {
        targetScale = downScale;
    }
}
