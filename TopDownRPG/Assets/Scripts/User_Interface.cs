using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public abstract class User_Interface : MonoBehaviour
{
    public GameObject MasterCanvas;
    public Inventroy_Manager manager;
    public Inventory_Master inventory;
    public GameObject SpawnPoint;
    Mouse mouse = Mouse.current;


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
    }
    void Update()
    {
        UpdateSlots();
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
    }

    public void OnExit(GameObject obj)
    {
        MouseData.currentSlot = null;
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

    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.currentObject);

        if (MouseData.currentInterface == null)
        {
            //destroy item
            interfaceSlot[obj].RemoveItem();
            //Debug.Log("item spawn/destroy");
            return;
        }

        if (MouseData.currentSlot)
        {
           // Debug.Log("item swap");
            InvetorySlot currentSlotData = MouseData.currentInterface.interfaceSlot[MouseData.currentSlot];
            inventory.SwapItem(interfaceSlot[obj], currentSlotData);
            manager.Equip();
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

}

public static class MouseData
{

    public static User_Interface currentInterface;
    public static GameObject currentObject;
    public static GameObject currentSlot;

}
