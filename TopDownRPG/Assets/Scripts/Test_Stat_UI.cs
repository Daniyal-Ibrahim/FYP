using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;


public class Test_Stat_UI : MonoBehaviour
{
    public Test_Stats player_Stats;

    public Slider health;
    private RectTransform hp;

    public Slider exp;
    private RectTransform ep;

    public TextMeshProUGUI text_HP;
    public TextMeshProUGUI text_EXP;

    public int hp_current;
    public int hp_max;

    public TextMeshProUGUI player_lvl_text;
    public int player_lvl;

    public int exp_current;
    public int exp_max;

    public Slider mana;
    private RectTransform mp;
    public Slider stamina;
    private RectTransform sp;

    public GameObject text;
    public LoadLvls lvl;

    private void Start()
    {
        hp = health.GetComponentInParent<RectTransform>();
        mp = mana.GetComponentInParent<RectTransform>();
        sp = stamina.GetComponentInParent<RectTransform>();
        hp_current = hp_max = player_Stats.HP ;
        exp_current = player_Stats.Exp;
        player_lvl = player_Stats.Lvl;
        exp_max = 100;
        exp.maxValue = exp_max;

    }


    public void setValueExp(int lvl)
    {
        exp.minValue = exp_max;
        exp_max = 15 * Mathf.RoundToInt((Mathf.Pow(lvl,3)));
    }

    IEnumerator LoadMainMenuAfterDealy()
    {
        yield return new WaitForSecondsRealtime(2f);
        text.SetActive(false);
        //UpgradeHealth(1);
        lvl.LoadMainMenu();
    }
    private void Update()
    {
        hp_current = player_Stats.HP;
        exp_current = player_Stats.Exp;
        exp.value = exp_current;

        health.value = hp_current;

        StringBuilder HpBuilder = new StringBuilder();
        HpBuilder.Append(hp_current).Append(" / ").Append(hp_max);
        //hp_curr_text.text = hp_current.ToString();
        //hp_max_text.text = hp_max.ToString();
        text_HP.text = HpBuilder.ToString();

        player_lvl_text.text = player_lvl.ToString();

        StringBuilder ExpBuilder = new StringBuilder();
        if (player_lvl == 20)
        {
            ExpBuilder.Append("MAX");
            exp_current = exp_max;
            exp.value = exp_current;

            //exp_curr_text.text = exp_current.ToString();
            //exp_max_text.text = exp_max.ToString();
            text_EXP.text = ExpBuilder.ToString();
        }
        else
        {
            ExpBuilder.Append(exp_current).Append(" / ").Append(exp_max);
            //exp_curr_text.text = exp_current.ToString();
            //exp_max_text.text = exp_max.ToString();
            text_EXP.text = ExpBuilder.ToString();
        }
        //setValueExp(player_lvl);
        

        if ((exp_current == exp_max || exp_current > exp_max ) && player_lvl < 20)
        {
            //exp_current = 0;
            player_Stats.Lvl += 1;
            player_lvl = player_Stats.Lvl;
            setValueExp(player_lvl+1);
            exp.maxValue = exp_max;
            //player_Stats.Exp = 0;
            hp_max += (((player_Stats.Vit - 10) / 2) + Mathf.RoundToInt(Random.Range(1, 8)));
            health.maxValue = hp_max;
            player_Stats.HP = hp_max;
        }


        if (health.value <= 0)
        {
            text.SetActive(true);
            //StartCoroutine(LoadMainMenuAfterDealy());
        }
        else
            text.SetActive(false);
    }
    public void UpgradeHealth(int value)
    {
        //hp.sizeDelta = new Vector2(hp.sizeDelta.x + value, hp.sizeDelta.y);
        SetMaxHealth((int)(hp.sizeDelta.x + value));
    }
    public void DowngradeHealth(int value)
    {
        //hp.sizeDelta = new Vector2(hp.sizeDelta.x - value, hp.sizeDelta.y);
        SetMaxHealth((int)(hp.sizeDelta.x - value));
    }
    public void AddHealth(int value)
    {
        if (player_Stats.HP + value >= hp_max)
        {
            player_Stats.HP = hp_max;
            hp_current = hp_max;
            health.value = hp_max;
        }
        else
        {
            player_Stats.HP += value;
            hp_current += value;
            health.value += value;
        }
    }
    public void SubHealth(int value)
    {
        player_Stats.HP -= value;
        hp_current -= value;
        health.value -= value;
    }
    public void SetMaxHealth(int value)
    {
        health.maxValue = value;
        health.value = health.maxValue;
    }
    public void SetHeatlth(int value) 
    {
        health.value = value;
    }

    public void UpgradeMana(int value)
    {
        //mp.sizeDelta = new Vector2(mp.sizeDelta.x + value, mp.sizeDelta.y);
        SetMaxMana((int)(mp.sizeDelta.x + value));
    }
    public void AddMana(int value)
    {
        mana.value += value;
    }
    public void SubMana(int value)
    {
        mana.value -= value;
    }
    public void SetMaxMana(int value)
    {
        mana.maxValue = value;
        mana.value = mana.maxValue;
    }
    public void SetMana(int value)
    {
        mana.value = value;
    }

    public void UpgradeStamina(int value)
    {
        //sp.sizeDelta = new Vector2(sp.sizeDelta.x + value, sp.sizeDelta.y);
        SetMaxStamina((int)(sp.sizeDelta.x + value));
    }
    public void AddStamina(int value)
    {
        stamina.value += value;
    }
    public void SubStamina(int value)
    {
        stamina.value -= value;
    }
    public void SetMaxStamina(int value)
    {
        stamina.maxValue = value;
        stamina.value = stamina.maxValue;
    }
    public void SetStamina(int value)
    {
        stamina.value = value;
    }
}
