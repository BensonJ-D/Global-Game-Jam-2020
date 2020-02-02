using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCustomiseOverseer : MonoBehaviour
{

    // 1_playerOneSkin, 2_PlayerTwoSkin,
    public Color[] skinData = new Color[2];

    // 1_PlayerOneRW, 2_PlayerOneLW, 3_PlayerTwoRW, 4_PlayerTwoLW;
    public weapon[] weaponData = new weapon[4];

    public weapon[] weaponList;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void recieveSkins(Color skin, int index)
    {
        Debug.Log("SETTING: "+ skin + "   " +"["+index+"]");
        skinData[index-1] = skin;
    }

    public void recieveWeapons(weapon weapon, int index)
    {
        Debug.Log("SETTING: " + weapon + "   " + "[" + index + "]");
        weaponData[index - 3] = weapon;
    }

}

[System.Serializable]
public class weapon
{
    public bool disabled;
    public string name;
    public Sprite sprite;
    public GameObject prefab;
}