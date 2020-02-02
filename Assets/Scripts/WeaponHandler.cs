using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private LimbsHandler[] arms;
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
        arms = transform.GetComponentsInChildren<LimbsHandler>();
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
            if (Input.GetButtonDown("Player 2 Up"))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Up;
                coolDown = 0.2f;
                attackDirection = AttackDirection.up;

            }
            if (Input.GetButtonDown("Player 2 Left"))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Left;
                attackDirection = AttackDirection.left;
                coolDown = 0.2f;
            }
            if (Input.GetButtonDown("Player 2 Right"))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Right;
                coolDown = 0.2f;
                attackDirection = AttackDirection.right;

            }

        }
    }

    private void FixedUpdate()
    {
        
    }

    public void WeaponBreak(float damage)
    {
        LimbsHandler[] equippedArms = System.Array.FindAll(arms, c => c.HasWeapon());

        if (damage > 1500f * (3 - equippedArms.Length))
        {
            Debug.Log("Break risk!");
            float percentage = (damage - 1500) / 5f;

            Debug.Log(percentage);
            bool limbBreak = Random.Range(0.0f, 100.0f) < percentage;

            if (limbBreak)
            {
                Debug.Log("Breaking!");
                int limb = (int)(Random.Range(0.0f, 100.0f) / 50f);

                Debug.Log(limb);
                Debug.Log(arms.Length);
                if (arms[limb].HasWeapon())
                {
                    Debug.Log("Weapon Dropped!");
                    arms[limb].DropWeapon();

                    switch (limb)
                    {
                        case 0:
                            RightArm = arms[limb].gameObject;
                            break;
                        case 1:
                            LeftArm = arms[limb].gameObject;
                            break;
                        default:
                            break;
                    }
                            
                }
            }
        }
    }


    //public void GrabWeapon(float damage)
    //{
    //    LimbsHandler[] limbs = System.Array.FindAll(arms, c => !c.HasWeapon());

    //    if (damage < 2000f * (limbs.Length))
    //    {
    //        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
    //        GameObject[] looseWeapons = System.Array.FindAll(weapons, c => c.layer == LayerMask.NameToLayer("Default"));

    //        foreach(GameObject weapon in looseWeapons)
    //        {
    //            if((weapon.transform.position - transform.position).magnitude < 1.5f)
    //            {
    //                foreach(LimbsHandler limb in limbs)
    //                {

    //                }
    //            }
    //        }
    //    }
    //}
}
