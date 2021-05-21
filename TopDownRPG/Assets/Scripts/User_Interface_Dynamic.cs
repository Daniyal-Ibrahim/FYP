using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class User_Interface_Dynamic : User_Interface
{
    public GameObject inventoyPrefab;
    public override void CreateSlots()
    {
        interfaceSlot = new Dictionary<GameObject, InvetorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoyPrefab, Vector3.zero, Quaternion.identity, transform);
            interfaceSlot.Add(obj, inventory.Container.Items[i]);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClickDynamic(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

        }
    }
}
