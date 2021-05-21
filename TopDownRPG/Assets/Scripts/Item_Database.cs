using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/DataBase")]
public class Item_Database : ScriptableObject, ISerializationCallbackReceiver
{
    /*
    public Item_Master[] Items;
    //public Dictionary<int, Item_Master> GetItem = new Dictionary<int, Item_Master>();
    public void UpdateID()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].data.ID != i)
                Items[i].data.ID = i;
            //GetItem.Add(i, Items[i]);
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //GetItem = new Dictionary<int, Item_Master>();
    }
    */

    public Item_Master[] Items;
    public Dictionary<int, Item_Master> GetItem = new Dictionary<int, Item_Master>();
    public void OnAfterDeserialize()
    {
        GetItem = new Dictionary<int, Item_Master>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
       
    }
}
