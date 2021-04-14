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

	public int space = 10;  // Amount of item spaces
	public int MaxStack = 99;
	// Our current list of items in the inventory

	public Item[] items;

	public int[] itemsNumber;

	public Item[] itemsQuick; // quick eq items

	public int[] itemsNumberQuick;
	// Add a new item if enough room

	public int DraggedItem;  // if its > 0 then something in inventory is dragged
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

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
	}

	// Remove an item
	public void Remove (Item item)
	{/*
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == item && itemsNumber[i] > 0)
			{
				itemsNumber[i] -= 1;
				if (itemsNumber[i] <= 0)
				{
					items[i] = null;
					return;
				}
				return;
			}
		}
		//items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();*/
	}
	public void SwitchPositions(int origin, int destination)  // switches positions in inventory
	{
		Item tmp = items[origin];
		items[origin] = items[destination];
		items[destination] = tmp;
		int temp = itemsNumber[origin];
		itemsNumber[origin] = itemsNumber[destination];
		itemsNumber[destination] = temp;
	}
}
