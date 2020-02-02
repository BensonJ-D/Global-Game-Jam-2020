using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour
{
    //WeaponHandler weaponHandler;
    public Vector2 oldVelocity;
    //public GameObject body;
    public float healThreshold = 1.0f;
    public float damage = 0;
    float clangCooldown = 0;
    public AudioSource clang; 

    private float fudgeFactor = 110;

    // Start is called before the first frame update
    void Start()
    {
        //weaponHandler = transform.GetComponentInParent<WeaponHandler>();
        
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
        clangCooldown -= Time.deltaTime;
        Vector2 newVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        oldVelocity.Set(newVelocity.x, newVelocity.y);
        int mylayer = transform.gameObject.layer; 
        String namedLayer = LayerMask.LayerToName(mylayer);

        if (namedLayer == "Player2" && Input.GetButton("Player2 Heal") && newVelocity.magnitude < healThreshold)
        {
            damage -= 5f;
            if (damage < 0.0f)
            {
                damage = 0.0f;
            }
            //weaponHandler.GrabWeapon(damage);
        }


        if (namedLayer == "Player1" && Input.GetButton("Player1 Heal") && newVelocity.magnitude < healThreshold)
        {
            damage -= 5f;
            if (damage < 0.0f)
            {
                damage = 0.0f;
            }
            //weaponHandler.GrabWeapon(damage);
        }
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
            if (clangCooldown < 0)
            {
                clang.pitch = (UnityEngine.Random.Range(0f, 1f)); 
                clang.Play();
                clangCooldown = 0.5f;
            }
            Vector2 impulse = (gameObject.GetComponent<Rigidbody2D>().velocity - oldVelocity)*gameObject.GetComponent<Rigidbody2D>().mass;
            if (impulse.x >= 1f || impulse.y >= 1f)
            {
                damage += impulse.magnitude / gameObject.GetComponent<Rigidbody2D>().mass * fudgeFactor;
                gameObject.GetComponent<Rigidbody2D>().AddForce(impulse.normalized * damage);
            }

            //weaponHandler.WeaponBreak(damage);
        }
    }
}
