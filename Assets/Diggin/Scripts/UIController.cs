using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject deathPanel;
    public GameObject eqPanel;
    public GameObject quickEQPanel;
    public GameObject craftingPanel;
    public GameObject menuPanel;
    /* public Slider healthbarSlider;
     public Text healthbarText;
     public Slider staminabarSlider;
     public Text staminabarText;
     public Slider hungerbarSlider;
     public Text hungerbarText;*/
    public Text timeText;
    public Image healthFill;
    public Image staminaFill;
    public Image hungerFill;
    public Image thirstFill;

    Inventory inventory;    // Our current inventory

    public InventorySlot[] slots;
    public InventoryQuickSlot[] quickSlots;
    public Text[] itemsAmount;
    public Image[] itemsIcons;

    public Text[] itemsQuickAmount; // those are in the inventory panel at the bottom
    public Image[] itemsQuickIcons;
    public Text[] itemsQuickAmount2; //those are on the bottom of the screen
    public Image[] itemsQuickIcons2;

    public Image[] itemsQuickBackgrounds;
    public Color ActiveColor;
    public Color InactiveColor;
    public int activeQuickSlot; // active quick inventory slot

    public KeyCode[] keys;

    void Start()
    {
        /*SetMaxDisplayHealth(100);
        SetMaxDisplayStamina(100);
        SetMaxDisplayHunger(100);*/
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        changePickedQuickSlot(-1);
    }
    void Update()
    {
        SetDisplayHealth(playerController.health);
        SetDisplayStamina(playerController.stamina);
        SetDisplayHunger(playerController.hunger);
        SetDisplayThirst(playerController.thirst);
        UpdateInventoryUI();
        UpdateQuickInventoryUI();
    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuPanelActive())
            {
                hideMenuPanel();
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerController.canMove = true;
            }
            else
            {
                displayMenuPanel();
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerController.canMove = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isquickEQPanelActive())
            {
                hidequickEQPanel();
                displayEQPanel();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerController.canMove = false;
            }
            else
            {
                hideEQPanel();
                displayquickEQPanel();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerController.canMove = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isCraftingPanelActive())
            {
                hideCraftingPanel();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerController.canMove = true;
            }
            else
            {
                displayCraftingPanel();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerController.canMove = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(inventory.itemsQuick[0] != null && activeQuickSlot != 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(0);
                playerController.ChangeObjectInHand(inventory.itemsQuick[0]);
                if(inventory.itemsQuick[0] is PlaceableItem)
                {
                    inventory.itemsQuick[0].Place();
                }
            }
            else
            {
                for(int i = 0; i<9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventory.itemsQuick[1] != null && activeQuickSlot != 1)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(1);
                playerController.ChangeObjectInHand(inventory.itemsQuick[1]);
                if (inventory.itemsQuick[1] is PlaceableItem)
                {
                    inventory.itemsQuick[1].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if(inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (inventory.itemsQuick[2] != null && activeQuickSlot != 2)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(2);
                playerController.ChangeObjectInHand(inventory.itemsQuick[2]);
                if (inventory.itemsQuick[2] is PlaceableItem)
                {
                    inventory.itemsQuick[2].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (inventory.itemsQuick[3] != null && activeQuickSlot != 3)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(3);
                playerController.ChangeObjectInHand(inventory.itemsQuick[3]);
                if (inventory.itemsQuick[3] is PlaceableItem)
                {
                    inventory.itemsQuick[3].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (inventory.itemsQuick[4] != null && activeQuickSlot != 4)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(4);
                playerController.ChangeObjectInHand(inventory.itemsQuick[4]);
                if (inventory.itemsQuick[4] is PlaceableItem)
                {
                    inventory.itemsQuick[4].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (inventory.itemsQuick[5] != null && activeQuickSlot != 5)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(5);
                playerController.ChangeObjectInHand(inventory.itemsQuick[5]);
                if (inventory.itemsQuick[5] is PlaceableItem)
                {
                    inventory.itemsQuick[5].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (inventory.itemsQuick[6] != null && activeQuickSlot != 6)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(6);
                playerController.ChangeObjectInHand(inventory.itemsQuick[6]);
                if (inventory.itemsQuick[6] is PlaceableItem)
                {
                    inventory.itemsQuick[6].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (inventory.itemsQuick[7] != null && activeQuickSlot != 7)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(7);
                playerController.ChangeObjectInHand(inventory.itemsQuick[7]);
                if (inventory.itemsQuick[7] is PlaceableItem)
                {
                    inventory.itemsQuick[7].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (inventory.itemsQuick[8] != null && activeQuickSlot != 8)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(8);
                playerController.ChangeObjectInHand(inventory.itemsQuick[8]);
                if (inventory.itemsQuick[8] is PlaceableItem)
                {
                    inventory.itemsQuick[8].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (inventory.itemsQuick[9] != null && activeQuickSlot != 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(9);
                playerController.ChangeObjectInHand(inventory.itemsQuick[9]);
                if (inventory.itemsQuick[9] is PlaceableItem)
                {
                    inventory.itemsQuick[9].Place();
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.itemsQuick[i] != null)
                    {
                        inventory.itemsQuick[i].StopAction();
                    }
                }
                changePickedQuickSlot(-1);
                playerController.ChangeObjectInHand(null);
            }
        }
    }

    #region Basic display functions
    public void SetDisplayHealth(float health)
    {
        healthFill.fillAmount = health / (float)100;
    }
    public void SetDisplayStamina(float stamina)
    {
        staminaFill.fillAmount = stamina / (float)100;
    }
    public void SetDisplayHunger(float Hunger)
    {
        hungerFill.fillAmount = Hunger / (float)100;
    }
    public void SetDisplayThirst(float Thirst)
    {
        thirstFill.fillAmount = Thirst/(float)100;
        
    }
    public void displayEQPanel()
    {
        eqPanel.SetActive(true);
    }
    public void displayDeathPanel()
    {
        deathPanel.SetActive(true);
    }
    public void displayquickEQPanel()
    {
        quickEQPanel.SetActive(true);
    }
    public void displayMenuPanel()
    {
        menuPanel.SetActive(true);
    }
    public void displayCraftingPanel()
    {
        craftingPanel.SetActive(true);
    }
    public void hideEQPanel()
    {
        eqPanel.SetActive(false);
    }
    public void hideDeathPanel()
    {
        deathPanel.SetActive(false);
    }
    public void hidequickEQPanel()
    {
        quickEQPanel.SetActive(false);
    }
    public void hideMenuPanel()
    {
        menuPanel.SetActive(false);
    }
    public void hideCraftingPanel()
    {
        craftingPanel.SetActive(false);
    }
    public bool isEQPanelActive()
    {
        return eqPanel.activeSelf;
    }
    public bool isDeathPanelActive()
    {
        return deathPanel.activeSelf;
    }
    public bool isquickEQPanelActive()
    {
        return quickEQPanel.activeSelf;
    }
    public bool isMenuPanelActive()
    {
        return menuPanel.activeSelf;
    }
    public bool isCraftingPanelActive()
    {
        return craftingPanel.activeSelf;
    }
    public void displayTime(float time)
    {
        //timeText.text = (time/3600).ToString("0f") + " : " + (time%3600).ToString("0f");
        timeText.text = Mathf.Floor((time / 3600)).ToString("f0") + " : " + Mathf.Floor((time % 3600)/60).ToString("f0");
    }
    #endregion

    #region Inventory UI Controller
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(inventory.items[i] != null)
            {
                slots[i].AddItem(inventory.items[i]);
                slots[i].UpdateSlotUI(inventory.itemsNumber[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    public void UpdateQuickInventoryUI()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (inventory.itemsQuick[i] != null)
            {
                quickSlots[i].AddItem(inventory.itemsQuick[i]);
                quickSlots[i].UpdateSlotUI(inventory.itemsNumberQuick[i]);
            }
            else
            {
                quickSlots[i].ClearSlot();
            }
            if (0 < inventory.itemsNumberQuick[i] && inventory.itemsQuick[i].isStackable && inventory.itemsQuick[i]!=null)
            {
//               itemsQuickAmount[i].text = inventory.itemsNumberQuick[i].ToString("f0");
//               itemsQuickIcons[i].sprite = inventory.itemsQuick[i].icon;
                itemsQuickAmount2[i].text = inventory.itemsNumberQuick[i].ToString("f0");
                itemsQuickIcons2[i].sprite = inventory.itemsQuick[i].icon;
            }
            else if(0 < inventory.itemsNumberQuick[i] && !inventory.itemsQuick[i].isStackable)
            {
//               itemsQuickAmount[i].text = " ";
//               itemsQuickIcons[i].sprite = inventory.itemsQuick[i].icon;
                itemsQuickAmount2[i].text = " ";
                itemsQuickIcons2[i].sprite = inventory.itemsQuick[i].icon;
            }
            else
            {
//              itemsQuickAmount[i].text = " ";
//              itemsQuickIcons[i].sprite = null;
                itemsQuickAmount2[i].text = " ";
                itemsQuickIcons2[i].sprite = null;
            }
        }
    }
    public void changePickedQuickSlot(int slot)//0-9
    {
        if(slot >= 0 )
        {
            activeQuickSlot = slot;
            for (int i = 0; i < 10; i++)
            {
                if (i == slot)
                {
                    itemsQuickBackgrounds[i].color = ActiveColor;
                }
                else
                {
                    itemsQuickBackgrounds[i].color = InactiveColor;
                }
            }
        }
        if (slot < 0)
        {
            activeQuickSlot = -1;
            for (int i = 0; i < 10; i++)
            {
                itemsQuickBackgrounds[i].color = InactiveColor;
            }
        }
    }
    #endregion
}
