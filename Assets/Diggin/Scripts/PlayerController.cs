using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIController UIcontroller;
    public GameController gameController;

    public bool alive; // is player alive

    public float health;
    public float stamina;
    public float hunger;
    public float thirst;
    public float hungerLossMultiplier;
    public float hungerHPLossMultiplier;
    public float thirstLossMultiplier;
    public float thirstHPLossMultiplier;
    public bool gettingHungry;
    public bool gettingThirsty;
    public GameObject respawnPoint = null;

     
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(alive && gettingHungry && hunger>0)
        {
            hunger -= hungerLossMultiplier*Time.deltaTime*(float)0.01;
        }
        else if(alive && gettingHungry)
        {
            DamagePlayer(hungerHPLossMultiplier * Time.deltaTime * (float)0.01);
        }
        if (alive && gettingThirsty && thirst > 0)
        {
            thirst -= thirstLossMultiplier * Time.deltaTime * (float)0.01;
        }
        else if (alive && gettingThirsty)
        {
            DamagePlayer(thirstHPLossMultiplier * Time.deltaTime * (float)0.01);
        }
        if (test)
        {
            DamagePlayer(10);
            test = false;
        }
    }

    public void DamagePlayer(float damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {
            health = 0;
            //UIcontroller.SetDisplayHealth(0);
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        alive = false;
        UIcontroller.displayDeathPanel();
        Debug.Log("Player Died");
    }
    public void RespawnPlayer()
    {

    }
    public void AddThirst(float value)
    {
        thirst += value;
    }
    public void AddFood(float value)
    {
        hunger += value;
    }
}
