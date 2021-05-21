using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Stat_UI : MonoBehaviour
{
    public Slider health;
    private RectTransform hp;
    public Slider mana;
    private RectTransform mp;
    public Slider stamina;
    private RectTransform sp;

    public GameObject text;
    public LoadLvls lvl;

    private void Awake()
    {
        hp = health.GetComponentInParent<RectTransform>();
        mp = mana.GetComponentInParent<RectTransform>();
        sp = stamina.GetComponentInParent<RectTransform>();
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
        if (health.value <= 0)
        {
            text.SetActive(true);
            StartCoroutine(LoadMainMenuAfterDealy());
            
        }
    }
    public void UpgradeHealth(int value)
    {
        hp.sizeDelta = new Vector2(hp.sizeDelta.x + value, hp.sizeDelta.y);
        SetMaxHealth((int)(hp.sizeDelta.x + value));
    }
    public void DowngradeHealth(int value)
    {
        hp.sizeDelta = new Vector2(hp.sizeDelta.x - value, hp.sizeDelta.y);
        SetMaxHealth((int)(hp.sizeDelta.x - value));
    }
    public void AddHealth(int value)
    {
        health.value += value;
    }
    public void SubHealth(int value)
    {
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
        mp.sizeDelta = new Vector2(mp.sizeDelta.x + value, mp.sizeDelta.y);
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
        sp.sizeDelta = new Vector2(sp.sizeDelta.x + value, sp.sizeDelta.y);
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
