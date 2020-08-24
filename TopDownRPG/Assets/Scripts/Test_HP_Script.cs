using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HP_Script : MonoBehaviour
{
    public Test_HP _HP;
    public int health;
    private void Awake()
    {
        _HP.maxHealth = health;
        _HP.curHealth = health;
    }
}
