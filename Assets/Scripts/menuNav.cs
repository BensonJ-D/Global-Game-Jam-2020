using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuNav : MonoBehaviour
{
    public Camera camera;
    public List<Slider> p2;
    public List<Slider> p1;
    int p1current, p2current;
    bool waitingForPlayInput;
 
    // Start is called before the first frame update
    void Start()
    {
        p1current = 1;
        p2current = 1; //Select the middle slider first for both players

        waitingForPlayInput = true; //wait for an input from player 1
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForPlayInput) //Main menu waiting for player input
        {
            if ((Input.GetButtonDown("Player 1 Right")) || (Input.GetButtonDown("Player 1 Left")) || (Input.GetButtonDown("Player 1 Up")) || (Input.GetButtonDown("Player 1 Left")) || (Input.GetKeyDown("f"))) //any button press for player 1
            {
                camera.transform.Translate(0, 1080, 0);
                waitingForPlayInput = false;
            }
        }


        if (!waitingForPlayInput) { //If any button has been pressed

            //PLAYER 1 LEFT AND RIGHT
            if (Input.GetButtonDown("Player 1 Left")) //Go left in menu for P1
            {
                if (p1current != 0)//dont go too far
                {
                    p1current -= 1; //new selected (it's left to right (0->2)
                }
            }
            if (Input.GetButtonDown("Player 1 Right")) //Go right in menu for P1
            {
                if (p1current != 2) //dont go too far
                {
                    p1current += 1; //new selected (it's left to right (0->2)
                }
            }

            //PLAYER 2 LEFT AND RIGHT
            if (Input.GetButtonDown("Player 2 Left")) //Go left in menu for P2
            {
                if (p2current != 0)//dont go too far
                {
                    p2current -= 1; //new selected (it's left to right (0->2)
                }
            }
            
            if (Input.GetButtonDown("Player 1 Right")) //Go right in menu for P2
            {
                if (p2current != 2) //dont go too far
                {
                    p2current += 1; //new selected (it's left to right (0->2)
                }
            }

            //PLAYER 1 UP AND DOWN
            //if (Input.GetButtonDown("DOWN 1 Left"))
            //{
            //    if (p1[p1current].value != p1[p1current].maxValue) //dont go too far
            //    {
            //        p1[p1current].value += 1; //go down a val
            //    }
            //}
            if (Input.GetButtonDown("Player 1 Up"))
            {
                if (p1[p1current].value != p1[p1current].minValue) //dont go too far
                {
                    p1[p1current].value -= 1; //go up a val
                }
            }

            //PLAYER 2 UP AND DOWN
            //if (Input.GetButtonDown("DOWN player 2"))
            //{
            //    if (p2[p2current].value != p2[p2current].maxValue) //dont go too far
            //    {
            //        p2[p2current].value += 1; //go down a val
            //    }
            //}
            if (Input.GetButtonDown("Player 2 Up"))
            {
                if (p2[p2current].value != p2[p2current].minValue) //dont go too far
                {
                    p2[p2current].value -= 1; //go up a val
                }
            }

        }

    }
}
