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
}
/*
public enum Rarity
{
    Common,     // White
    Uncommon,   // Green
    Rare,       // Blue
    Epic,       // Purple
    Legendary,  // Golden
} */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public abstract class Item_Master : ScriptableObject
{
    public ItemType type;
    public Sprite icon;
    public string itemName;
    [TextArea(15, 15)]
    public string description;
    public bool Stackable;
    public GameObject spawnPrefab;
    public ItemProperties[] itemProperties;
    public Item data = new Item();
    public Item CreateItem() { Item newItem = new Item(this); return newItem; }
}

[System.Serializable]
public class Item
{
    public ItemType Type;
    public float itemX, itemY, itemZ;
    public string Name;
    public int ID = -1;
    public ItemProperties[] properties;
    public bool Stackable;
    public GameObject prefab;
    public string description;
    public Item()
    {
        Name = "";
        ID = -1;   
    }
    public Item(Item_Master item)
    {
        Type = item.type;
        Name = item.itemName;
        ID = item.data.ID;
        Stackable = item.Stackable;
        prefab = item.spawnPrefab;
        description = item.description;
        prefab.GetComponent<Item_PickUp>().pickUp = item;
        // create a check or item hash to prevent same item from getting a diff value when u repick it 
        properties = new ItemProperties[item.data.properties.Length];
        for (int i = 0; i < properties.Length; i++)
        {
            properties[i] = new ItemProperties(item.data.properties[i].min, item.data.properties[i].max)
            {
                property = item.data.properties[i].property
            };
        }
    }
}

    [System.Serializable]
    public class ItemProperties
    {
        public Property property;
        public int value;

        
        public int min;
        public int max;
        public ItemProperties(int _min, int _max)
        {
            min = _min;
            max = _max;
            GenerateValue();
        }

        public void AddValue(ref int baseValue)
        {
            baseValue += value;
        }

        public void GenerateValue()
        {
            value = UnityEngine.Random.Range(min, max);
        }
        
    }