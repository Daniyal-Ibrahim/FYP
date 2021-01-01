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
    public List<GameObject> weaponPrfabs;
    public Animator animator;

    public bool InventoryActive = false;

    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Enable / Disable Inventooy and equiptment 
            if (inv.activeInHierarchy == false && equ.activeInHierarchy == false)
                InventoryActive = false;

            if (InventoryActive)
            {
                inv.gameObject.SetActive(false);
                equ.gameObject.SetActive(false);
                InventoryActive = false;
                
            }
            else
            {
                inv.gameObject.SetActive(true);
                equ.gameObject.SetActive(true);
                InventoryActive = true;
                
            }
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

    public void Equip()
    {
        // HEAD
        if (equiptment.Container.Items[0].item.ID == 0)
        {
            weaponPrfabs[0].SetActive(true);
            Debug.Log("Head equiped");
        }
        else
        {
            weaponPrfabs[0].SetActive(false);
            Debug.Log("Head Un-equiped");
        }
        // CHEST
        if (equiptment.Container.Items[1].item.ID == 1)
        {
            weaponPrfabs[1].SetActive(true);
            Debug.Log("Chest equiped");
        }
        else
        {
            weaponPrfabs[1].SetActive(false);
            Debug.Log("Chest Un-equiped");
        }
        // ARM
        if (equiptment.Container.Items[2].item.ID == 2)
        {
            weaponPrfabs[2].SetActive(true);
            weaponPrfabs[3].SetActive(true);
            weaponPrfabs[4].SetActive(true);
            weaponPrfabs[5].SetActive(true);
            Debug.Log("Arms equiped");
        }
        else
        {
            weaponPrfabs[2].SetActive(false);
            weaponPrfabs[3].SetActive(false);
            weaponPrfabs[4].SetActive(false);
            weaponPrfabs[5].SetActive(false);
            Debug.Log("Arms Un-equiped");
        }
        // LEG
        if (equiptment.Container.Items[3].item.ID == 3)
        {
            weaponPrfabs[6].SetActive(true);
            weaponPrfabs[7].SetActive(true);
            weaponPrfabs[8].SetActive(true);
            weaponPrfabs[9].SetActive(true);
            weaponPrfabs[10].SetActive(true);
            Debug.Log("Legs equiped");
        }
        else
        {
            weaponPrfabs[6].SetActive(false);
            weaponPrfabs[7].SetActive(false);
            weaponPrfabs[8].SetActive(false);
            weaponPrfabs[9].SetActive(false);
            weaponPrfabs[10].SetActive(false);
            Debug.Log("Legs Un-equiped");
        }
        // BAG
        /*
        if (equiptment.Container.Items[4].item.ID == 4)
        {
            animator.SetInteger("Animation ID", 2);
            weaponPrfabs[4].SetActive(true);
            Debug.Log("Sword equiped");
        }
        else
        {
            animator.SetInteger("Animation ID", 0);
            weaponPrfabs[4].SetActive(false);
            Debug.Log("Sword Un-equiped");
        }
        // SPELL
        if (equiptment.Container.Items[9].item.ID == 9)
        {
            animator.SetInteger("Animation ID", 2);
            weaponPrfabs[9].SetActive(true);
            Debug.Log("Sword equiped");
        }
        else
        {
            animator.SetInteger("Animation ID", 0);
            weaponPrfabs[9].SetActive(false);
            Debug.Log("Sword Un-equiped");
        }
        */
        // SWORD
        if (equiptment.Container.Items[10].item.ID == 10)
        {
            animator.SetInteger("Animation ID", 2);
            weaponPrfabs[13].SetActive(true);
            Debug.Log("Sword equiped");
        }
        else
        {
            animator.SetInteger("Animation ID", 0);
            weaponPrfabs[13].SetActive(false);
            Debug.Log("Sword Un-equiped");
        }
        // SHIELD
        if (equiptment.Container.Items[11].item.ID == 11)
        {
            weaponPrfabs[14].SetActive(true);
            Debug.Log("Shield equiped");
        }
        else
        {
            weaponPrfabs[14].SetActive(false);
            Debug.Log("Shield Un-equiped");
        }
    }


    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equiptment.Container.Clear();
    }
}
