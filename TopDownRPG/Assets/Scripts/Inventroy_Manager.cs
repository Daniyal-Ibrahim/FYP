using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy_Manager : MonoBehaviour
{
    public Inventory_Master inventory;
    public Inventory_Master equiptment;

    public void OnTriggerEnter(Collider other)
    {
        var item_PickUp = other.GetComponent<Item_PickUp>();
        if (item_PickUp)
        {
            inventory.AddItem(new Item(item_PickUp.pickUp), item_PickUp.amount);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equiptment.Container.Clear();
    }
}
