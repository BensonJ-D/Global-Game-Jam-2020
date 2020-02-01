using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector2(transform.position.x + (0.1f * Input.GetAxis("Axis 11")), transform.position.y);

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x + -0.1f, transform.position.y);
            //transform.GetComponentInChildren<Rigidbody2D>(). (new Vector2(transform.position.x + -0.1f, transform.position.y * 10f), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
            //transform.GetComponentInChildren<Rigidbody2D>().AddRelativeForce(Vector2.right * 5f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);

        }


    }
}
