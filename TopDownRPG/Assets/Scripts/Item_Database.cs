using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/DataBase")]
public class Item_Database : ScriptableObject, ISerializationCallbackReceiver
{
    public Item_Master[] Items;
    public Dictionary<int, Item_Master> GetItem = new Dictionary<int, Item_Master>();

    #region // How ID are assigned
    /*
     * Depending on the Item Database that is currently in use 
     * this will run through all the items present in the database 
     * and assign thier ID respective to their current position 
     * This way i can add or remove items as needed and their IDs will be 
     * updated automaticly, Might be unstable for larger projects but good enough for me... for now
     */
    #endregion
    public void OnAfterDeserialize()
    {
        GetItem = new Dictionary<int, Item_Master>();
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].data.ID != i)
                Items[i].data.ID = i;

            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, Item_Master>();
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].data.ID != i)
                Items[i].data.ID = i;

            GetItem.Add(i, Items[i]);
        }
    }
    

}
