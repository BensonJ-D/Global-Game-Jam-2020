using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public GameObject umbrella;
    public GameObject umbrellaOpen;


    public void Start()
    {
        umbrellaOpen.GetComponentInChildren<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        umbrellaOpen.GetComponentInChildren<PolygonCollider2D>().enabled = false;
        umbrellaOpen.GetComponentInChildren<Rigidbody2D>().simulated = false;


        //gameObject.GetComponentInChildren<HingeJoint2D>().connectedBody = instantiatedUmbrella.GetComponentInChildren<Rigidbody2D>(); 
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            umbrellaOpen.transform.position = umbrella.transform.position;
            umbrellaOpen.GetComponentInChildren<PolygonCollider2D>().enabled = true;
            umbrellaOpen.GetComponentInChildren<Rigidbody2D>().simulated = true;
            umbrella.GetComponentInChildren<PolygonCollider2D>().enabled = false;
            umbrella.GetComponentInChildren<Rigidbody2D>().simulated = false;
            umbrella.GetComponentInChildren<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            umbrella.GetComponentInChildren<PolygonCollider2D>().enabled = true;
            umbrella.GetComponentInChildren<Rigidbody2D>().simulated = true;
            umbrellaOpen.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            gameObject.GetComponentInChildren<HingeJoint2D>().connectedBody = umbrellaOpen.GetComponentInChildren<Rigidbody2D>(); 



            //gameObject.GetComponent<PolygonCollider2D>() = umbrellaOpen.GetComponentInChildren<PolygonCollider2D>(); 

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            umbrella.transform.position = umbrellaOpen.transform.position;
            umbrellaOpen.GetComponentInChildren<PolygonCollider2D>().enabled = false;
            umbrellaOpen.GetComponentInChildren<Rigidbody2D>().simulated = false;
            umbrella.GetComponentInChildren<PolygonCollider2D>().enabled = true;
            umbrella.GetComponentInChildren<Rigidbody2D>().simulated = true;
            umbrella.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            umbrella.GetComponentInChildren<PolygonCollider2D>().enabled = true;
            umbrella.GetComponentInChildren<Rigidbody2D>().simulated = true;
            umbrellaOpen.GetComponentInChildren<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            gameObject.GetComponentInChildren<HingeJoint2D>().connectedBody = umbrella.GetComponentInChildren<Rigidbody2D>(); 



            //gameObject.GetComponent<PolygonCollider2D>() = umbrellaOpen.GetComponentInChildren<PolygonCollider2D>(); 

        }
    }
}
