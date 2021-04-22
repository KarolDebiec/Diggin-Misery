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
    void Start()
    {
        /*SetMaxDisplayHealth(100);
        SetMaxDisplayStamina(100);
        SetMaxDisplayHunger(100);*/
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
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
    #endregion
}
