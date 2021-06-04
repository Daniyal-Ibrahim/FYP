using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    public Test_Stats player_Stats;

    public TextMeshProUGUI Str;
    public TextMeshProUGUI Dex;
    public TextMeshProUGUI Vit;
    public TextMeshProUGUI Int;
    public TextMeshProUGUI Wis;
    public TextMeshProUGUI Atu;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Dodge;

    private void Awake()
    {
        player_Stats = GameObject.Find("Player").GetComponent<Test_Stats>();

    }

    private void Update()
    {
        UpdateStats();
    }
    void UpdateStats()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(player_Stats.Str).Append(" ").Append("(").Append(player_Stats.AbilityBonus(player_Stats.Str)).Append(")");
        Str.text = builder.ToString(); builder.Clear();

        builder.Append(player_Stats.Dex).Append(" ").Append("(").Append(player_Stats.AbilityBonus(player_Stats.Dex)).Append(")");
        Dex.text = builder.ToString(); builder.Clear();

        builder.Append(player_Stats.Vit).Append(" ").Append("(").Append(player_Stats.AbilityBonus(player_Stats.Vit)).Append(")");
        Vit.text = builder.ToString(); builder.Clear();

        builder.Append(player_Stats.Int).Append(" ").Append("(").Append(player_Stats.AbilityBonus(player_Stats.Int)).Append(")");
        Int.text = builder.ToString(); builder.Clear();

        builder.Append(player_Stats.Wis).Append(" ").Append("(").Append(player_Stats.AbilityBonus(player_Stats.Wis)).Append(")");
        Wis.text = builder.ToString(); builder.Clear();

        builder.Append(player_Stats.Atu).Append(" ").Append("(").Append(player_Stats.AbilityBonus(player_Stats.Atu)).Append(")");
        Atu.text = builder.ToString(); builder.Clear();

        /*
        builder.Append(player_Stats.AV).Append(" ").Append(player_Stats.AbilityBonus(player_Stats.AV));
        Armor.text = builder.ToString(); builder.Clear();

        builder.Append(player_Stats.DV).Append(" ").Append(player_Stats.AbilityBonus(player_Stats.DV));
        Dodge.text = builder.ToString(); builder.Clear();
        */
    }


}
