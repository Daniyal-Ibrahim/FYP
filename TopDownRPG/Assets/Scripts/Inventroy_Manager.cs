using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventroy_Manager : MonoBehaviour
{
    public Inventory_Master inventory;
    public GameObject inv;
    public Inventory_Master equiptment;
    public GameObject equ;
    public bool Active = false;

    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inv.activeInHierarchy == false && equ.activeInHierarchy == false)
                Active = false;

            if (Active)
            {
                inv.gameObject.SetActive(false);
                equ.gameObject.SetActive(false);
                Active = false;
            }
            else
            {
                inv.gameObject.SetActive(true);
                equ.gameObject.SetActive(true);
                Active = true;
            }
            //Debug.Log("Input");
        }
    }

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
