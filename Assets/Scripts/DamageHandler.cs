using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour
{

    public Vector2 oldVelocity;
    //public GameObject body;

    public float damage = 0;    
    private float fudgeFactor = 40;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Vector2 newVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        oldVelocity.Set(newVelocity.x, newVelocity.y);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
{
        string otherHit = other.gameObject.tag;
        if(otherHit!=gameObject.tag && otherHit != "Platform") {
            Vector2 impulse = (gameObject.GetComponent<Rigidbody2D>().velocity - oldVelocity)*gameObject.GetComponent<Rigidbody2D>().mass;
            damage = damage+impulse.magnitude/gameObject.GetComponent<Rigidbody2D>().mass*fudgeFactor;

            gameObject.GetComponent<Rigidbody2D>().AddForce(impulse.normalized*damage);
        }
    }
}
