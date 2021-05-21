using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.IO;

public enum InterfaceType
{
    Inventory,
    Equipment,
    Chest
}
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory_Master : ScriptableObject
{
    public Inventory Container;
    public User_Interface_Static UIStatic;
    public Item_Database database;
    public GameObject SpawnPoint;
    // implementing saveing and loading inventory
    public string savePath;
    public InvetorySlot[] GetItems {get { return Container.Items; }}

    public bool AddItem(Item item, int amount)
    {
        InvetorySlot slot = FindItemOnInventory(item);
        if (EmptySlotsCount <= 0) 
        {

            if (database.Items[item.ID].Stackable)
            {
                slot.AddAmount(amount);
                return true;
            }
            else
                return false; 
        }

        
        if (!database.Items[item.ID].Stackable || slot == null)
        {
            //for(int i =0; i < amount; i++) { 
            SetEmptySlot(item, amount);
            return true;
        }
        slot.AddAmount(amount);
        return true;
    }

    public InvetorySlot FindItemOnInventory(Item item)
    {
        for (int i = 0; i < GetItems.Length; i++)
        {
            if (GetItems[i].item.ID == item.ID)
            {
                return GetItems[i];
            }
        }
        return null;

    }

    public int EmptySlotsCount
    {
        get
        {
            int count = 0;
            for (int i = 0; i < GetItems.Length; i++)
            {
                if (GetItems[i].item.ID <= -1)
                {
                    count++;
                }
            }
            return count;
        }
    }
    public void AutoEquip_UseItem(InvetorySlot slot1)
    {
        GameObject obj = GameObject.Find("EquiptmentSlotHolder");

        UIStatic = obj.GetComponent(typeof(User_Interface_Static)) as User_Interface_Static;
        if (slot1.item.Type.ToString() == "Consumables")
        {
            Debug.Log("this item is a Consumable" + slot1.name);
            Inventroy_Manager manager = new Inventroy_Manager();
            manager.UseConsumeable(slot1);

        }
        
    }
    public InvetorySlot SetEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < GetItems.Length; i++)
        {
            if (GetItems[i].item.ID <= -1)
            {
                GetItems[i].UpdateSlot(item, amount);
                return GetItems[i];
            }
        }
        // add code here for when inventory full
        return null;
    }

    public void SwapItem(InvetorySlot Swap1, InvetorySlot Swap2)
    {
        //Debug.Log("item swap function called");
        if (Swap2.Placeable(Swap1.Item_Master) && Swap1.Placeable(Swap2.Item_Master))
        {
            //Debug.Log("item swap working");
            InvetorySlot temp = new InvetorySlot(Swap2.item, Swap2.amount);
            Swap2.UpdateSlot(Swap1.item, Swap1.amount);
            Swap1.UpdateSlot(temp.item, temp.amount);
        }
    }

    public void DropItem(Item item)
    {
        for (int i = 0; i < GetItems.Length; i++)
        {
            if (GetItems[i].item == item)
            {
                GetItems[i].UpdateSlot(null, 0);
            }
        }
    }


    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
        //IFormatter formatter = new BinaryFormatter();
        //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        //formatter.Serialize(stream, Container);
        //stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            /*
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetItems.Length; i++)
            {
                GetItems[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
            */
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}

public delegate void SlotsUpdated(InvetorySlot slot); 

[System.Serializable]
public class InvetorySlot
{
    public ItemType[] Allowed = new ItemType[0];
    [System.NonSerialized]
    public User_Interface parent;
    public Item item = new Item();
    public int amount;
    public string name;
    //[System.NonSerialized]
    //public SlotsUpdated OnBeforeUpdate;
    //[System.NonSerialized]
    //public SlotsUpdated OnAfterUpdate;

    public Item_Master Item_Master
    {
        get
        {
            if (item.ID >= 0)
            {
                return parent.inventory.database.Items[item.ID];
            }
            return null;
        }
    }

    public InvetorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public InvetorySlot( Item item, int amount)
    {
        UpdateSlot( item, amount);
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void UpdateSlot( Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
;
    }

    public void RemoveItem()
    {
        // find the sapwn point
        GameObject spawnpoint = GameObject.Find("Spawn_Point");
        //non stackable objects
        GameObject.Instantiate(item.prefab, spawnpoint.transform.position, spawnpoint.transform.rotation);
        item = new Item();

        // if item stackable show menu
        //item.prefab.GetComponent<Item_PickUp>().amount = 10;
        GameManager game = new GameManager();
        game.SaveGame();
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
    public InvetorySlot[] Items = new InvetorySlot[12];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(new Item(), 0);
        }
    }
    //public List<InvetorySlot> Items = new List<InvetorySlot>();
}

