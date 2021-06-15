using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingItem : MonoBehaviour
{
    public Inventory inventory;
    public Item[] requiredItems;
    public int[] requiredItemsAmount;
    public Item outputItem;
    public int outputItemNumber; // how many items are going to be crafted
    public Text outputItemName;
    public Text outputItemAmount;
    public Image[] requiredItemsIconsDisplay;
    public Text[] requiredItemsNumbers;
    public Image ItemIconDisplay;
    public Image ItemButtonBackground;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfCanCraftItem())
        {
            DisplayAvailable();
        }
        else
        {
            DisplayUnavailable();
        }

    }
    private void Awake()
    {
        UpdateRequirements();
        if(CheckIfCanCraftItem())
        {
            DisplayAvailable();
        }
        else
        {
            DisplayUnavailable();
        }
    }
    public void UpdateRequirements() // updates needed items in the display
    {
        outputItemName.text = outputItem.name;
        for (int i = 0; i < requiredItemsAmount.Length; i++)
        {
            requiredItemsNumbers[i].text = requiredItemsAmount[i].ToString("f0");
        }
        ItemIconDisplay.sprite = outputItem.icon;
        for (int i = 0; i < requiredItems.Length; i++)
        {
            requiredItemsIconsDisplay[i].sprite = requiredItems[i].icon;
        }
    }
    public bool CheckIfCanCraftItem() // checks if there are enough items in inventory
    {
        bool canCraft = true;
        for (int i = 0; i < requiredItems.Length; i++)
        {
            if (inventory.AmountOfItem(requiredItems[i]) < requiredItemsAmount[i] && canCraft)
            {
                canCraft = false;
            }
        }
        return canCraft;
    }
    public void CraftItem()
    {
        if (CheckIfCanCraftItem())
        {
            //delete items used
            int i = 0;
            foreach (int amount in requiredItemsAmount)
            {
                for (int k = 0; k < amount; k++)
                {
                    inventory.Remove(requiredItems[i]);
                }
                i++;
            }
            //add item created
            for (i = 0; i < outputItemNumber; i++)
            {
                inventory.Add(outputItem);
            }
        }
    }
    public void DisplayAvailable()
    {
        ItemButtonBackground.color = Color.green;
    }
    public void DisplayUnavailable()
    {
        ItemButtonBackground.color = Color.red;
    }
}
