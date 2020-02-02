using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicators : MonoBehaviour
{
    public Text healthTxt1, healthTxt2, player1LivesTxt, player2LivesTxt;
    public GameObject player1, player2;
    public int player1Lives, player2Lives;
    public float player1RightLimbHealth; 
    private void Start()
    {
        player1RightLimbHealth = 100f;
        player1Lives = 3;
        player2Lives = 3;

    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {

        //player1 = GameObject.Find("PlayerOneBody");
        //player2 = GameObject.Find("PlayerTwoBody");
        player1LivesTxt.text = player1.GetComponentInChildren<PlayerHealth>().PlayerLives + "";
        player2LivesTxt.text = player2.GetComponentInChildren<PlayerHealth>().PlayerLives + "";
        healthTxt1.text = player1.GetComponentInChildren<DamageHandler>().damage + "%";
        healthTxt2.text = player2.GetComponentInChildren<DamageHandler>().damage + "%";


    }

    
}
