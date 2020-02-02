using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuilder : MonoBehaviour
{
    public GameObject[] WeaponsLeft, WeaponsRight;
    int weaponSelectedLeft, weaponSelectedRight;
    public GameObject SelectionArrows;
    bool makeselection;
    public GameObject LeftArm, RightArm;
    private int player;
    enum CurrentlySelecting
    {
        rightArm, leftArm
    }
    CurrentlySelecting currentlySelecting;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<BasicMovement>().Player;
        currentlySelecting = CurrentlySelecting.leftArm;
        //gameObject.GetComponentInChildren<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Time.timeScale = 0;
        weaponSelectedLeft = 0;
        makeselection = true;
        currentlySelecting = CurrentlySelecting.leftArm;

    }

    // Update is called once per frame
    void Update()
    {
        if(weaponSelectedLeft > WeaponsLeft.Length)
        {
            weaponSelectedLeft = WeaponsLeft.Length;
        }else if(weaponSelectedLeft < 0)
        {
            weaponSelectedLeft = 0; 
        }

        if (weaponSelectedRight > WeaponsRight.Length)
        {
            weaponSelectedRight = WeaponsRight.Length;
        }
        else if (weaponSelectedRight < 0)
        {
            weaponSelectedRight = 0;
        }


        if (currentlySelecting == CurrentlySelecting.leftArm)
        {
            try
            {
                WeaponsLeft[weaponSelectedLeft].SetActive(true);
                LeftArm.GetComponentInChildren<HingeJoint2D>().connectedBody = WeaponsLeft[weaponSelectedLeft].GetComponentInChildren<Rigidbody2D>();
                gameObject.GetComponentInChildren<WeaponHandler>().LeftArm = WeaponsLeft[weaponSelectedLeft];
                gameObject.GetComponentInChildren<WeaponHandler>().LeftWeaponJoint.connectedBody = WeaponsLeft[weaponSelectedLeft].GetComponent<Rigidbody2D>();
                for (int i = 0; i < WeaponsLeft.Length; i++)
                {
                    if (i != weaponSelectedLeft)
                    {

                        WeaponsLeft[i].SetActive(false);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {

            }
        }else if(currentlySelecting == CurrentlySelecting.rightArm)
        {
            try
            {
                WeaponsRight[weaponSelectedRight].SetActive(true);
                RightArm.GetComponentInChildren<HingeJoint2D>().connectedBody = WeaponsRight[weaponSelectedRight].GetComponentInChildren<Rigidbody2D>();
                gameObject.GetComponentInChildren<WeaponHandler>().RightArm = WeaponsRight[weaponSelectedRight];
                gameObject.GetComponentInChildren<WeaponHandler>().RightWeaponJoint.connectedBody = WeaponsRight[weaponSelectedRight].GetComponent<Rigidbody2D>();
                for (int i = 0; i < WeaponsRight.Length; i++)
                {
                    if (i != weaponSelectedRight)
                    {
                        WeaponsRight[i].SetActive(false);
                    }
                }
            }
            catch(IndexOutOfRangeException e)
            {

            }
        }

        if (makeselection)
        {
            
            Debug.Log(currentlySelecting); 
            if(currentlySelecting == CurrentlySelecting.leftArm)
            {
                if (Input.GetAxisRaw(String.Format("Axis 11 Player {0}", player)) < 0)
                {
                    SelectionArrows.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                    currentlySelecting = CurrentlySelecting.rightArm;

                }

                if (Input.GetAxisRaw(String.Format("Axis 11 Player {0}", player)) > 0)
                {
                    SelectionArrows.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                    currentlySelecting = CurrentlySelecting.leftArm;
                }
                if (Input.GetAxisRaw(String.Format("Axis 12 Player {0}", player)) < 0)
                {
                    if (weaponSelectedLeft + 1 < WeaponsLeft.Length)
                    { 
                        weaponSelectedLeft++;
                    }
                    //SelectionArrows.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                }

                if (Input.GetAxisRaw(String.Format("Axis 12 Player {0}", player)) > 0)
                {
                    //SelectionArrows.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                    if (weaponSelectedLeft - 1 < WeaponsLeft.Length)
                    {
                        weaponSelectedLeft--;

                    }
                }
                makeselection = false;
            }else if(currentlySelecting == CurrentlySelecting.rightArm)
            {
                if (Input.GetAxisRaw(String.Format("Axis 11 Player {0}", player)) < 0)
                {
                    SelectionArrows.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                    currentlySelecting = CurrentlySelecting.rightArm;

                }

                if (Input.GetAxisRaw(String.Format("Axis 11 Player {0}", player)) > 0)
                {
                    SelectionArrows.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                    currentlySelecting = CurrentlySelecting.leftArm;
                }
                if (Input.GetAxisRaw(String.Format("Axis 12 Player {0}", player)) < 0)
                {
                    if (weaponSelectedLeft + 1 < WeaponsLeft.Length)
                    {
                        weaponSelectedRight++;
                    }
                    //SelectionArrows.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                }

                if (Input.GetAxisRaw(String.Format("Axis 12 Player {0}", player)) > 0)
                {
                    //SelectionArrows.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                    if (weaponSelectedLeft - 1 < WeaponsLeft.Length)
                    {
                        weaponSelectedRight--;
                    }
                }
                makeselection = false;

            }


            if (Input.GetButtonDown("Player 1 Right"))
            {
                Time.timeScale = 1;
                Destroy(SelectionArrows);
                Destroy(GetComponentInChildren<CharacterBuilder>()); 
            }
        }

        if(Input.GetAxisRaw(String.Format("Axis 11 Player {0}", player)) == 0f && Input.GetAxisRaw(String.Format("Axis 12 Player {0}", player)) == 0f)
        {
            makeselection = true; 
        } 
        



    }


}
