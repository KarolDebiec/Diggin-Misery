using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public PlayerController playerController;
	public Vector3 playerItemDropOffset;

	public int space = 10;  // Amount of item spaces
	public int MaxStack = 99;
	// Our current list of items in the inventory

	public Item[] items;

	public int[] itemsNumber;

	public Item[] itemsQuick; // quick eq items

	public int[] itemsNumberQuick;
	// Add a new item if enough room

	public int DraggedItem;  // if its > 0 then something in inventory is dragged

	public Item testitem;
	void LateUpdate() //for test
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			Add(testitem);
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			Remove(testitem);
		}
	}

	public void Add (Item item)
	{
		if (item.showInInventory) 
		{
			/*if (items.Count >= space) 
			{
				Debug.Log ("Not enough room.");
				return;
			}*/
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] == item && itemsNumber[i] < MaxStack)
				{
					itemsNumber[i] += 1;
					return;
				}
				else if (items[i] == item && itemsNumber[i] >= MaxStack)
				{
					for (int k = 0; k < items.Length; k++)
					{
						if (items[i] == null)
						{
							items[i] = item;
							itemsNumber[i] = 1;
							return;
						}
					}
				}
			}
			for (int i = 0; i<items.Length ; i++)
			{
				if(items[i]==null)
				{
					items[i] = item; 
					itemsNumber[i]=1;
					return;
				}
			}
			for (int i = 0; i < itemsQuick.Length; i++)
			{
				if (itemsQuick[i] == item && itemsNumberQuick[i] < MaxStack)
				{
					itemsNumberQuick[i] += 1;
					return;
				}
				else if (itemsQuick[i] == item && itemsNumberQuick[i] >= MaxStack)
				{
					for (int k = 0; k < itemsQuick.Length; k++)
					{
						if (itemsQuick[i] == null)
						{
							itemsQuick[i] = item;
							itemsNumberQuick[i] = 1;
							return;
						}
					}
				}
			}
			for (int i = 0; i < itemsQuick.Length; i++)
			{
				if (itemsQuick[i] == null)
				{
					itemsQuick[i] = item;
					itemsNumberQuick[i] = 1;
					return;
				}
			}
			Debug.Log("Brak miejsca na dodanie przedmiotu");
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
	}	
	// Remove an item
	public void Remove (Item item)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == item && itemsNumber[i] > 0)
			{
				itemsNumber[i] -= 1;
				if (itemsNumber[i] <= 0)
				{
					itemsNumber[i] = 0;
					items[i] = null;
					return;
				}
				return;
			}
		}
		for (int i = 0; i < itemsQuick.Length; i++)
		{
			if (itemsQuick[i] == item && itemsNumberQuick[i] > 0)
			{
				itemsNumberQuick[i] -= 1;
				if (itemsNumberQuick[i] <= 0)
				{
					itemsNumberQuick[i] = 0;
					itemsQuick[i] = null;
					return;
				}
				return;
			}
		}
		//items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
	public void SwitchPositions(int origin, int destination)  // switches positions in inventory
	{
		if(destination < 200 && origin < 200)               // because quick slot id is starting from 200 so then it has to change between diffrent arrays
		{
			if (items[origin] == items[destination] && itemsNumber[destination] < MaxStack) // if its the same item then it wont switch but add
			{
				int dif = MaxStack - itemsNumber[destination];
				if (dif < itemsNumber[origin])
				{
					itemsNumber[destination] += dif;
					itemsNumber[origin] -= dif;
					return;
				}
				else if (dif >= itemsNumber[origin])
				{
					itemsNumber[destination] += itemsNumber[origin];
					itemsNumber[origin] = 0;
					items[origin] = null;
					return;
				}
			}
			Item tmp = items[origin];
			items[origin] = items[destination];
			items[destination] = tmp;
			int temp = itemsNumber[origin];
			itemsNumber[origin] = itemsNumber[destination];
			itemsNumber[destination] = temp;
		}
		else if (destination >= 200 && origin < 200)
		{
			if (items[origin] == itemsQuick[destination - 200] && itemsNumberQuick[destination-200] < MaxStack) // if its the same item then it wont switch but add
			{
				int dif = MaxStack - itemsNumberQuick[destination - 200];
				if (dif < itemsNumber[origin])
				{
					itemsNumberQuick[destination - 200] += dif;
					itemsNumber[origin] -= dif;
					return;
				}
				else if (dif >= itemsNumber[origin])
				{
					itemsNumberQuick[destination - 200] += itemsNumber[origin];
					itemsNumber[origin] = 0;
					items[origin] = null;
					return;
				}
			}
			Item tmp = items[origin];
			items[origin] = itemsQuick[destination - 200];
			itemsQuick[destination - 200] = tmp;
			int temp = itemsNumber[origin];
			itemsNumber[origin] = itemsNumberQuick[destination - 200];
			itemsNumberQuick[destination - 200] = temp;
		}
		else if (destination < 200 && origin >= 200)
		{
			if (itemsQuick[origin - 200] == items[destination] && itemsNumber[destination] < MaxStack) // if its the same item then it wont switch but add
			{
				int dif = MaxStack - itemsNumber[destination];
				if (dif < itemsNumberQuick[origin - 200])
				{
					itemsNumber[destination] += dif;
					itemsNumberQuick[origin - 200] -= dif;
					return;
				}
				else if (dif >= itemsNumberQuick[origin - 200])
				{
					itemsNumber[destination] += itemsNumberQuick[origin - 200];
					itemsNumberQuick[origin - 200] = 0;
					itemsQuick[origin - 200] = null;
					return;
				}
			}
			Item tmp = itemsQuick[origin - 200];
			itemsQuick[origin - 200] = items[destination];
			items[destination] = tmp;
			int temp = itemsNumberQuick[origin - 200];
			itemsNumberQuick[origin - 200] = itemsNumber[destination];
			itemsNumber[destination] = temp;
		}
		else if (destination >= 200 && origin >= 200)
		{
			if (itemsQuick[origin - 200] == itemsQuick[destination - 200] && itemsNumberQuick[destination - 200] < MaxStack) // if its the same item then it wont switch but add
			{
				int dif = MaxStack - itemsNumberQuick[destination - 200];
				if (dif < itemsNumberQuick[origin - 200])
				{
					itemsNumberQuick[destination - 200] += dif;
					itemsNumberQuick[origin - 200] -= dif;
					return;
				}
				else if (dif >= itemsNumberQuick[origin - 200])
				{
					itemsNumberQuick[destination - 200] += itemsNumberQuick[origin - 200];
					itemsNumberQuick[origin - 200] = 0;
					itemsQuick[origin - 200] = null;
					return;
				}
			}
			Item tmp = itemsQuick[origin - 200];
			itemsQuick[origin - 200] = itemsQuick[destination - 200];
			itemsQuick[destination - 200] = tmp;
			int temp = itemsNumberQuick[origin - 200];
			itemsNumberQuick[origin - 200] = itemsNumberQuick[destination - 200];
			itemsNumberQuick[destination - 200] = temp;
		}
	}
	public void OnItemDrop(int droppedItemID)
	{
		if (droppedItemID < 200)
		{
			Instantiate(items[droppedItemID].prefab, playerItemDropOffset + playerController.transform.position, Quaternion.identity);
			Remove(items[droppedItemID]);
		}
		else if (droppedItemID >= 200)
		{
			Instantiate(itemsQuick[droppedItemID-200].prefab, playerItemDropOffset + playerController.transform.position, Quaternion.identity);
			Remove(itemsQuick[droppedItemID-200]);
		}
	}
}
