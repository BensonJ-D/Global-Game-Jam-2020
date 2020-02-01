using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicators : MonoBehaviour
{
    public Text healthTxt1, healthTxt2, player1LivesTxt, player2LivesTxt, player1RightLimbTxt, player1LeftLimbTxt, player2RightLimbTxt, player2LeftLimbTxt;
    public GameObject player1, player2, player1RightLimb;
    public int player1Lives, player2Lives;
    public float player1RightLimbHealth; 
    private void Start()
    {
        player1RightLimbHealth = 100f;
        player1RightLimbTxt.text = player1RightLimbHealth + "%";
        player1Lives = 5;
        player2Lives = 5;

    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        player1RightLimbHealth -= player1RightLimb.GetComponentInChildren<DamageHandler>().damage;
        player1RightLimbTxt.text = player1RightLimbHealth + "%";
        //player1 = GameObject.Find("PlayerOneBody");
        //player2 = GameObject.Find("PlayerTwoBody");
        healthTxt1.text = player1.GetComponentInChildren<DamageHandler>().damage + "%";
        healthTxt2.text = player2.GetComponentInChildren<DamageHandler>().damage + "%";
        player2LivesTxt.text = player2Lives + "";
        player1LivesTxt.text = player1Lives + "";


    }
}
