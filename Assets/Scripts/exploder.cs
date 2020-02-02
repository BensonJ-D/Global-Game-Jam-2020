using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exploder : MonoBehaviour
{
    public int lifetime = 100000;
    public bool thrown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(thrown == true) {
            lifetime--;
            if(lifetime<0) {
                
            }
        }

    }
}
