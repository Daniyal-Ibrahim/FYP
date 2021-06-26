using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon_Activate : MonoBehaviour
{
    public GameObject weapon;
    public Animator animator;
    public void DamageStart()
    {
        weapon.SetActive(true);
    }

    private void Update()
    {
        if (animator.GetFloat("S_ID") == 0)
        {
            DamageEnd();
        }
    }
    public void DamageEnd()
    {
        weapon.SetActive(false);
    }
    /* NOTE
     * This was suppose to be used to only activate the weapons damage call during 
     * specific animation frames but always having it active seems more fun to play as 
     * so final decision pending
     * 
     */
}
