using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public int lifetime = 100;
    public bool thrown = false;

    public GameObject explosion;
    public GameObject bomb;

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
                var myExplosion = Instantiate(explosion, bomb.transform);
                myExplosion.transform.SetParent(null);
                thrown = false;
                lifetime = 100;
            }
        }

    }
}
