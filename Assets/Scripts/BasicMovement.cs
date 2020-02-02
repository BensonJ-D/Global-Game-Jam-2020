using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public int jumps = 0;
    public int Player;
    private bool selfCorrecting;
    public GameObject SelfPrefab;


    private void FixedUpdate()
    {
        if (selfCorrecting)
        {
            transform.rotation = new Quaternion(0.5f, 44f, 0f, 0f);
        }
    }
    // Update is called once per frame
    void Update()
    {



        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position = new Vector2(transform.position.x + -0.03f, transform.position.y);

        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
        //    //transform.GetComponentInChildren<Rigidbody2D>().AddRelativeForce(Vector2.right * 5f);
        //}


        if (Player == 1)
        {
            transform.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(-Input.GetAxisRaw("Axis 11 Player 1") * 2f, 0f), ForceMode2D.Impulse);
            transform.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(-Input.GetAxisRaw("HorizontalPlayer1") * 2f, 0f), ForceMode2D.Impulse);

            
            //transform.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(0f, -Input.GetAxisRaw("Axis 12 Player 1")), ForceMode2D.Impulse);


            if (Input.GetButtonDown("Jump Player 1") && jumps > 0){
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 70f, ForceMode2D.Impulse);
                jumps--;
            }
            //if(Input.GetAxisRaw("Jump Player 1") >= 1f)
            //{
            //    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
            //    jumps--;
            //}

            if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 70, ForceMode2D.Impulse);
                jumps--;
            }
        }
        else
        {
            transform.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(-Input.GetAxisRaw("Axis 11 Player 2") * 2f, 0f), ForceMode2D.Impulse);
            transform.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(-Input.GetAxisRaw("HorizontalPlayer2") * 2f, 0f), ForceMode2D.Impulse);

            //transform.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(0f, -Input.GetAxisRaw("Axis 12 Player 2") * 2f), ForceMode2D.Impulse);

            if (Input.GetButtonDown("Jump Player 2") && jumps > 0)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 70f, ForceMode2D.Impulse);
                jumps--;

            }

            if (Input.GetKeyDown(KeyCode.RightCommand) && jumps > 0)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 70f, ForceMode2D.Impulse);
                jumps--;
            }
        }
        

        //transform.position = new Vector2(transform.position.x + (0.1f * Input.GetAxis("Axis 11")), transform.position.y);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            selfCorrecting = false;
            jumps = 2;
        }
       
    }

    private void OnCollisionExit(Collision collision)
    {
        selfCorrecting = true; 
    }

    private void OnBecameInvisible()
    {
        //Instantiate(SelfPrefab, new Vector2(0, 2f), transform.rotation);
        //Destroy(gameObject);
    }
}
