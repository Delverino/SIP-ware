using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollider : MonoBehaviour
{
    private WareManager wm;
    public bool success;

    private void Start()
    {
        wm = GameObject.FindGameObjectWithTag("GameController").GetComponent<WareManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (success)
        {
            Debug.Log("Winner");
            wm.Success();
        }
        else
        {
            Debug.Log("Loser");
            wm.Failure();
        }
    }
}
