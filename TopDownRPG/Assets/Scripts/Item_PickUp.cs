using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item_PickUp : MonoBehaviour,ISerializationCallbackReceiver
{
    public Item_Master pickUp;
    public int amount;
    public bool pickedup;
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {

    }
}
