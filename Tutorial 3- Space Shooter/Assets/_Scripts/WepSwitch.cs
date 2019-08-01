using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepSwitch : MonoBehaviour
{

    public int selectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon) 
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void WeaponSwitcher()
    {
        int i = 1;
        selectedWeapon = 1;
        foreach(Transform weapon in transform)
            {
                if( i == selectedWeapon)
                    weapon.gameObject.SetActive(false);
                else
                    weapon.gameObject.SetActive(true);
                i++;
            }
    }
}
