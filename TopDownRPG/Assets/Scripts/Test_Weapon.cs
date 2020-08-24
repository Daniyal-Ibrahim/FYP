using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon : MonoBehaviour
{
    public int Damage;
    public void OnTriggerEnter(Collider other)
    {

        var hp = other.GetComponent<Test_HP>();
        if (hp)
        {
            hp.Damaged(Damage);
        }
    }
}
