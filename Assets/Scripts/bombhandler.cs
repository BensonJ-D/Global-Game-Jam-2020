using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombhandler : MonoBehaviour
{
    public GameObject yeeter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.H)) {
            yeeter.GetComponent<HingeJoint2D>().enabled = false;
            yeeter.GetComponentInChildren<exploder>().thrown = true;
        }
    }
}
