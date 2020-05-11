using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndShoot : MonoBehaviour
{
    public float xVel = 1f;
    public int edge = 10;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            xVel *= -1;
        }

        transform.position += new Vector3(xVel * Time.deltaTime, 0, 0);

        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(projectile,transform.position,transform.rotation);
        }
    }
}
