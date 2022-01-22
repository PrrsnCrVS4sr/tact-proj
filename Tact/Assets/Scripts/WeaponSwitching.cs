using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    

    int selectedWeapon = 0;
    int previousSelectedWeapon;
    // Update is called once per frame
    void Update()
    {
        previousSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            if(selectedWeapon >= transform.childCount-1)
            {
                selectedWeapon =0;
            }
            else
            {
                selectedWeapon ++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            if(selectedWeapon<=0)
            {
                selectedWeapon = transform.childCount -1;
            }
            else
            {
                selectedWeapon --;
            }
        }


        if(selectedWeapon != previousSelectedWeapon)
        {
            SwitchWeapon();
        }
    }

    private void SwitchWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(selectedWeapon == i)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
