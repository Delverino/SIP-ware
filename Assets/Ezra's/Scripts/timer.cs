using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("In seconds")]
    public float length;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > length)
        {
            LevelManager.Instance.done();
        }
    }
}
