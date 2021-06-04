using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public abstract class User_Interface : MonoBehaviour
{
    public GameObject MasterCanvas;
    public Inventroy_Manager manager;
    public Inventory_Master inventory;
    public GameObject SpawnPoint;
    public Tooltip tooltip;
    public int tap;
    protected Dictionary<GameObject, InvetorySlot> interfaceSlot = new Dictionary<GameObject, InvetorySlot>();

    void Start()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        tooltip = GameObject.FindWithTag("Tool").GetComponent<Tooltip>();
        split_slider.minValue = 1;
    }
    void Update()
    {
        UpdateSlots();


        if (spliting)
        {
            //split_slider.value = Mathf.RoundToInt(split_itemAmountToSplit);
            split_itemAmountToSplit = Mathf.RoundToInt(split_slider.value);
            StringBuilder builder = new StringBuilder();
            builder.Append(split_slider.value).Append(" / ").Append(split_itemAmountTotal);
            //Debug.Log(+split_itemAmountToSplit + " " + split_itemAmountTotal + "");
            split_amount.text = builder.ToString();
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InvetorySlot> slot in interfaceSlot)
        {
            if (slot.Value.item.ID >= 0)
            {
                slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.Value.Item_Master.icon;
                slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.amount == 1 ? "" : slot.Value.amount.ToString("n0");
            }
            else
            {
                slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.currentInterface = obj.GetComponent<User_Interface>();

    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.currentInterface = null;
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.currentSlot = obj;
        if (interfaceSlot[obj].item.ID >= 0)
        {
            StartCoroutine(ToolTip_Delay());
            IEnumerator ToolTip_Delay()
            {
                yield return new WaitForSeconds(0.5f);
                tooltip = GameObject.FindWithTag("Tool").GetComponent<Tooltip>();
                tooltip.Show_Tooltip(interfaceSlot[obj].item);
            }
        }

    }

    public void OnExit(GameObject obj)
    {
        MouseData.currentSlot = null;
        StopAllCoroutines();
        tooltip.Hide_Tooltip();
    }

    public void OnPointerClick(GameObject obj)
    {

    }


    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(MasterCanvas.transform);
        if (interfaceSlot[obj].item.ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = interfaceSlot[obj].Item_Master.icon;
            img.raycastTarget = false;
        }
        MouseData.currentObject = mouseObject;
    }

    // thigns needed to split items
    public Image split_image;
    public TextMeshProUGUI split_name;
    public Slider split_slider;
    public TextMeshProUGUI split_amount;
    int split_itemAmountToSplit;
    int split_itemAmountTotal;
    public GameObject split_pannel;
    public bool spliting = false;
    GameObject splieter;
    public void Confirm()
    {
        spliting = false;
        split_pannel.SetActive(false);
        interfaceSlot[splieter].SubAmount(split_itemAmountToSplit);
        interfaceSlot[splieter].RemoveItem(split_itemAmountToSplit);


    }

    public void Cancel()
    {
        spliting = false;
        split_pannel.SetActive(false);
    }

    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.currentObject);
        // if you are dropping the item
        if (MouseData.currentInterface == null)
        {
            //destroy item
            if (interfaceSlot[obj].item.Stackable && interfaceSlot[obj].amount > 1)
            {
                spliting = true;
                split_pannel.SetActive(true);
                // get data
                split_image.sprite = interfaceSlot[obj].Item_Master.icon;
                split_name.text = interfaceSlot[obj].Item_Master.itemName;
                split_itemAmountTotal = interfaceSlot[obj].amount;
                split_itemAmountToSplit = Mathf.RoundToInt(split_itemAmountTotal / 2);
                Debug.Log("Total amount = " + split_itemAmountTotal + " Items Split = " + split_itemAmountToSplit +  " ");
                split_slider.maxValue = split_itemAmountTotal - 1;
                split_slider.value = split_itemAmountToSplit;
                // wait for confirm or cancel
                splieter = obj;
                return;
                //Debug.Log("item spawn/destroy");
            }
            else 
            {
                interfaceSlot[obj].RemoveItem();
            }
            return;
        }

        if (MouseData.currentSlot)
        {
            // Debug.Log("item swap");
            InvetorySlot currentSlotData = MouseData.currentInterface.interfaceSlot[MouseData.currentSlot];
            inventory.SwapItem(interfaceSlot[obj], currentSlotData);
            if (MouseData.currentInterface.GetComponent<User_Interface_Static>())
            {
                Debug.Log("This is the equiptment screen");
                manager.NEquip(interfaceSlot[obj], currentSlotData);
            }

            if (MouseData.currentInterface.GetComponent<User_Interface_Dynamic>())
            {
                Debug.Log("This is the Inventory screen");
                manager.NEquip(interfaceSlot[obj], currentSlotData);
            }

        }

        // create equip function

    }

    public void OnDrag(GameObject obj)
    {
        if (MouseData.currentObject != null)
        {
            MouseData.currentObject.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        }

    }
    public void OnPointerClickDynamic(GameObject obj)
    {
        // do something
        if (MouseData.currentSlot)
        {
            Debug.Log("test ");
            
            InvetorySlot currentSlotData = MouseData.currentInterface.interfaceSlot[MouseData.currentSlot];
   
            inventory.AutoEquip_UseItem(currentSlotData);
            //InvetorySlot currentSlotData = MouseData.currentInterface.interfaceSlot[MouseData.currentSlot];
            //inventory.SwapItem(interfaceSlot[obj], currentSlotData);
            //manager.Equip(currentSlotData);
        }

    }

    public void OnPointerClickStatic(GameObject obj)
    {
        // do something
        if (MouseData.currentSlot)
        {

            InvetorySlot currentSlotData = MouseData.currentInterface.interfaceSlot[MouseData.currentSlot];

            Debug.Log("this is " +obj.name+ " " );

            inventory.AutoEquip_UseItem(currentSlotData);
            //InvetorySlot currentSlotData = MouseData.currentInterface.interfaceSlot[MouseData.currentSlot];
            //inventory.SwapItem(interfaceSlot[obj], currentSlotData);
            //manager.Equip(currentSlotData);
        }

    }
}

    public static class MouseData
{

    public static User_Interface currentInterface;
    public static GameObject currentObject;
    public static GameObject currentSlot;

}
