using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitch : MonoBehaviour
{
    public float waitTime;
    public float pitchSpeed;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine( waitToThrow() );
    }

    IEnumerator waitToThrow()
    {
        yield return new WaitForSeconds(waitTime);
        body.velocity = Vector3.left * pitchSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("winzone"))
        {
            body.velocity = Vector3.zero;
            Debug.Log("you win!");
        }
    }
}
