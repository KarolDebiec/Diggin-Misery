using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemIconHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Inventory inventory;
    private CanvasGroup canvasGroup;
    public int slotId; // 0-19 or something like that
    void Awake()
    {
        Debug.Log("halo alo");
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        canvasGroup = GetComponent<CanvasGroup>();
        if(((gameObject.transform.parent).transform.parent).GetComponent<InventorySlot>()==null)
        {
            slotId = ((gameObject.transform.parent).transform.parent).GetComponent<InventoryQuickSlot>().slotID;
        }
        else
        {
            slotId = ((gameObject.transform.parent).transform.parent).GetComponent<InventorySlot>().slotID;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.position = Input.mousePosition;
        inventory.DraggedItem = slotId;
        Debug.Log("Dragging");
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        bool dropItem = false;
        List<GameObject> hoveredList = eventData.hovered;
        foreach (var GO in hoveredList)
        {
            Debug.Log("Hovering over: " + GO.name);
            if(GO.name == "EquipmentPanel")
            {
                dropItem = true;
            }
        }
        canvasGroup.blocksRaycasts = true;
        transform.localPosition = Vector3.zero;
        inventory.DraggedItem = -1;
        if(!dropItem)
        {
            inventory.OnItemDrop(slotId);
            Debug.Log("Dropped item with id : " + slotId);
        }
    }



}

