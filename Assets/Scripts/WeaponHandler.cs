using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject LeftArm, RightArm;
    public float Force = 100000f; 
    public Sprite Facing, Left, Right, Up; 
    public int Player;
    private float coolDown;
    enum AttackDirection
    {
        left, right, up, facing
    };
    private AttackDirection attackDirection; 


    // Start is called before the first frame update
    void Start()
    {
        coolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {

        coolDown -= Time.deltaTime;

        if(coolDown <= 0)
        {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Facing;
        }else
        {
            switch (attackDirection)
            {
                case AttackDirection.left:
                    LeftArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.left * Force, ForceMode2D.Impulse);
                    break;
                case AttackDirection.right:
                    RightArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.right * Force, ForceMode2D.Impulse);
                    break;
                case AttackDirection.up:
                    LeftArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * Force /2 , ForceMode2D.Impulse);
                    RightArm.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * Force /2 , ForceMode2D.Impulse);
                    break; 
            }
        }

        if (Player == 1 && coolDown <= 0)
        {
            if (Input.GetButtonDown("Player 1 Up"))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Up;
                coolDown = 0.2f;
                attackDirection = AttackDirection.up;

            }
            if (Input.GetButtonDown("Player 1 Left"))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Left;
                attackDirection = AttackDirection.left;
                coolDown = 0.2f;
            }
            if (Input.GetButtonDown("Player 1 Right"))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Right;
                coolDown = 0.2f;
                attackDirection = AttackDirection.right;

            }

        }
        else
        {

        }
    }

    private void FixedUpdate()
    {
        
    }
}
