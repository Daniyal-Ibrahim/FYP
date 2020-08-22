using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/DataBase")]
public class Item_Database : ScriptableObject, ISerializationCallbackReceiver
{
    public Item_Master[] Items;
    public Dictionary<int, Item_Master> GetItem = new Dictionary<int, Item_Master>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].data.ID = i;
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, Item_Master>();
    }
}
