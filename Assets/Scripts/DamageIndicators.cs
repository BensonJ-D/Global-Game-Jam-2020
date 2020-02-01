using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicators : MonoBehaviour
{
    public Text healthTxt1, healthTxt2;
    public GameObject player1, player2; 
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        healthTxt1.text = player1.GetComponentInChildren<DamageHandler>().damage + "%";
        healthTxt2.text = player2.GetComponentInChildren<DamageHandler>().damage + "%";


    }
}
