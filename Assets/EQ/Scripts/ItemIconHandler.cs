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
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotId = ((gameObject.transform.parent).transform.parent).GetComponent<InventorySlot>().slotID;
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
        canvasGroup.blocksRaycasts = true;
        transform.localPosition = Vector3.zero;
        inventory.DraggedItem = -1;
    }



}

