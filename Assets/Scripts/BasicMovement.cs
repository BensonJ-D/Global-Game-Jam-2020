using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    bool onGround = false;
    int jumps = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x + -0.03f, transform.position.y);
            //transform.GetComponentInChildren<Rigidbody2D>(). (new Vector2(transform.position.x + -0.1f, transform.position.y * 10f), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + 0.03f, transform.position.y);
            //transform.GetComponentInChildren<Rigidbody2D>().AddRelativeForce(Vector2.right * 5f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumps>0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 60, ForceMode2D.Impulse);
            jumps--;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            onGround = true;
            jumps = 2;
        }
       
    }
}
