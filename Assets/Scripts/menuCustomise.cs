using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCustomise : MonoBehaviour
{
    public Slider slider;
    public int val;
    public int index;
    public menuCustomiseOverseer overseer;
    public Image childSprite;
    public bool isSkin;
    Color skinCol;
    public List<weapon> weaponListSimplified = new List<weapon>();

    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        childSprite = this.transform.Find("selection").GetComponent<Image>();
        for (int i = 0; i < overseer.weaponList.Length; i++)
        {
            //Debug.Log("checking weapon" + i);
            if (overseer.weaponList[i].disabled != true)
            {
                weaponListSimplified.Add(overseer.weaponList[i]);
                //Debug.Log("adding weapon" + i);
            }
        }
        if (!isSkin)
        {
            slider.maxValue = weaponListSimplified.Count-1;
            childSprite.sprite = weaponListSimplified[val].sprite;
            //Debug.Log(weaponListSimplified.Count);
            overseer.recieveWeapons(weaponListSimplified[val], index);

        }
        else
        {
            skinCol = Color.HSVToRGB((Random.Range(0.1f,0.5f)), 0.8f, 1);
            childSprite.color = skinCol;
            overseer.recieveSkins(skinCol, index);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(val != slider.value) //OnValueChange
        {
          //  Debug.Log(slider.value);
            val = (int)slider.value;
            //overseer.recieveData(val, gameObject.name, index);

            //Do smart things here
            if (isSkin){
                skinCol = Color.HSVToRGB((val+1)/slider.maxValue, 0.5f, 1);
                childSprite.color = skinCol;
                // Debug.Log("RGB:" + skinCol + "   HSV:" + (val + 1) / slider.maxValue + ", 80, 100");
                overseer.recieveSkins(skinCol, index);

            }
            else //if weapon {}
            {
                childSprite.sprite = weaponListSimplified[val].sprite;
                overseer.recieveWeapons(weaponListSimplified[val], index);
            }


        }
    }
}
