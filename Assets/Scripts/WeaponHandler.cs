using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject LeftArm, RightArm;
    public float Force = 50f; 
    public Sprite Facing, Left, Right, Up; 
    public int Player;
    private float coolDown;
    // Start is called before the first frame update
    void Start()
    {

        coolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        coolDown -= Time.deltaTime; 
        if (Player == 1 && coolDown <= 0)
        {
            if (Input.GetButtonDown("Player 1 Up"))
            {
                LeftArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * Force, ForceMode2D.Impulse);
                RightArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * Force, ForceMode2D.Impulse);
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Up;
                Destroy(gameObject);
                coolDown = 0.5f; 

            }
            if (Input.GetButtonDown("Player 1 Left"))
            {
                LeftArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.left * Force, ForceMode2D.Impulse);
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Left;
                coolDown = 0.5f;

            }
            if (Input.GetButtonDown("Player 1 Right"))
            {
                RightArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.right * Force, ForceMode2D.Impulse);
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Right;
                coolDown = 0.5f;
            }
            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Facing;
            }


        }
        else
        {

        }
    }
}
