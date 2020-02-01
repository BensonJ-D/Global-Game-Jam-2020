using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootyShoot : MonoBehaviour
{

    public GameObject booleet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
           GameObject b = Instantiate(booleet, gameObject.transform.position, transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = -transform.right * 10;
        }
    }
}
