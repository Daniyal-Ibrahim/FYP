using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HP : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    // Update is called once per frame

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void Damaged(int value)
    {
        curHealth -= value;

        if (curHealth <= 0)
        {
            Death();
        }
        else
        {
            Debug.Log("Damaged");
        }
    }
    public void Death()
    {
        Debug.Log("Dead");
        Destroy(this.gameObject);
    }
}
