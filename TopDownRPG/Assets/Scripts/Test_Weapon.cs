using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon : MonoBehaviour
{
    public Inventroy_Manager manager;
    public int Damage;
    public bool isPlayer;

    

    public void OnEnable()
    {
        if (isPlayer)
        {
            Damage = manager.equiptment.Container.Items[10].item.properties[0].value;
        }
    }

    public void OnDisable()
    {
        if (isPlayer)
        {
            Damage = 0;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
       // if (other.CompareTag("Player") && other.isTrigger)
      //  {
            var pl = other.GetComponent<Test_Stat_UI>();
            if (pl)
            {
                pl.SubHealth(Damage);
            }            
       // }

        if (other.CompareTag("Enemy") && !this.CompareTag("Enemy") && other.isTrigger) 
        {
            var hp = other.GetComponent<Test_HP>();
            if (hp)
            {
                hp.Damaged(Damage);
            }
        }
        
    }


}
