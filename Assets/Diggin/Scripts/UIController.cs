using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject deathPanel;
    public GameObject eqPanel;
    public GameObject quickEQPanel;
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
    void Start()
    {
        /*SetMaxDisplayHealth(100);
        SetMaxDisplayStamina(100);
        SetMaxDisplayHunger(100);*/
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDisplayHealth(playerController.health);
        SetDisplayStamina(playerController.stamina);
        SetDisplayHunger(playerController.hunger);
        SetDisplayThirst(playerController.thirst);
    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuPanelActive())
            {
                hideMenuPanel();
                Time.timeScale = 1;
            }
            else
            {
                displayMenuPanel();
                Time.timeScale = 0;
            }
        }
    }
    /*public void SetMaxDisplayHealth(float health)
    {
        healthbarSlider.maxValue = health;
        healthbarSlider.value = health;
        healthbarText.text = health.ToString("F0");
    }*/
    public void SetDisplayHealth(float health)
    {
        // healthbarSlider.value = health;
        // healthbarText.text = health.ToString("F0");
        healthFill.fillAmount = health / (float)100;
    }
    /*public void SetMaxDisplayStamina(float stamina)
    {
        staminabarSlider.maxValue = stamina;
        staminabarSlider.value = stamina;
        staminabarText.text = stamina.ToString("F0");

    }*/
    public void SetDisplayStamina(float stamina)
    {
        //staminabarSlider.value = stamina;
        //staminabarText.text = stamina.ToString("F0");
        staminaFill.fillAmount = stamina / (float)100;
    }
    /*public void SetMaxDisplayHunger(float Hunger)
    {
        hungerbarSlider.maxValue = Hunger;
        hungerbarSlider.value = Hunger;
        hungerbarText.text = Hunger.ToString("F0");

    }*/
    public void SetDisplayHunger(float Hunger)
    {
        //hungerbarSlider.value = Hunger;
        //hungerbarText.text = Hunger.ToString("F0");
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
    public void displayTime(float time)
    {
        //timeText.text = (time/3600).ToString("0f") + " : " + (time%3600).ToString("0f");
        timeText.text = Mathf.Floor((time / 3600)).ToString("f0") + " : " + Mathf.Floor((time % 3600)/60).ToString("f0");
    }
}
