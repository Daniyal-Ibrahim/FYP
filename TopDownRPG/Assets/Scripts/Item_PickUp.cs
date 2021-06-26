using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item_PickUp : MonoBehaviour// ,ISerializationCallbackReceiver
{
    // scriptable object representing the item
    public Item_Master pickUp;
    // how much of the item should be picked up 
    // too lazy to code a check to add non stackable obj seperatly
    // just dont set the amount to more then 1 for a non stackable obj
    public int amount;
    // bool used to respaws/ diabled already pickedup objs
    public bool pickedup;
    /*
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {

    }
    */
}
