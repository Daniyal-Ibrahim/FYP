using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Stats : MonoBehaviour
{
    public int Str, Dex, Vit, Int, Wis, Atu, Lvl, Exp, HP, AV, DV;

    public Test_Stat_UI stat_UI;

    public void Awake()
    {
        HP = Vit;
    }

    public int AbilityBonus(int stat)
    {
        int bonus = ((stat - 10) / 2);
        return bonus;
    }
}
