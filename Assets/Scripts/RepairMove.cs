using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairMove : MonoBehaviour
{
    public GameObject body;

    public DamageHandler damageHandler;

    // Start is called before the first frame update
    void Start()
    {

    }   

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Y) && damageHandler.damage > 0) {
            damageHandler.damage = damageHandler.damage - 2;
        }
    }
}
