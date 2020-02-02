using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArms : MonoBehaviour
{

    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.J)) {
            body.GetComponent<RelativeJoint2D>().angularOffset = 0f;
        }
                if(Input.GetKey(KeyCode.K)) {
            body.GetComponent<RelativeJoint2D>().angularOffset = 50f;
        }
    }
}
