using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class ItemSet
{
    // Head Elements *NOTE* add beard and hair 
    public GameObject Helmet;
    public GameObject Face;
    public GameObject Hair;
    public GameObject Beard;
    // Chest Elements *NOTE* add accesseries
    public GameObject Chest;
    // Arms Elemets
    public GameObject R_ArmUpper;
    public GameObject L_ArmUpper;
    public GameObject R_ArmLower;
    public GameObject L_ArmLower;
    public GameObject R_Hand;
    public GameObject L_Hand;
    // Pants Elements
    public GameObject Hips;
    public GameObject R_Leg;
    public GameObject L_Leg;
}

public class Inventroy_Manager : MonoBehaviour
{
    public GameObject game;
    public Item_Database database;
    public Inventory_Master inventory;
    public GameObject inv;
    public Inventory_Master equiptment;
    public GameObject equ;
    public GameObject Split;
    public List<GameObject> equiptmentPrefab;

    public List<ItemSet> itemSets;
    public List<GameObject> weapons;


    public Animator animator;
    public Tooltip tooltip;
    public Player_Input player;
    
    public bool InventoryActive = false;
    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            tooltip.Hide_Tooltip();
            // Enable / Disable Inventooy and equiptment 
            if (inv.activeInHierarchy == false && equ.activeInHierarchy == false && Split.activeInHierarchy == false)
                InventoryActive = false;

            if (InventoryActive)
            {
                inv.gameObject.SetActive(false);
                equ.gameObject.SetActive(false);
                Split.SetActive(false);
                InventoryActive = false;
                player.canRotate = true;

                //GetComponent<PlayerInput>().SwitchCurrentActionMap("Inventory");
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");

            }
            else
            {
                inv.gameObject.SetActive(true);
                equ.gameObject.SetActive(true);
                InventoryActive = true;
                player.canRotate = false;
                //GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Inventory");
                Debug.Log(GetComponent<PlayerInput>().currentActionMap);


            }
        }
    }
    private void Awake()
    {
        game = GameObject.Find("GameManager");
        UpdateEquiptment();
    }
    public void UpdateEquiptment()
    {
        for (int i = 0; i < equiptment.Container.Items.Length; i++)
        {
            NEquip(equiptment.Container.Items[i], equiptment.Container.Items[i]);
        }
    }

    #region // Trigger events on collision

    public void OnTriggerEnter(Collider other)
    {
        var item_PickUp = other.GetComponent<Item_PickUp>();
        // if the object is a pickup 
        if (item_PickUp)
        {
            // if its been added to the inventory
            if (inventory.AddItem(new Item(item_PickUp.pickUp), item_PickUp.amount))
            {
                item_PickUp.pickedup = true;
                //game.GetComponent<GameManager>().SaveGame();
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);
            }
            else
                Debug.Log("Inventory Full");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var dialog = other.GetComponent<DialogTrigger>();
        if (dialog)
        {
            dialog.talking = false;
            FindObjectOfType<DialogManager>().EndDialog();
        }
    }

    #endregion

    // does what it says
    public void UseConsumeable(InvetorySlot obj)
    {
        if (obj.item.Name == "HP Potion")
        {
            obj.amount -= 1;
            Test_Stat_UI test_Stats = GameObject.Find("UI_Elements").GetComponent<Test_Stat_UI>();

            test_Stats.AddHealth(5);

            if (obj.amount == 0)
            {
                obj.UpdateSlot(new Item(), 0);
            }
        }

        if (obj.item.Name == "HP Up Potion")
        {
            obj.amount -= 1;
            Test_Stat_UI test_Stats = GameObject.Find("UI_Elements").GetComponent<Test_Stat_UI>();

            //test_Stats.UpgradeHealth(50);
            test_Stats.AddHealth(5);

            if (obj.amount == 0)
            {
                // obj.RemoveItem();
                obj.UpdateSlot(new Item(), 0);
            }
        }
    }
    public void NEquip(InvetorySlot slot1, InvetorySlot slot2)
    {
        #region // Cheat Sheet
        // slot 1 is the obj the mouse is dragging 
        // slot 2 is the obj on the interface

        // equiptment.Container.Items[0]     Head
        // equiptment.Container.Items[1]     Chest
        // equiptment.Container.Items[2]     Arm
        // equiptment.Container.Items[3]     Leg
        // equiptment.Container.Items[4]     Bag
        // equiptment.Container.Items[5]     Ring 1
        // equiptment.Container.Items[6]     Ring 2
        // equiptment.Container.Items[7]     Ring 3
        // equiptment.Container.Items[8]     Ring 4
        // equiptment.Container.Items[9]     Spell Book
        // equiptment.Container.Items[10]    Primary Weapon
        // equiptment.Container.Items[11]    Secondary Weapons

        // itemSet[0] is nude model
        // itemSet[1] is Studded leather
        // itemSet[2] is Half-Plate
        #endregion
        int x,y;
        // Code for equiping/unequiping item models .. can be used to change animation states and stats but would be better if somethign else handled it 
        // Helmet 
        if (equiptment.Container.Items[0] == slot1)
        {
            Debug.Log("Removing something in Helmet Slot");
            for (int j = 1; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    // Setting nude model as Active
                    // add function to remove stats here
                    itemSets[0].Face.SetActive(true);
                    itemSets[0].Hair.SetActive(true);
                    itemSets[0].Beard.SetActive(true);
                    for (int k = 1; k < itemSets.Count; k++)
                    {
                        // Setting all other model as false
                        itemSets[k].Helmet.SetActive(false);
                        // add Function to update stats here 
                    }
                }
            }

        }
        if (equiptment.Container.Items[0] == slot2)
        {
            y = 0;
            Debug.Log("Placing something in Helmet");

            for (int j = 1; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    for (int k = 0; k < itemSets.Count; k++)
                    {
                        // set all other models to false
                        itemSets[k].Helmet.SetActive(false);
                    }
                    // X is the model associated with the set

                    // set model to active
                    itemSets[y+1].Helmet.SetActive(true);
                    // set extra models to false to prevent cliping 
                    itemSets[0].Face.SetActive(false);
                    itemSets[0].Hair.SetActive(false);
                    itemSets[0].Beard.SetActive(false);
                    // add fucntion to add stats here
                    return;
                }
                else // swaping with an empty slot
                {
                    for (int i = 1; i < itemSets.Count; i++)
                    {
                        itemSets[i].Helmet.SetActive(false);
                    }
                    itemSets[0].Face.SetActive(true);
                    itemSets[0].Hair.SetActive(true);
                    itemSets[0].Beard.SetActive(true);
                    // add function to remove stats here
                }
                y++;
            }

        }

        // Chest 
        if (equiptment.Container.Items[1] == slot1)
        {
            Debug.Log("Removing something in chest");
            for (int j = 2; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    // Setting nude model as Active
                    // add function to remove stats here
                    itemSets[0].Chest.SetActive(true);
                    for (int k = 1; k < itemSets.Count; k++)
                    {
                        // Setting all other model as false
                        itemSets[k].Chest.SetActive(false);
                        // add Function to update stats here 
                    }
                }
            }
        }
        if (equiptment.Container.Items[1] == slot2)
        {
            y = 0;
            Debug.Log("Placing something in chest");

            for (int j = 2; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    for (int k = 0; k < itemSets.Count; k++)
                    {
                        // set all other models to false
                        itemSets[k].Chest.SetActive(false);
                    }
                    // X is the model associated with the set
                    x = y+1;
                    // set model to active
                    itemSets[x].Chest.SetActive(true);
                    // add fucntion to add stats here
                    return;
                }
                else // swaping with an empty slot
                {
                    for (int i = 1; i < itemSets.Count; i++)
                    {
                        itemSets[i].Chest.SetActive(false);
                    }
                    itemSets[0].Chest.SetActive(true);
                    // add function to remove stats here
                }
                y++;
            }
        }

        // Arms 
        if (equiptment.Container.Items[2] == slot1)
        {
            Debug.Log("Removing something in Arms");
            for (int j = 3; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    for (int k = 0; k < itemSets.Count; k++)
                    {
                        itemSets[k].R_ArmLower.SetActive(false);
                        itemSets[k].L_ArmLower.SetActive(false);
                        itemSets[k].R_ArmUpper.SetActive(false);
                        itemSets[k].L_ArmUpper.SetActive(false);
                        itemSets[k].R_Hand.SetActive(false);
                        itemSets[k].L_Hand.SetActive(false);
                    }
                    itemSets[0].R_ArmLower.SetActive(true);
                    itemSets[0].L_ArmLower.SetActive(true);
                    itemSets[0].R_ArmUpper.SetActive(true);
                    itemSets[0].L_ArmUpper.SetActive(true);
                    itemSets[0].R_Hand.SetActive(true);
                    itemSets[0].L_Hand.SetActive(true);
                }
            }
        }
        if (equiptment.Container.Items[2] == slot2)
        {
            Debug.Log("Placing something in Arms");
            y = 0;
            for (int j = 3; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    for (int k = 0; k < itemSets.Count; k++)
                    {
                        itemSets[k].R_ArmLower.SetActive(false);
                        itemSets[k].L_ArmLower.SetActive(false);
                        itemSets[k].R_ArmUpper.SetActive(false);
                        itemSets[k].L_ArmUpper.SetActive(false);
                        itemSets[k].R_Hand.SetActive(false);
                        itemSets[k].L_Hand.SetActive(false);
                    }
                    x = y + 1;
                    itemSets[x].R_ArmLower.SetActive(true);
                    itemSets[x].L_ArmLower.SetActive(true);
                    itemSets[x].R_ArmUpper.SetActive(true);
                    itemSets[x].L_ArmUpper.SetActive(true);
                    itemSets[x].R_Hand.SetActive(true);
                    itemSets[x].L_Hand.SetActive(true);
                    return;
                }
                else // swaping with an empty slot
                {
                    for (int i = 1; i < itemSets.Count; i++)
                    {
                        itemSets[i].R_ArmLower.SetActive(false);
                        itemSets[i].L_ArmLower.SetActive(false);
                        itemSets[i].R_ArmUpper.SetActive(false);
                        itemSets[i].L_ArmUpper.SetActive(false);
                        itemSets[i].R_Hand.SetActive(false);
                        itemSets[i].L_Hand.SetActive(false);
                    }
                    itemSets[0].R_ArmLower.SetActive(true);
                    itemSets[0].L_ArmLower.SetActive(true);
                    itemSets[0].R_ArmUpper.SetActive(true);
                    itemSets[0].L_ArmUpper.SetActive(true);
                    itemSets[0].R_Hand.SetActive(true);
                    itemSets[0].L_Hand.SetActive(true);
                }
                y++;
            }
        }
        
        // Legs 
        if (equiptment.Container.Items[3] == slot1)
        {
            Debug.Log("Removing something in Legs");
            for (int j = 4; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    itemSets[0].Hips.SetActive(true);
                    itemSets[0].R_Leg.SetActive(true);
                    itemSets[0].L_Leg.SetActive(true);
                    for (int k = 1; k < itemSets.Count; k++)
                    {
                        itemSets[k].Hips.SetActive(false);
                        itemSets[k].R_Leg.SetActive(false);
                        itemSets[k].L_Leg.SetActive(false);
                    }
                }
            }

        }
        if (equiptment.Container.Items[3] == slot2)
        {
            y = 0;
            Debug.Log("Placing something in Legs");
            for (int j = 4; j < 1 + (itemSets.Capacity * 4); j += 4)
            {
                if (slot2.item.ID == j)
                {
                    for (int k = 0; k < itemSets.Count; k++)
                    {
                        itemSets[k].Hips.SetActive(false);
                        itemSets[k].R_Leg.SetActive(false);
                        itemSets[k].L_Leg.SetActive(false);
                    }
                    x = y + 1;
                    itemSets[x].Hips.SetActive(true);
                    itemSets[x].R_Leg.SetActive(true);
                    itemSets[x].L_Leg.SetActive(true);
                    return;
                }
                else // swaping with an empty slot
                {
                    for (int i = 1; i < itemSets.Count; i++)
                    {
                        itemSets[i].Hips.SetActive(false);
                        itemSets[i].R_Leg.SetActive(false);
                        itemSets[i].L_Leg.SetActive(false);
                    }
                    itemSets[0].Hips.SetActive(true);
                    itemSets[0].R_Leg.SetActive(true);
                    itemSets[0].L_Leg.SetActive(true);
                }
                y++;
            }
        }
        
        // Primary Weapon 
        if (equiptment.Container.Items[10] == slot1)
        {
            Debug.Log("Removing something in Primary");
            for (int i = 0; i < weapons.Count; i ++)
            {
                weapons[i].SetActive(false);
                animator.SetInteger("P_ID", 0);
            }

        }
        if (equiptment.Container.Items[10] == slot2)
        {
            Debug.Log("Placing something in Primary");
            if (slot2.item.ID == 13)
            {
                weapons[0].SetActive(true);
                animator.SetInteger("P_ID", 1);
            }

            else // swaping with an empty slot
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    weapons[i].SetActive(false);
                    animator.SetInteger("P_ID", 0);
                }

            }

        }

        // Secondary Weapon 
        if (equiptment.Container.Items[11] == slot1)
        {
            Debug.Log("Removing something in Secondary");
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
                animator.SetInteger("S_ID", 0);
            }

        }
        if (equiptment.Container.Items[11] == slot2)
        {
            Debug.Log("Placing something in Secondary");
            if (slot2.item.ID == 14)
            {
                weapons[1].SetActive(true);
                animator.SetInteger("S_ID", 1);
            }

            else // swaping with an empty slot
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    weapons[i].SetActive(false);
                    animator.SetInteger("S_ID", 0);
                }

            }

        }



    }

    /*

    public void Equip(InvetorySlot obj)
    {

        Debug.Log(obj.item.ID + "Equiped");
        switch (obj.item.ID)
        {
            case 0: // HEAD
                Debug.Log("case Head");
                if (equiptment.Container.Items[0].item.ID == 0)
                {
                    equiptmentPrefab[0].SetActive(true);
                    
                }
                else
                {
                    equiptmentPrefab[0].SetActive(false);
                    
                }
                break;

            case 1: // CHEST
                Debug.Log("case Chest");
                if (equiptment.Container.Items[1].item.ID == 1)
                {
                    equiptmentPrefab[1].SetActive(true);
                   
                }
                else
                {
                    equiptmentPrefab[1].SetActive(false);
                   
                }
                break;

            case 2: // ARM
                Debug.Log("case Arm");
                if (equiptment.Container.Items[2].item.ID == 2)
                {
                    equiptmentPrefab[2].SetActive(true);
                    equiptmentPrefab[3].SetActive(true);
                    equiptmentPrefab[4].SetActive(true);
                    equiptmentPrefab[5].SetActive(true);
                   
                }
                else
                {
                    equiptmentPrefab[2].SetActive(false);
                    equiptmentPrefab[3].SetActive(false);
                    equiptmentPrefab[4].SetActive(false);
                    equiptmentPrefab[5].SetActive(false);
                    
                }
                break;

            case 3: // LEG
                Debug.Log("case Leg");
                if (equiptment.Container.Items[3].item.ID == 3)
                {
                    equiptmentPrefab[6].SetActive(true);
                    equiptmentPrefab[7].SetActive(true);
                    equiptmentPrefab[8].SetActive(true);
                    equiptmentPrefab[9].SetActive(true);
                    equiptmentPrefab[10].SetActive(true);
                    
                }
                else
                {
                    equiptmentPrefab[6].SetActive(false);
                    equiptmentPrefab[7].SetActive(false);
                    equiptmentPrefab[8].SetActive(false);
                    equiptmentPrefab[9].SetActive(false);
                    equiptmentPrefab[10].SetActive(false);
                    
                }
                break;

            case 4: // BAG
                Debug.Log("case Bag");

                break;

            case 5: // RING 1
                break;
            case 6: // RING 2
                break;
            case 7: // RING 3
                break;
            case 8: // RING 4
                break;

            case 9: // SPELL 
                break;

            case 14: // MAIN-HAND
                Debug.Log("case main-hand");
                // 2H spear
                if (equiptment.Container.Items[10].item.ID == 14)
                {
                    animator.SetInteger("Animation ID", 2);
                    equiptmentPrefab[11].SetActive(true);
                    equiptmentPrefab[12].SetActive(false);
                    equiptmentPrefab[13].SetActive(false);
                    equiptmentPrefab[14].SetActive(false);
                    equiptmentPrefab[15].SetActive(false);
                    equiptmentPrefab[16].SetActive(false);
                }
               
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[11].SetActive(false);
                }
                break;
            case 15: // MAIN-HAND
                Debug.Log("case main-hand");
                // Spear
                if (equiptment.Container.Items[10].item.ID == 15)
                {
                    animator.SetInteger("Animation ID", 1);
                    equiptmentPrefab[12].SetActive(true);
                    equiptmentPrefab[11].SetActive(false);
                    equiptmentPrefab[13].SetActive(false);
                    equiptmentPrefab[14].SetActive(false);
                    equiptmentPrefab[15].SetActive(false);
                    equiptmentPrefab[16].SetActive(false);
                }         
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[12].SetActive(false);
                    
                }
                break;
            case 16: // MAIN-HAND
                Debug.Log("case main-hand");          
                // 2H Sword
                if (equiptment.Container.Items[10].item.ID == 16)
                {
                    animator.SetInteger("Animation ID", 2);
                    equiptmentPrefab[13].SetActive(true);
                    equiptmentPrefab[12].SetActive(false);
                    equiptmentPrefab[11].SetActive(false);
                    equiptmentPrefab[14].SetActive(false);
                    equiptmentPrefab[15].SetActive(false);
                    equiptmentPrefab[16].SetActive(false);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[13].SetActive(false);
                }
                break;
            case 17: // MAIN-HAND
                Debug.Log("case main-hand");
                // Sword
                if (equiptment.Container.Items[10].item.ID == 17)
                {
                    animator.SetInteger("Animation ID", 1);
                    equiptmentPrefab[14].SetActive(true);
                    equiptmentPrefab[12].SetActive(false);
                    equiptmentPrefab[13].SetActive(false);
                    equiptmentPrefab[11].SetActive(false);
                    equiptmentPrefab[15].SetActive(false);
                    equiptmentPrefab[16].SetActive(false);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[14].SetActive(false);
                }
                break;
            case 18: // MAIN-HAND
                Debug.Log("case main-hand");
                // Dagger
                if (equiptment.Container.Items[10].item.ID == 18)
                {
                    animator.SetInteger("Animation ID", 1);
                    equiptmentPrefab[15].SetActive(true);
                    equiptmentPrefab[12].SetActive(false);
                    equiptmentPrefab[13].SetActive(false);
                    equiptmentPrefab[14].SetActive(false);
                    equiptmentPrefab[11].SetActive(false);
                    equiptmentPrefab[16].SetActive(false);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[15].SetActive(false);
                }
                break;
            case 19: // MAIN-HAND
                Debug.Log("case main-hand");
                // Staff
                if (equiptment.Container.Items[10].item.ID == 19)
                {
                    animator.SetInteger("Animation ID", 2);
                    equiptmentPrefab[16].SetActive(true);
                    equiptmentPrefab[12].SetActive(false);
                    equiptmentPrefab[13].SetActive(false);
                    equiptmentPrefab[14].SetActive(false);
                    equiptmentPrefab[15].SetActive(false);
                    equiptmentPrefab[11].SetActive(false);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[16].SetActive(false);
                }
                break;

            case 20: // OFF-HAND
                Debug.Log("case off-hand");
                // Shield
                if (equiptment.Container.Items[11].item.ID == 20)
                {
                    //animator.SetInteger("Animation ID", 1);
                    equiptmentPrefab[17].SetActive(true);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[17].SetActive(false);;
                }
                break;

            case 21: // OFF-HAND
                Debug.Log("case off-hand bow");
                // Bow ? no
                if (equiptment.Container.Items[11].item.ID == 22)
                {
                    animator.SetInteger("Animation ID", 3);
                    equiptmentPrefab[18].SetActive(true);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[18].SetActive(false); ;
                }
                break;

            case 22: // OFF-HAND
                Debug.Log("case off-hand");
                // Bow
                if (equiptment.Container.Items[11].item.ID == 22)
                {
                    animator.SetInteger("Animation ID", 3);
                    equiptmentPrefab[19].SetActive(true);
                }
                else
                {
                    animator.SetInteger("Animation ID", 0);
                    equiptmentPrefab[19].SetActive(false);
                }
                break;

            default:
                Debug.Log("Default case");
                break;
        }
    }

    */



    public GameObject projectile;
    public GameObject projectile2;
    public GameObject spawnPoint;
    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }

    public void SpawnProjectile2()
    {
        GameObject bullet = Instantiate(projectile2, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
    }
    private void Update()
    {
        // set flag for dual wielding 
        // if using a dual wield weapons u cant use secondary slot
    }

    private void OnApplicationQuit()
    {
        //inventory.Container.Clear();
        //equiptment.Container.Clear();
    }
}
