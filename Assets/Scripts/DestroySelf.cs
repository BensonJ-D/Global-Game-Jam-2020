using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    public GameObject me;
    public int lifetime = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime--;
        if(lifetime<0) {
            GameObject.Destroy(me);
        }
    }
}