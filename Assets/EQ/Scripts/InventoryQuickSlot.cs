using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class InventoryQuickSlot : MonoBehaviour, IDropHandler
{

	public Image icon;
	public Text AmountText;

	Item item;  // Current item in the slot

	private Inventory inventory;
	public int slotID;          // quick slot id is starting from 200
								// Add item to the slot
	void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
	}
	public void AddItem(Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
	}
	// Clear the slot
	public void ClearSlot()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		AmountText.text = " ";
	}
	public void UpdateSlotUI(int amount)
	{
		if (item == null || icon == null)
		{
			icon.enabled = false;
		}
		else
		{
			icon.enabled = true;
			icon.sprite = item.icon;
		}
		//icon.sprite = item.icon;
		if (amount > 0 && item.isStackable)
		{
			AmountText.text = amount.ToString("f0");
		}
		else
		{
			AmountText.text = " ";
		}
	}
	// If the remove button is pressed, this function will be called.
	/*public void RemoveItemFromInventory ()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>().Remove(item);
	}*/

	// Use the item
	public void UseItem()
	{
		if (item != null)
		{
			item.Use();
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (inventory.DraggedItem < 0)
		{
			Debug.Log("Nothing from inventory is dragged");
		}
		else if (inventory.DraggedItem != slotID)
		{
			inventory.SwitchPositions(inventory.DraggedItem, slotID);
			Debug.Log("Switched : " + inventory.DraggedItem + " with " + slotID);
		}
		else
		{
			Debug.Log("Item dropped on itself");
		}
	}
}
