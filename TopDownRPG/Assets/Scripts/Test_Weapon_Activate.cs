using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon_Activate : MonoBehaviour
{
    public GameObject weapon;
    public void DamageStart()
    {
        weapon.SetActive(true);
    }
    public void DamageEnd()
    {
        weapon.SetActive(false);
    }
}
