using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateArm : MonoBehaviour
{
    public float force = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            //Destroy(gameObject);
            gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.left * force, ForceMode2D.Impulse);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Destroy(gameObject);
            gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.right * force, ForceMode2D.Impulse);

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Destroy(gameObject);
            gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Destroy(gameObject);
            gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.down * force, ForceMode2D.Impulse);

        }


    }
}
