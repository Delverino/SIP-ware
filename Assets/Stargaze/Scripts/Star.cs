using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
    public int index { get; set; } = -1;
    private Constellation constellation;

    private static float animationDuration = 0.7f;
    private static float animationScale = 1.5f;

    private Sequence animationSequence;
    private Sequence scaleSequence;

    void Awake()
    {
        constellation = GetComponentInParent<Constellation>();

        scaleSequence = DOTween.Sequence();
        scaleSequence.Append(transform.DOScale(transform.localScale * animationScale, animationDuration / 2f));
        scaleSequence.Append(transform.DOScale(transform.localScale, animationDuration / 2f));
        Debug.Log(scaleSequence.Duration());

        animationSequence = DOTween.Sequence();
        animationSequence.Append(scaleSequence);
        animationSequence.Join(transform.DORotate(new Vector3(0.0f, 0.0f, 360.0f), animationDuration, RotateMode.LocalAxisAdd));
        Debug.Log(animationSequence.Duration());
    }

    public void Activate()
    {
        Debug.Log($"Star {index} activated");

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh)
        {
            mesh.material.color = Color.blue;
        }

        animationSequence.Play();
    }
}
