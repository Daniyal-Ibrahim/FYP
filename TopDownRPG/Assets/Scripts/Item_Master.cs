using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    Default,
    Key_Items,
    Consumables,
    Tools,
    Primary,
    Secondary,
    Rings,
    Head_Piece,
    Chest_Piece,
    Arm_Piece,
    Leg_Piece,
    Spell_book,
    Bag,
}

public enum Property
{
    Attack,
    Defence,

    Heath,
    Mana,

    Speed,
}
public abstract class Item_Master : ScriptableObject
{
    public ItemType type;
    public Sprite icon;
    [TextArea(15, 20)]
    public string description;
    public bool Stackable;
    public Item data = new Item();

    public Item CreateItem() { Item newItem = new Item(this); return newItem; }

}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID = -1;
    public ItemProperties[] properties;
    public bool Stackable;

    public Item()
    {
        Name = "";
        ID = -1;
    }   

    public Item(Item_Master item)
    {
        Name = item.name;
        ID = item.data.ID;
        Stackable = item.Stackable;
        properties = new ItemProperties[item.data.properties.Length];
        for (int i = 0; i < properties.Length; i++)
        {
            properties[i].property = item.data.properties[i].property;
        }
    }
}

[System.Serializable]
public class ItemProperties
{
    public Property property;
    public int value;
}
