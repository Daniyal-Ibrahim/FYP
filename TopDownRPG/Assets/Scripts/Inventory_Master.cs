using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]


public class Inventory_Master : ScriptableObject
{
    public Inventory Container;
    public Item_Database database;

    public bool AddItem(Item item, int amount)
    {

        if (EmptySlots <= 0) { return false; }

        InvetorySlot slot = FindItemOnInventory(item);
        if (!database.GetItem[item.ID].Stackable || slot == null)
        {
            SetEmptySlot(item, amount);
            return true;
        }
        slot.AddAmount(amount);
        return true;
    }
    public InvetorySlot FindItemOnInventory(Item item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.ID == item.ID)
            {
                return Container.Items[i];
            }
        }
        return null;

    }
    public int EmptySlots
    {
        get
        {
            int count = 0;
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].item.ID <= -1)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public InvetorySlot SetEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.ID <= -1)
            {
                Container.Items[i].UpdateSlot(item, amount);
                return Container.Items[i];
            }
        }
        // add code here for when inventory full
        return null;
    }

    public void SwapItem(InvetorySlot Swap1, InvetorySlot Swap2)
    {
        Debug.Log("item swap function called");
        if (Swap2.Placeable(Swap1.Item_Master) && Swap1.Placeable(Swap2.Item_Master))
        {
            Debug.Log("item swap working");
            InvetorySlot temp = new InvetorySlot(Swap2.item, Swap2.amount);
            Swap2.UpdateSlot(Swap1.item, Swap1.amount);
            Swap1.UpdateSlot(temp.item, temp.amount);
        }
    }

    public void DropItem(Item item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == item)
            {
                Container.Items[i].UpdateSlot(null, 0);
            }
        }
    }


    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}

[System.Serializable]
public class InvetorySlot
{
    public ItemType[] Allowed = new ItemType[0];
    [System.NonSerialized]
    public User_Interface parent;
    public Item item;
    public int amount;
    public string name;

    public Item_Master Item_Master
    {
        get
        {
            if (item.ID >= 0)
            {
                return parent.inventory.database.GetItem[item.ID];
            }
            return null;
        }
    }

    public InvetorySlot()
    {
        item = null;
        amount = 0;
    }
    public InvetorySlot( Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void UpdateSlot( Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void RemoveItem()
    {
        item = new Item();
        amount = 0;
    }

    public bool Placeable(Item_Master item)
    {
        if (Allowed.Length <= 0 || item == null || item.data.ID < 0)
        {
            return true;
        }

        for (int i = 0; i < Allowed.Length; i++)
        {
            if (item.type == Allowed[i])
            {
                return true;
            }
        }
        return false;
    }



}

[System.Serializable]
public class Inventory
{
    public InvetorySlot[] Items = new InvetorySlot[36];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(new Item(), 0);
        }
    }
    //public List<InvetorySlot> Items = new List<InvetorySlot>();
}

