using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventroy_Manager : MonoBehaviour
{
    public GameObject game;
    public Item_Database database;
    public Inventory_Master inventory;
    public GameObject inv;
    public Inventory_Master equiptment;
    public GameObject equ;
    public List<GameObject> equiptmentPrefab;
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
            if (inv.activeInHierarchy == false && equ.activeInHierarchy == false)
                InventoryActive = false;

            if (InventoryActive)
            {
                inv.gameObject.SetActive(false);
                equ.gameObject.SetActive(false);
                InventoryActive = false;
                
                //GetComponent<PlayerInput>().SwitchCurrentActionMap("Inventory");
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");

            }
            else
            {
                inv.gameObject.SetActive(true);
                equ.gameObject.SetActive(true);
                InventoryActive = true;
                //GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Inventory");
                Debug.Log(GetComponent<PlayerInput>().currentActionMap);


            }
        }
    }
    private void Awake()
    {
        game = GameObject.Find("GameManager");
        //UpdateEquiptment();
    }

    public void UpdateEquiptment()
    {
        for (int i = 0; i < equiptment.Container.Items.Length; i++)
        {
            Equip(equiptment.Container.Items[i]);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        
        var item_PickUp = other.GetComponent<Item_PickUp>();
        if (item_PickUp)
        {
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
    public void UseConsumeable(InvetorySlot obj)
    {
        if (obj.item.Name == "HP Potion")
        {
            obj.amount -= 1;
            Test_Stat_UI test_Stats = GameObject.Find("UI_Elements").GetComponent<Test_Stat_UI>();

            test_Stats.AddHealth(50);


            if (obj.amount == 0)
            {
                obj.UpdateSlot(new Item(), 0);
            }
        }

        if (obj.item.Name == "HP Up Potion")
        {
            obj.amount -= 1;
            Test_Stat_UI test_Stats = GameObject.Find("UI_Elements").GetComponent<Test_Stat_UI>();

            test_Stats.UpgradeHealth(50);


            if (obj.amount == 0)
            {
                // obj.RemoveItem();
                obj.UpdateSlot(new Item(), 0);
            }
        }
    }

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
    public GameObject projectile;
    public GameObject spawnPoint;
    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Save();
            equiptment.Save();
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            inventory.Load();
            equiptment.Load();

            /*
            if (equiptment.Container.Items[0].item.ID == 0)
            {
                equiptmentPrefab[0].SetActive(true);
            }
            if (equiptment.Container.Items[1].item.ID == 1)
            {
                equiptmentPrefab[1].SetActive(true);
            }
            if (equiptment.Container.Items[2].item.ID == 2)
            {
                equiptmentPrefab[2].SetActive(true);
                equiptmentPrefab[3].SetActive(true);
                equiptmentPrefab[4].SetActive(true);
                equiptmentPrefab[5].SetActive(true);
            }
            if (equiptment.Container.Items[3].item.ID == 3)
            {
                equiptmentPrefab[6].SetActive(true);
                equiptmentPrefab[7].SetActive(true);
                equiptmentPrefab[8].SetActive(true);
                equiptmentPrefab[9].SetActive(true);
                equiptmentPrefab[10].SetActive(true);
            }
            if (equiptment.Container.Items[10].item.ID == 10)
            {
                animator.SetInteger("Animation ID", 1);
                equiptmentPrefab[12].SetActive(true);
            }
            if (equiptment.Container.Items[11].item.ID == 11)
            {
                equiptmentPrefab[14].SetActive(true);
            }
            */
        }
    }

    private void OnApplicationQuit()
    {
        //inventory.Container.Clear();
        //equiptment.Container.Clear();
    }
}
