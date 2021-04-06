using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingItem : MonoBehaviour
{
    public Inventory inventory;
    public int[] requiredItems;
    public int[] requiredItemsAmount;
    public GameObject outputItem;
    public int outputItemNumber; // how many items are going to be crafted
    public Sprite ItemIcon;  //displayed icon of the item
    public Sprite[] requiredItemsIcons;
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
        
    }
    private void Awake()
    {
        UpdateRequirements();
        CheckIfCanCraftItem();
    }
    public void UpdateRequirements() // updates needed items in the display
    {
        int i = 0;
        foreach(Text number in requiredItemsNumbers)
        {
            number.text = requiredItemsAmount[i].ToString("f0");
            i++;
        }
        i = 0;
        ItemIconDisplay.sprite = ItemIcon;
        foreach (Image icon in requiredItemsIconsDisplay)
        {
            icon.sprite = requiredItemsIcons[i];
            i++;
        }
    }
    public void CheckIfCanCraftItem() // checks if there are enough items in inventory
    {
    
    }
    public void CraftItem()
    {
        //delete items used
        //add item created
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
